// Caro3_4/BLL/PvAIManager.cs
using Caro3_4.Class;
using Caro3_4.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro3_4.BLL
{
    public class PvAIManager : BaseGameManager
    {
        private BotManager? botManager;
        private int level;
        private const int AIThinkTimeDelay = 200; // Thời gian chờ giả lập AI suy nghĩ (ms)


        public PvAIManager(Panel chessBoardPanel, TextBox playerNameTextBox, PictureBox playerMarkPictureBox, ProgressBar timerProgressBar,
                           string playerOneName, string playerTwoName, int level)
            : base(chessBoardPanel, playerNameTextBox, playerMarkPictureBox, timerProgressBar, playerOneName, playerTwoName) // playerTwoName thường là "Máy tính"
        {
            this.level = level;
        }

        // Khởi tạo riêng cho PvAI: tạo BotManager
        protected override void InitializeGameSpecifics()
        {
            this.botManager = new BotManager(this.level);
            // Đảm bảo người chơi thứ 2 có tên là "Máy tính" để logic IsAiTurn hoạt động
            if (Players.Count > 1 && Players[1].Name != "Máy tính")
            {
                // Có thể throw exception hoặc sửa lại tên nếu cần
                // Hoặc dựa vào chỉ số CurrentPlayer == 1 để xác định AI
                Console.WriteLine("Warning: Player 2 is expected to be AI ('Máy tính')");
            }
        }

        // Xử lý nước đi của người chơi trong PvAI
        protected override void HandlePlayerMove(Button button)
        {
            // Chỉ xử lý nếu là lượt người chơi (Player 0)
            if (CurrentPlayer == 0) // Giả sử người luôn là Player 0
            {
                PerformMove(button);

                if (CheckEndCondition(button)) return;

                SwitchPlayer(); // Chuyển sang lượt AI
            }
            // Không làm gì nếu click khi đang là lượt AI (panel nên bị disabled)
        }

        // Bắt đầu lượt tiếp theo trong PvAI
        protected override void StartNextTurn()
        {
            if (isGameEnded) return;

            if (IsAiTurn()) // Nếu là lượt AI
            {
                chessBoardPanel.Enabled = false; // Vô hiệu hóa bàn cờ
                timeManager.ResetTimer();       // Reset timer (không start)
                UpdateProgressBarDisplay();     // Hiển thị progress bar đầy cho AI
                Application.DoEvents();         // Xử lý các sự kiện UI còn tồn đọng
                StartComputerMove();            // Bắt đầu nước đi của máy
            }
            else // Nếu là lượt người chơi
            {
                chessBoardPanel.Enabled = true; // Bật lại bàn cờ
                timeManager.StartTimer();       // Bắt đầu đếm giờ
                UpdateProgressBarDisplay();     // Cập nhật progress bar
            }
        }

        // Xác định có phải lượt AI không
        protected override bool IsAiTurn()
        {
            // Giả định AI luôn là người chơi thứ 2 (index 1)
            return CurrentPlayer == 1 && Players.Count > 1 && Players[1].Name == "Máy tính";
            // Hoặc chỉ cần return CurrentPlayer == 1; nếu luôn đảm bảo AI là player 1
        }

        // Thực hiện nước đi của máy
        private async void StartComputerMove()
        {
            if (isGameEnded || botManager == null || PlayTimeLine.Count == Const.CHESS_BOARD_LENGTH * Const.CHESS_BOARD_LENGTH) return;

            // Thêm độ trễ nhỏ để giả lập suy nghĩ và cho UI cập nhật
            await Task.Delay(AIThinkTimeDelay);

            Point computerMovePoint = botManager.CalculateBestMove(Matrix, PlayTimeLine, Players, CurrentPlayer); // CurrentPlayer là index của AI (1)

            // Kiểm tra nước đi hợp lệ trả về từ AI
            if (IsValidComputerMove(computerMovePoint))
            {
                Button computerButton = Matrix[computerMovePoint.Y][computerMovePoint.X];
                if (computerButton.BackgroundImage == null)
                {
                    PerformMove(computerButton); // Thực hiện nước đi của AI

                    if (CheckEndCondition(computerButton)) return; // Kiểm tra thắng/hòa sau nước AI

                    SwitchPlayer(); // Chuyển về lượt người chơi
                }
                else
                {
                    // Lỗi: AI chọn ô đã đánh -> Tìm nước đi thay thế
                    Console.WriteLine($"Error: Bot tried to play on occupied square ({computerMovePoint.X}, {computerMovePoint.Y})");
                    HandleInvalidAiMove();
                }
            }
            else
            {
                // Lỗi: AI không tìm được nước đi hoặc trả về tọa độ không hợp lệ
                Console.WriteLine($"Error: Bot returned invalid move ({computerMovePoint.X}, {computerMovePoint.Y}) or couldn't find a move.");
                HandleInvalidAiMove(); // Xử lý như trường hợp chọn ô đã đánh
            }
        }

        private bool IsValidComputerMove(Point point)
        {
            return point.X >= 0 && point.Y >= 0 &&
                   point.Y < Const.CHESS_BOARD_LENGTH && point.X < Const.CHESS_BOARD_LENGTH;
        }

        // Xử lý khi AI chọn nước không hợp lệ
        private void HandleInvalidAiMove()
        {
            Point fallbackMove = FindFirstEmptyCell(Matrix);
            if (fallbackMove.X != -1)
            {
                Button fallbackButton = Matrix[fallbackMove.Y][fallbackMove.X];
                PerformMove(fallbackButton);
                if (CheckEndCondition(fallbackButton)) return;
                SwitchPlayer();
            }
            else // Bàn cờ đầy nhưng chưa ai thắng? (Lỗi logic hoặc hòa)
            {
                if (!isGameEnded) HandleEndGame(-1); // Hòa
            }
        }


        // Logic Undo cho PvAI (undo cả người và máy)
        public override bool Undo()
        {
            // Chỉ cho phép Undo khi đang là lượt người chơi và đã có ít nhất 2 nước đi (1 người, 1 máy)
            if (isGameEnded || IsAiTurn() || PlayTimeLine.Count < 2) return false;

            timeManager.StopTimer(); // Dừng timer

            // Undo 2 nước (1 của người, 1 của máy)
            for (int i = 0; i < 2; i++)
            {
                if (PlayTimeLine.Count == 0) break; // Không đủ nước để undo
                PlayInfo move = PlayTimeLine.Pop();
                Button btn = Matrix[move.Point.Y][move.Point.X];
                btn.BackgroundImage = null;
                btn.BackColor = Color.FromArgb(200, 200, 200); // Reset màu nền
            }

            // Sau khi undo 2 nước, lượt chơi phải quay về người chơi (Player 0)
            CurrentPlayer = 0;

            ChangePlayerDisplay(); // Cập nhật UI
            StartNextTurn();       // Bắt đầu lại lượt cho người chơi
            chessBoardPanel.Enabled = true; // Bật lại bàn cờ

            return true;
        }

        // Ghi đè Dispose để xử lý BotManager nếu cần (hiện tại BotManager không có tài nguyên cần dispose)
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Giải phóng tài nguyên của BotManager nếu có
                // botManager?.Dispose(); // Nếu BotManager implement IDisposable
            }
            base.Dispose(disposing); // Gọi Dispose của lớp cơ sở
        }
    }
}
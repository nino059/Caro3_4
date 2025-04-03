// Caro3_4/BLL/ChessboardManager.cs
using Caro3_4.Class;
using Caro3_4.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
// Đảm bảo using namespace TimeManager và các BLL khác nếu cần
// using Caro3_4.BLL;

namespace Caro3_4.BLL
{
    public class ChessboardManager : IDisposable
    {
        #region Properties & Fields

        // UI Elements
        private Panel chessBoardPanel;
        private TextBox playerNameTextBox;
        private PictureBox playerMarkPictureBox;
        private ProgressBar timerProgressBar; // ProgressBar để hiển thị thời gian

        // Game State
        public List<List<Button>> Matrix { get; private set; }
        public Stack<PlayInfo> PlayTimeLine { get; private set; }
        public List<Player> Players { get; private set; }
        public int CurrentPlayer { get; private set; }
        public int NumberOfPlayers { get; private set; }
        private bool isGameEnded = false;

        // Dependencies
        private WinChecker winChecker;
        private BotManager? botManager; // Có thể null nếu là 2 người chơi
        private TimeManager timeManager; // Sử dụng TimeManager đã cải thiện để chạy mượt

        // Event
        public event EventHandler? EndedGame;

        #endregion

        #region Initialize

        // Constructor
        public ChessboardManager(Panel chessBoardPanel, TextBox playerNameTextBox, PictureBox playerMarkPictureBox, ProgressBar timerProgressBar,
                                 string playerOneName, string playerTwoName, int numberOfPlayers, int level)
        {
            this.chessBoardPanel = chessBoardPanel;
            this.playerNameTextBox = playerNameTextBox;
            this.playerMarkPictureBox = playerMarkPictureBox;
            this.timerProgressBar = timerProgressBar;
            this.NumberOfPlayers = numberOfPlayers;

            // Khởi tạo danh sách người chơi
            this.Players = new List<Player>()
            {
                new Player(playerOneName, Image.FromFile(Application.StartupPath + "\\Resources\\X.png")),
                new Player(playerTwoName, Image.FromFile(Application.StartupPath + "\\Resources\\O.png"))
            };

            // Khởi tạo các thành phần logic
            this.winChecker = new WinChecker();
            // Khởi tạo TimeManager (phiên bản dùng Stopwatch)
            this.timeManager = new TimeManager();
            this.timeManager.TimeChanged += TimeManager_TimeChanged!;
            this.timeManager.TimeExpired += TimeManager_TimeExpired!;

            // Khởi tạo BotManager nếu chơi với máy
            if (numberOfPlayers == 1)
            {
                this.botManager = new BotManager(level);
            }

            // Khởi tạo ma trận và lịch sử
            Matrix = new List<List<Button>>();
            PlayTimeLine = new Stack<PlayInfo>();

            // *** QUAN TRỌNG: Cấu hình ProgressBar cho mili giây ***
            // Đảm bảo Maximum khớp với TimeLimitMilliseconds trong TimeManager
            this.timerProgressBar.Maximum = TimeManager.TimeLimitMilliseconds; // Ví dụ: 15000
            this.timerProgressBar.Minimum = 0;
            this.timerProgressBar.Value = 0; // Bắt đầu từ 0
        }
        #endregion

        #region Core Game Logic Methods

        // Vẽ bàn cờ và bắt đầu game mới
        public void DrawChessBoard()
        {
            isGameEnded = false;
            chessBoardPanel.Enabled = true;
            chessBoardPanel.Controls.Clear();

            // Reset trạng thái game
            PlayTimeLine = new Stack<PlayInfo>();
            Matrix = new List<List<Button>>();
            CurrentPlayer = 0;

            // *** Cập nhật lại Maximum phòng trường hợp thay đổi ***
            this.timerProgressBar.Maximum = TimeManager.TimeLimitMilliseconds;
            this.timerProgressBar.Value = 0;

            ChangePlayerDisplay();
            timeManager.ResetTimer(); // Đặt lại bộ đếm thời gian về 0
            UpdateProgressBarDisplay(); // Cập nhật hiển thị ProgressBar

            // Vẽ các ô cờ (Button)
            Button oldButton = new Button() { Location = new Point(0, 0), Width = 0 };
            for (int i = 0; i < Const.CHESS_BOARD_LENGTH; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Const.CHESS_BOARD_LENGTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Const.CHESS_WIDTH,
                        Height = Const.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString(),
                        BackColor = Color.FromArgb(200, 200, 200)
                    };
                    btn.Click += btn_Click!;
                    chessBoardPanel.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Const.CHESS_HEIGHT);
                oldButton.Width = 0; oldButton.Height = 0;
            }
            // --- Kết thúc vẽ bàn cờ ---

            // Bắt đầu timer nếu là lượt người hoặc gọi AI nếu là lượt máy
            if (Players[CurrentPlayer].Name != "Máy tính")
            {
                timeManager.StartTimer(); // Bắt đầu đếm giờ cho người
            }
            else if (NumberOfPlayers == 1)
            {
                UpdateProgressBarDisplay();
                StartComputerMove();
            }
        }

        // Xử lý khi người chơi click vào ô cờ
        private void btn_Click(object sender, EventArgs e)
        {
            if (isGameEnded) return;
            Button? button = sender as Button;
            if (button == null || button.BackgroundImage != null) return;

            if (Players[CurrentPlayer].Name != "Máy tính")
            {
                timeManager.StopTimer(); // Dừng timer của người chơi
            }

            PerformMove(button); // Thực hiện nước đi

            if (CheckEndCondition(button)) return; // Kiểm tra kết thúc

            SwitchPlayer(); // Chuyển lượt

            // Nếu đến lượt máy
            if (NumberOfPlayers == 1 && Players[CurrentPlayer].Name == "Máy tính")
            {
                chessBoardPanel.Enabled = false;
                Application.DoEvents();
                UpdateProgressBarDisplay();
                StartComputerMove();
            }
        }

        // Thực hiện việc đánh dấu ô cờ và lưu lịch sử
        private void PerformMove(Button button)
        {
            MarkChess(button, Players[CurrentPlayer]);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(button), CurrentPlayer));
            // Reset màu nền các ô khác (tùy chọn)
            button.BackColor = Color.AliceBlue; // Highlight ô vừa đánh
        }

        // Gọi AI để tính toán và thực hiện nước đi
        private void StartComputerMove()
        {
            if (isGameEnded || botManager == null || PlayTimeLine.Count == Const.CHESS_BOARD_LENGTH * Const.CHESS_BOARD_LENGTH) return;

            Point computerMovePoint = botManager.CalculateBestMove(Matrix, PlayTimeLine, Players, CurrentPlayer);

            if (computerMovePoint.X >= 0 && computerMovePoint.Y >= 0 &&
                computerMovePoint.Y < Const.CHESS_BOARD_LENGTH && computerMovePoint.X < Const.CHESS_BOARD_LENGTH)
            {
                Button computerButton = Matrix[computerMovePoint.Y][computerMovePoint.X];
                if (computerButton.BackgroundImage == null)
                {
                    PerformMove(computerButton);
                    if (CheckEndCondition(computerButton)) return;
                    SwitchPlayer();
                }
                else
                {
                    Console.WriteLine($"Error: Bot tried to play on occupied square ({computerMovePoint.X}, {computerMovePoint.Y})");
                    Point fallbackMove = FindFirstEmptyCell(Matrix);
                    if (fallbackMove.X != -1)
                    {
                        Button fallbackButton = Matrix[fallbackMove.Y][fallbackMove.X];
                        PerformMove(fallbackButton);
                        if (CheckEndCondition(fallbackButton)) return;
                        SwitchPlayer();
                    }
                    else HandleEndGame(-1); // Hòa
                }
            }
            else
            {
                Console.WriteLine("Error: Bot could not find a valid move.");
                if (PlayTimeLine.Count == Const.CHESS_BOARD_LENGTH * Const.CHESS_BOARD_LENGTH) HandleEndGame(-1); // Hòa
            }
        }

        // Kiểm tra điều kiện kết thúc game (thắng, hòa)
        private bool CheckEndCondition(Button lastMoveButton)
        {
            if (isGameEnded) return true;
            Point lastPoint = GetChessPoint(lastMoveButton);
            int currentPlayerIndex = PlayTimeLine.Peek().CurrentPlayer;

            if (winChecker.IsWin(Matrix, lastPoint))
            {
                HandleEndGame(currentPlayerIndex); // Thắng
                return true;
            }
            if (winChecker.IsDraw(PlayTimeLine.Count))
            {
                HandleEndGame(-1); // Hòa
                return true;
            }
            return false;
        }

        // Xử lý khi game kết thúc
        private void HandleEndGame(int winnerIndex, bool isTimeout = false)
        {
            if (isGameEnded) return;
            isGameEnded = true;
            timeManager.StopTimer();
            chessBoardPanel.Enabled = false;
            UpdateProgressBarDisplay(); // Cập nhật lần cuối

            string message;
            if (isTimeout)
            {
                int loserIndex = winnerIndex; // winnerIndex lúc này là người bị hết giờ
                winnerIndex = 1 - loserIndex;
                if (Players.Count > loserIndex && Players.Count > winnerIndex)
                    message = $"{Players[loserIndex].Name} đã hết giờ! {Players[winnerIndex].Name} thắng!";
                else
                    message = "Hết giờ!";
            }
            else
            {
                if (winnerIndex == -1) message = "Hòa cờ!";
                else if (Players.Count > winnerIndex) message = $"{Players[winnerIndex].Name} đã thắng!";
                else message = "Có người thắng!";
            }

            MessageBox.Show(message, "Kết thúc", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EndedGame?.Invoke(this, EventArgs.Empty);
        }


        // Chức năng Undo (hoàn tác nước đi)
        public bool Undo()
        {
            if (isGameEnded) return false;

            int movesToUndo = (NumberOfPlayers == 1 && PlayTimeLine.Count > 1 && Players[1 - CurrentPlayer].Name == "Máy tính") ? 2 : 1;
            if (PlayTimeLine.Count < movesToUndo || (NumberOfPlayers == 1 && Players[CurrentPlayer].Name == "Máy tính"))
                return false;

            timeManager.StopTimer();

            for (int i = 0; i < movesToUndo; i++)
            {
                if (PlayTimeLine.Count == 0) break;
                PlayInfo oldMove = PlayTimeLine.Pop();
                Button btn = Matrix[oldMove.Point.Y][oldMove.Point.X];
                btn.BackgroundImage = null;
                btn.BackColor = Color.FromArgb(200, 200, 200);
            }

            if (PlayTimeLine.Count == 0) CurrentPlayer = 0;
            else CurrentPlayer = PlayTimeLine.Peek().CurrentPlayer; //*** SỬA LỖI LOGIC: phải là người chơi của nước đi TRƯỚC ĐÓ, không phải người đi nước vừa xóa ***
                                                                    // -> Nên đặt lại CurrentPlayer dựa vào người chơi của nước đi trên đỉnh stack MỚI
            if (PlayTimeLine.Count > 0)
                CurrentPlayer = PlayTimeLine.Peek().CurrentPlayer;
            else
                CurrentPlayer = 0; // Nếu stack rỗng, quay về người chơi 0


            ChangePlayerDisplay();
            timeManager.StartTimer(); // Bắt đầu lại timer cho người chơi hiện tại sau undo
            chessBoardPanel.Enabled = true;
            UpdateProgressBarDisplay();

            return true;
        }


        // Chuyển lượt chơi sang người tiếp theo
        private void SwitchPlayer()
        {
            if (isGameEnded) return;
            CurrentPlayer = 1 - CurrentPlayer;
            ChangePlayerDisplay();
            timeManager.StopTimer(); // Dừng timer cũ

            if (Players[CurrentPlayer].Name != "Máy tính")
            {
                chessBoardPanel.Enabled = true;
                timeManager.StartTimer(); // Bắt đầu timer mới cho người
            }
            else
            {
                timeManager.ResetTimer(); // Reset timer cho bot (không start)
                UpdateProgressBarDisplay(); // Cập nhật hiển thị cho bot
            }
        }

        // Cập nhật hiển thị tên và quân cờ của người chơi hiện tại
        private void ChangePlayerDisplay()
        {
            if (Players != null && Players.Count > CurrentPlayer && CurrentPlayer >= 0)
            {
                playerNameTextBox.Text = Players[CurrentPlayer].Name;
                playerMarkPictureBox.Image = Players[CurrentPlayer].Mark;
            }
        }

        // Cập nhật giá trị và hiển thị của ProgressBar (phiên bản mượt)
        private void UpdateProgressBarDisplay()
        {
            if (timerProgressBar == null || timerProgressBar.IsDisposed || timerProgressBar.Disposing) return;

            if (timerProgressBar.InvokeRequired)
            {
                timerProgressBar.Invoke(new MethodInvoker(UpdateProgressBarDisplay));
            }
            else
            {
                long valueToShowMs; // Sử dụng long vì ElapsedMilliseconds là long
                bool showProgress = true;

                if (isGameEnded)
                {
                    valueToShowMs = 0; // Kết thúc thì về 0
                }
                else if (Players.Count > CurrentPlayer && Players[CurrentPlayer].Name == "Máy tính")
                {
                    valueToShowMs = timerProgressBar.Maximum; // Hiển thị đầy cho bot
                    // showProgress = false; // Bỏ comment nếu muốn ẩn khi bot đánh
                }
                else // Lượt của người chơi
                {
                    valueToShowMs = timeManager.ElapsedMilliseconds; // Lấy mili giây đã trôi qua
                }

                // Đảm bảo giá trị nằm trong khoảng hợp lệ [Minimum, Maximum] và ép kiểu về int
                int progressBarValue = (int)Math.Max(timerProgressBar.Minimum, Math.Min(valueToShowMs, timerProgressBar.Maximum));

                timerProgressBar.Visible = showProgress;
                if (showProgress)
                {
                    // Cập nhật giá trị ProgressBar
                    if (progressBarValue >= timerProgressBar.Minimum && progressBarValue <= timerProgressBar.Maximum) // Kiểm tra lần cuối
                    {
                        timerProgressBar.Value = progressBarValue;
                    }
                    else if (progressBarValue < timerProgressBar.Minimum)
                    {
                        timerProgressBar.Value = timerProgressBar.Minimum;
                    }
                    else
                    { // progressBarValue > timerProgressBar.Maximum
                        timerProgressBar.Value = timerProgressBar.Maximum;
                    }
                }
            }
        }


        // Xử lý sự kiện khi TimeManager thay đổi thời gian
        private void TimeManager_TimeChanged(object sender, EventArgs e)
        {
            UpdateProgressBarDisplay(); // Cập nhật ProgressBar thường xuyên
        }

        // Xử lý sự kiện khi TimeManager báo hết giờ
        private void TimeManager_TimeExpired(object sender, EventArgs e)
        {
            if (!isGameEnded)
            {
                // Gọi HandleEndGame, báo là hết giờ, truyền index người đang có lượt (CurrentPlayer)
                HandleEndGame(CurrentPlayer, true);
            }
        }

        #endregion

        #region Helper Methods

        // Đặt hình ảnh quân cờ lên Button
        private void MarkChess(Button btn, Player player)
        {
            if (btn != null && player != null && player.Mark != null)
            {
                btn.BackgroundImage = player.Mark;
            }
        }

        // Lấy tọa độ (cột, hàng) của Button trên bàn cờ
        private Point GetChessPoint(Button btn)
        {
            int row = Convert.ToInt32(btn.Tag);
            int col = Matrix[row].IndexOf(btn);
            return new Point(col, row);
        }

        // Tìm ô trống đầu tiên trên bàn cờ
        private Point FindFirstEmptyCell(List<List<Button>> matrix)
        {
            for (int r = 0; r < Const.CHESS_BOARD_LENGTH; r++)
                for (int c = 0; c < Const.CHESS_BOARD_LENGTH; c++)
                    if (matrix[r][c].BackgroundImage == null)
                        return new Point(c, r);
            return new Point(-1, -1);
        }

        #endregion

        #region Dispose Pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                timeManager?.Dispose(); // Giải phóng TimeManager
                if (Players != null)
                {
                    foreach (var player in Players) { player.Mark?.Dispose(); }
                    Players.Clear();
                }
                if (chessBoardPanel != null && !chessBoardPanel.IsDisposed)
                {
                    // Cân nhắc việc xóa Controls nếu cần thiết, nhưng thường không bắt buộc
                    // chessBoardPanel.Controls.Clear();
                }
            }
        }

        ~ChessboardManager()
        {
            Dispose(false);
        }

        #endregion
    }
}
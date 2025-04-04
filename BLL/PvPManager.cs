// Caro3_4/BLL/PvPManager.cs
using Caro3_4.Class;
using Caro3_4.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Caro3_4.BLL
{
    public class PvPManager : BaseGameManager
    {
        public PvPManager(Panel chessBoardPanel, TextBox playerNameTextBox, PictureBox playerMarkPictureBox, ProgressBar timerProgressBar,
                          string playerOneName, string playerTwoName)
            : base(chessBoardPanel, playerNameTextBox, playerMarkPictureBox, timerProgressBar, playerOneName, playerTwoName)
        {
        }

        // Khởi tạo riêng cho PvP (hiện tại không cần gì thêm)
        protected override void InitializeGameSpecifics()
        {
            // Không cần BotManager
        }

        // Xử lý nước đi của người chơi trong PvP
        protected override void HandlePlayerMove(Button button)
        {
            PerformMove(button); // Thực hiện nước đi (đặt cờ, lưu stack)

            if (CheckEndCondition(button)) // Kiểm tra thắng/hòa
            {
                return; // Kết thúc game
            }

            SwitchPlayer(); // Chuyển lượt
        }

        // Bắt đầu lượt tiếp theo trong PvP: chỉ cần bật timer
        protected override void StartNextTurn()
        {
            chessBoardPanel.Enabled = true; // Đảm bảo bàn cờ được bật
            timeManager.StartTimer();       // Bắt đầu đếm giờ cho người chơi tiếp theo
            UpdateProgressBarDisplay();     // Cập nhật thanh thời gian
        }

        // PvP không có lượt của AI
        protected override bool IsAiTurn()
        {
            return false;
        }


        // Logic Undo cho PvP (chỉ cần undo 1 nước)
        public override bool Undo()
        {
            if (isGameEnded || PlayTimeLine.Count < 1) return false;

            timeManager.StopTimer(); // Dừng timer

            // Undo 1 nước
            PlayInfo lastMove = PlayTimeLine.Pop();
            Button btn = Matrix[lastMove.Point.Y][lastMove.Point.X];
            btn.BackgroundImage = null;
            btn.BackColor = Color.FromArgb(200, 200, 200); // Reset màu nền

            // Xác định lại người chơi hiện tại
            // CurrentPlayer đã bị đảo ở SwitchPlayer() trước đó, nên chỉ cần KHÔNG đảo lại là đúng
            // Nếu stack rỗng, quay về người chơi 0, nếu không giữ nguyên CurrentPlayer hiện tại (là người vừa bị undo)
            if (PlayTimeLine.Count == 0)
                CurrentPlayer = 0;
            else
                // Người chơi hiện tại chính là người thực hiện nước đi vừa bị undo
                CurrentPlayer = lastMove.CurrentPlayer; // Đặt lại đúng người chơi


            ChangePlayerDisplay();      // Cập nhật UI
            StartNextTurn();            // Bắt đầu lại lượt cho người chơi đó
            chessBoardPanel.Enabled = true; // Đảm bảo bàn cờ được bật lại

            return true;
        }
    }
}
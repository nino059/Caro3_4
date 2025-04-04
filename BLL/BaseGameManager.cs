using Caro3_4.Class;
using Caro3_4.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Caro3_4.BLL
{
    public abstract class BaseGameManager : IDisposable
    {
        #region Properties & Fields (Shared)

        // UI Elements
        protected Panel chessBoardPanel;
        protected TextBox playerNameTextBox;
        protected PictureBox playerMarkPictureBox;
        protected ProgressBar timerProgressBar;

        // Game State
        public List<List<Button>> Matrix { get; protected set; }
        public Stack<PlayInfo> PlayTimeLine { get; protected set; }
        public List<Player> Players { get; protected set; }
        public int CurrentPlayer { get; protected set; }
        protected bool isGameEnded = false; // Giữ là protected hoặc private

        // Dependencies
        protected WinChecker winChecker;
        protected TimeManager timeManager; // Giữ là protected

        // Event
        public event EventHandler? EndedGame;

        #endregion

        #region Initialize

        public BaseGameManager(Panel chessBoardPanel, TextBox playerNameTextBox, PictureBox playerMarkPictureBox, ProgressBar timerProgressBar,
                               string playerOneName, string playerTwoName)
        {
            this.chessBoardPanel = chessBoardPanel;
            this.playerNameTextBox = playerNameTextBox;
            this.playerMarkPictureBox = playerMarkPictureBox;
            this.timerProgressBar = timerProgressBar;

            this.Players = new List<Player>()
            {
                new Player(playerOneName, Image.FromFile(Application.StartupPath + "\\Resources\\X.png")),
                new Player(playerTwoName, Image.FromFile(Application.StartupPath + "\\Resources\\O.png"))
            };

            this.winChecker = new WinChecker();
            this.timeManager = new TimeManager();
            this.timeManager.TimeChanged += TimeManager_TimeChanged!;
            this.timeManager.TimeExpired += TimeManager_TimeExpired!;

            Matrix = new List<List<Button>>();
            PlayTimeLine = new Stack<PlayInfo>();

            ConfigureProgressBar();   
            InitializeGameSpecifics();  
        }
        // Đặt lại progressbar
        protected virtual void ConfigureProgressBar()
        {
            this.timerProgressBar.Maximum = Const.TimeLimitMilliseconds;
            this.timerProgressBar.Minimum = 0;
            this.timerProgressBar.Value = 0;
        }
        // Gọi hàm trừu tượng để lớp con cài đặt
        protected abstract void InitializeGameSpecifics();

        #endregion

        #region Public Accessors/Methods for Form Interaction

        // Đọc trạng thái isGameEnded
        public bool IsGameFinished => isGameEnded;

        // Kiểm tra null phòng trường hợp timeManager chưa được khởi tạo hoặc đã bị Dispose
        public void StopCurrentTimer()
        {
            if (timeManager != null && timeManager.IsRunning)
            {
                timeManager.StopTimer();
            }
        }

        // Thử resume lượt đi
        public void ResumeTurn()
        {
            // Resume nếu game chưa kết thúc VÀ không phải lượt AI VÀ timer không đang chạy
            if (!isGameEnded && !IsAiTurn() && timeManager != null && !timeManager.IsRunning)
            {
                timeManager.StartTimer();
                UpdateProgressBarDisplay(); // Cập nhật lại UI timer
            }
            // Nếu là lượt AI hoặc game đã kết thúc, không làm gì cả
        }

        #endregion

        #region Logic Methods 

        public virtual void StartNewGame()
        {
            isGameEnded = false;
            chessBoardPanel.Enabled = true;
            chessBoardPanel.Controls.Clear();
            PlayTimeLine = new Stack<PlayInfo>();
            Matrix = new List<List<Button>>();
            CurrentPlayer = 0;

            ConfigureProgressBar();
            ChangePlayerDisplay();
            timeManager.ResetTimer();
            UpdateProgressBarDisplay();

            DrawChessBoardInternal();

            StartNextTurn();
        }

        protected virtual void DrawChessBoardInternal()
        {
            this.chessBoardPanel.SuspendLayout();  // Ngăn vẽ lại panel cho đến khi xong
            this.chessBoardPanel.Controls.Clear();

            Button oldButton = new Button() { Location = new Point(0, 0), Width = 0 };
            for (int i = 0; i < Const.CHESS_BOARD_LENGTH; i++)
            {
                if (i >= Matrix.Count)  // Đảm bảo đủ số hàng
                {
                    Matrix.Add(new List<Button>());
                }
                for (int j = 0; j <= Const.CHESS_BOARD_LENGTH; j++)
                {
                    Button btn;
                    if (j < Matrix[i].Count)
                    {
                        btn = Matrix[i][j];  // Tái sử dụng button cũ
                    }
                    else
                    {
                        btn = new Button();
                        btn.Width = Const.CHESS_WIDTH;
                        btn.Height = Const.CHESS_HEIGHT;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        btn.Tag = i.ToString();
                        btn.Click += Btn_Click!;
                        Matrix[i].Add(btn);  // Thêm vào hàng
                    }

                    btn.Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y);
                    btn.BackColor = Color.FromArgb(200, 200, 200);
                    btn.BackgroundImage = null;  // Xóa hình ảnh cờ (X hoặc O)
                    this.chessBoardPanel.Controls.Add(btn);
                    oldButton = btn;
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Const.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
            this.chessBoardPanel.ResumeLayout(true); // Vẽ lại panel
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (isGameEnded || sender == null) return;
            Button? button = sender as Button;
            if (button == null || button.BackgroundImage != null) return;

            if (timeManager.IsRunning)
                timeManager.StopTimer();

            HandlePlayerMove(button);
        }

        protected abstract void HandlePlayerMove(Button button);

        protected virtual void PerformMove(Button button)
        {
            MarkChess(button, Players[CurrentPlayer]);
            PlayTimeLine.Push(new PlayInfo(GetChessPoint(button), CurrentPlayer));
            button.BackColor = Color.AliceBlue;
        }

        protected virtual bool CheckEndCondition(Button lastMoveButton)
        {
            if (isGameEnded) return true;
            Point lastPoint = GetChessPoint(lastMoveButton);
            if (lastPoint.X == -1) return false; // Thêm kiểm tra nếu GetChessPoint lỗi

            int playerIndexOfMove = PlayTimeLine.Peek().CurrentPlayer;

            if (winChecker.IsWin(Matrix, lastPoint))
            {
                HandleEndGame(playerIndexOfMove);
                return true;
            }
            if (winChecker.IsDraw(PlayTimeLine.Count))
            {
                HandleEndGame(-1);
                return true;
            }
            return false;
        }

        protected virtual void HandleEndGame(int winnerIndex, bool isTimeout = false)
        {
            if (isGameEnded) return;
            isGameEnded = true;
            timeManager.StopTimer();
            chessBoardPanel.Enabled = false;
            UpdateProgressBarDisplay();

            string message;
            if (isTimeout)
            {
                int loserIndex = winnerIndex;
                winnerIndex = 1 - loserIndex;
                if (Players.Count > loserIndex && Players.Count > winnerIndex && loserIndex >= 0 && winnerIndex >= 0)
                    message = $"{Players[loserIndex].Name} đã hết giờ! {Players[winnerIndex].Name} thắng!";
                else
                    message = "Hết giờ!";
            }
            else
            {
                if (winnerIndex == -1) message = "Hòa cờ!";
                else if (Players.Count > winnerIndex && winnerIndex >= 0) message = $"{Players[winnerIndex].Name} đã thắng!";
                else message = "Có người thắng!";
            }

            MessageBox.Show(message, "Kết thúc", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EndedGame?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void SwitchPlayer()
        {
            if (isGameEnded) return;
            CurrentPlayer = 1 - CurrentPlayer;
            ChangePlayerDisplay();

            if (timeManager.IsRunning)
                timeManager.StopTimer();

            StartNextTurn();
        }

        protected abstract void StartNextTurn();

        // Khai báo IsAiTurn là abstract để lớp con phải định nghĩa
        protected abstract bool IsAiTurn();

        protected virtual void ChangePlayerDisplay()
        {
            if (Players != null && Players.Count > CurrentPlayer && CurrentPlayer >= 0)
            {
                playerNameTextBox.Text = Players[CurrentPlayer].Name;
                playerMarkPictureBox.Image = Players[CurrentPlayer].Mark;
            }
        }

        protected virtual void UpdateProgressBarDisplay()
        {
            if (timerProgressBar == null || timerProgressBar.IsDisposed || timerProgressBar.Disposing) return;

            if (timerProgressBar.InvokeRequired)
            {
                try { timerProgressBar.Invoke(new MethodInvoker(UpdateProgressBarDisplay)); } catch { /* Bỏ qua lỗi nếu form đã đóng */ }
            }
            else
            {
                long valueToShowMs = 0;
                bool showProgress = !isGameEnded;

                if (!isGameEnded)
                {
                    if (IsAiTurn())  // THÊM ĐOẠN NÀY
                    {
                        valueToShowMs = timerProgressBar.Maximum; // Hoặc 0, hoặc giữ nguyên giá trị
                        showProgress = false; // Ẩn thanh progress bar
                    }
                    else
                    {
                        // Kiểm tra timeManager trước khi truy cập
                        if (timeManager != null)
                            valueToShowMs = timeManager.ElapsedMilliseconds;
                        else
                            valueToShowMs = 0; // Hoặc giá trị mặc định khác
                    }
                }

                int progressBarValue = (int)Math.Max(timerProgressBar.Minimum, Math.Min(valueToShowMs, timerProgressBar.Maximum));

                timerProgressBar.Visible = showProgress;
                if (showProgress)
                {
                    if (progressBarValue >= timerProgressBar.Minimum && progressBarValue <= timerProgressBar.Maximum)
                        timerProgressBar.Value = progressBarValue;
                    else if (progressBarValue < timerProgressBar.Minimum)
                        timerProgressBar.Value = progressBarValue;
                    else
                        timerProgressBar.Value = timerProgressBar.Maximum;
                }
            }
        }

        private void TimeManager_TimeChanged(object sender, EventArgs e)
        {
            UpdateProgressBarDisplay();
        }

        private void TimeManager_TimeExpired(object sender, EventArgs e)
        {
            if (!isGameEnded)
            {
                HandleEndGame(CurrentPlayer, true);
            }
        }

        public abstract bool Undo();

        #endregion

        #region Helper Methods (Shared)

        protected virtual void MarkChess(Button btn, Player player)
        {
            if (btn != null && player != null && player.Mark != null)
            {
                btn.BackgroundImage = player.Mark;
            }
        }

        protected virtual Point GetChessPoint(Button btn)
        {
            try
            {
                int row = Convert.ToInt32(btn.Tag);
                int col = Matrix[row].IndexOf(btn);
                return new Point(col, row);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting chess point for button {btn.Name}: {ex.Message}");
                return new Point(-1, -1);
            }
        }

        protected virtual Point FindFirstEmptyCell(List<List<Button>> matrix)
        {
            for (int r = 0; r < Const.CHESS_BOARD_LENGTH; r++)
                for (int c = 0; c < Const.CHESS_BOARD_LENGTH; c++)
                    // Thêm kiểm tra null cho matrix[r] và matrix[r][c]
                    if (matrix != null && matrix.Count > r && matrix[r] != null && matrix[r].Count > c && matrix[r][c] != null && matrix[r][c].BackgroundImage == null)
                        return new Point(c, r);
            return new Point(-1, -1);
        }

        #endregion

        #region Dispose Pattern (Shared)

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    timeManager?.Dispose(); // Dispose an toàn
                    if (Players != null)
                    {
                        foreach (var player in Players) { player.Mark?.Dispose(); }
                        Players.Clear();
                    }
                }
                disposed = true;
            }
        }

        ~BaseGameManager()
        {
            Dispose(false);
        }

        #endregion
    }
}
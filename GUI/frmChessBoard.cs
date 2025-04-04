using Caro3_4.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro3_4.GUI
{
    public partial class frmChessBoard : Form
    {
        #region Properties

        BaseGameManager gameManager;

        #endregion

        #region Methods
        public frmChessBoard(string playerOne, string playerTwo, int numberOfPlayers, int level)
        {
            InitializeComponent();

            // *** THAY ĐỔI: Khởi tạo lớp quản lý phù hợp ***
            if (numberOfPlayers == 1)
            {
                // Chế độ chơi với máy
                gameManager = new PvAIManager(pnlChessBoard, txbPlayer1Name, picbMark1, proTimer, playerOne, playerTwo, level);
                this.Text = "Caro PvB"; // Cập nhật tiêu đề form
            }
            else // numberOfPlayers == 2
            {
                // Chế độ 2 người chơi
                gameManager = new PvPManager(pnlChessBoard, txbPlayer1Name, picbMark1, proTimer, playerOne, playerTwo);
                this.Text = "Caro PvP"; // Cập nhật tiêu đề form
            }

            // Gắn sự kiện EndedGame từ gameManager
            gameManager.EndedGame += ChessBoard_EndedGame!;

            // Bắt đầu game mới thông qua gameManager
            StartNewGame(); // Đổi tên hàm để rõ ràng hơn
        }

        void EndGame()
        {
            // Panel đã được vô hiệu hóa bởi gameManager khi game kết thúc
            undoToolStripMenuItem.Enabled = false; // Vô hiệu hóa Undo khi game kết thúc
            // Các cập nhật UI khác nếu cần
        }

        void StartNewGame()
        {
            undoToolStripMenuItem.Enabled = true; // Bật lại Undo khi bắt đầu game mới
            gameManager.StartNewGame();        // Gọi hàm bắt đầu của gameManager
        }

        void Undo()
        {
            gameManager.Undo(); // Gọi hàm Undo của gameManager
        }

        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            // Được gọi bởi manager khi game kết thúc
            EndGame(); // Cập nhật UI của Form (ví dụ: menu)
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) { StartNewGame(); }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e) { Undo(); }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) { this.Close(); }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dừng timer tạm thời trước khi hỏi
            gameManager?.StopCurrentTimer();

            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
                // Nếu người dùng không thoát và game chưa kết thúc,
                // CÓ THỂ cần resume timer nếu đang là lượt người chơi.
                // Cách an toàn hơn là để nó tự resume khi người chơi click tiếp.
                // Hoặc gọi ResumeTurn nếu bạn đã tạo nó:
                if (gameManager != null && !gameManager.IsGameFinished)
                {
                    gameManager!.ResumeTurn(); // Chỉ gọi nếu ResumeTurn được implement đúng
                }
                // HOẶC BỎ HẲN PHẦN KIỂM TRA VÀ RESUME Ở ĐÂY
            }
            else
            {
                // Thoát: Giải phóng tài nguyên
                gameManager?.Dispose();
            }
        }

        // Các phương thức khác giữ nguyên...
        private void frmChessBoard_Load(object sender, EventArgs e) { /*...*/ }
        private void btnPlayMusic_Click(object sender, EventArgs e) { /*...*/ }
        #endregion

        private void pnlPlayer1Info_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
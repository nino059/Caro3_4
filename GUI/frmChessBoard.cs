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
using System.Windows.Forms;

namespace Caro3_4.GUI
{
    public partial class frmChessBoard : Form
    {
        #region Properties
        ChessboardManager ChessBoardManager;
        #endregion

        #region Methods
        public frmChessBoard(string playerOne, string playerTwo, int numberOfPlayers, int level)
        {
            InitializeComponent();

            // *** THAY ĐỔI: Truyền progressBarTimer vào constructor ***
            // Đảm bảo progressBarTimer đã được thêm vào form trong Designer
            ChessBoardManager = new ChessboardManager(pnlChessBoard, txbPlayerName, picbMark, proTimer, playerOne, playerTwo, numberOfPlayers, level);

            ChessBoardManager.EndedGame += ChessBoard_EndedGame!;

            NewGame();
        }

        void EndGame()
        {
            // Panel đã được vô hiệu hóa bởi ChessboardManager
            // Chỉ cần cập nhật UI của Form nếu cần
            undoToolStripMenuItem.Enabled = false;
        }

        void NewGame()
        {
            undoToolStripMenuItem.Enabled = true; // Bật lại Undo khi chơi mới
            // DrawChessBoard sẽ tự động kích hoạt lại panel và reset timer/progressbar
            ChessBoardManager.DrawChessBoard();
        }

        // ... (Các hàm Quit, Undo, event handlers giữ nguyên) ...

        // Undo chỉ gọi manager, không cần bật lại panel ở đây
        void Undo() { ChessBoardManager.Undo(); }


        private void ChessBoard_EndedGame(object sender, EventArgs e)
        {
            // Được gọi bởi manager khi game kết thúc
            EndGame(); // Cập nhật UI của Form (ví dụ: menu)
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) { NewGame(); }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e) { Undo(); }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) { this.Close(); } // Gọi Close để kích hoạt FormClosing

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                // Quan trọng: Giải phóng manager để dừng timer và tài nguyên khác
                ChessBoardManager?.Dispose();
            }
        }

        private void btnPlayMusic_Click(object sender, EventArgs e) { /*...*/ }
        #endregion
    }
}
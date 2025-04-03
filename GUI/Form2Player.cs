using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro3_4.GUI
{
    public partial class Form2Player : Form
    {
        public Form2Player()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (txtPlayer1.Text == "")
            {
                errorMessage("Người chơi 1 chưa nhập tên!");
                return;
            }
            if (txtPlayer2.Text == "")
            {
                errorMessage("Người chơi 2 chưa nhập tên!");
                return;
            }
            if (txtPlayer1.Text == txtPlayer2.Text)
            {
                errorMessage("Tên hai người chơi không được trùng nhau!");
                return;
            }
            frmChessBoard formGame = new frmChessBoard(txtPlayer1.Text, txtPlayer2.Text, 2, 0);
            
            formGame.ShowDialog();
            this.Close();
        }
        private void errorMessage(string text)
        {
            MessageBox.Show(text, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

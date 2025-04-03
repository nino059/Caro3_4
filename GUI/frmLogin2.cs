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
    public partial class frmLogin2 : Form
    {
        public frmLogin2()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName1.Text == "" || txtUserName1.Text == "Tên người chơi 1")
            {
                errorMessage("Người chơi 1 chưa nhập tên!");
                return;
            }
            if (txtUserName2.Text == "" || txtUserName2.Text == "Tên người chơi 2")
            {
                errorMessage("Người chơi 2 chưa nhập tên!");
                return;
            }
            if (txtUserName1.Text == txtUserName2.Text)
            {
                errorMessage("Tên hai người chơi không được trùng nhau!");
                return;
            }
            frmChessBoard formGame = new frmChessBoard(txtUserName1.Text, txtUserName2.Text, 2, 0);
            formGame.StartPosition = FormStartPosition.CenterParent;
            formGame.ShowDialog();
            this.Close();
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }     
        private void errorMessage(string text)
        {
            MessageBox.Show(text, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Khi người dùng nhấn vào textbox để nhập thì mất chữ hiện thị "Tên người chơi"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName1_Enter(object sender, EventArgs e)
        {
            if (txtUserName1.Text == "Tên người chơi 1")
            {
                txtUserName1.Text = "";
                txtUserName1.ForeColor = Color.ForestGreen;
            }
        }
        private void txtUserName2_Enter(object sender, EventArgs e)
        {
            if (txtUserName2.Text == "Tên người chơi 2")
            {
                txtUserName2.Text = "";
                txtUserName2.ForeColor = Color.ForestGreen;
            }
        }

        /// <summary>
        /// Khi rời khỏi textName mà chưa nhập gì thì hiện lại "Tên người chơi"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName1.Text))
            {
                txtUserName1.Text = "Tên người chơi 1";
                txtUserName1.ForeColor = Color.Gray;
            }
        }
        private void txtUserName2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName2.Text))
            {
                txtUserName2.Text = "Tên người chơi 2";
                txtUserName2.ForeColor = Color.Gray;
            }
        }


        /// <summary>
        /// nhấn enter sau khi nhập tên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserName2.Focus();
                e.SuppressKeyPress = true;  // Ngăn không cho nhấn Enter trong TextBox
            }
        }
        private void txtUserName2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
                e.SuppressKeyPress = true;  // Ngăn không cho nhấn Enter trong TextBox
            }
        }

    }
}

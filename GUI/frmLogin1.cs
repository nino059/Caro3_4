using Caro3_4.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro3_4.GUI
{
    public partial class frmLogin1 : Form
    {

        #region Tạo khung bo tròn
        // Ghi đè phương thức OnLoad để áp dụng bo tròn khi form tải
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Bán kính góc bo tròn
            int cornerRadius = 30;

            // Tạo vùng bo tròn cho form
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(this.Width - cornerRadius - 1, 0, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(this.Width - cornerRadius - 1, this.Height - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(0, this.Height - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();

                // Áp dụng hình dạng bo tròn vào form
                this.Region = new Region(path);
            }
        }

        #endregion

        public frmLogin1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Khi người dùng nhấn vào textbox để nhập thì mất chữ hiện thị "Tên người chơi"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Tên người chơi")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.ForestGreen;
            }
        }

        /// <summary>
        /// Khi rời khỏi textName mà chưa nhập gì thì hiện lại "Tên người chơi"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                txtUserName.Text = "Tên người chơi";
                txtUserName.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// nhấn enter sau khi nhập tên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbLevel.Focus();
                e.SuppressKeyPress = true;  // Ngăn không cho nhấn Enter trong TextBox
            }
        }

        /// <summary>
        /// Xóa "Chọn cấp độ" và đổi font khi lựa chọn lần đầu
        /// </summary>
        bool isFirstSelection = true;

        /// <summary>
        /// Khi chọn Level thì xóa chữ chọn chế độ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFirstSelection && cmbLevel.SelectedIndex != 0)
            {
                cmbLevel.Font = new System.Drawing.Font("Algerian", 16F, FontStyle.Bold);
                cmbLevel.Items.RemoveAt(0); // Xóa "CHỌN CHẾ ĐỘ"
                isFirstSelection = false; // Đánh dấu đã chọn
            }
        }

        /// <summary>
        /// Chỉ click đăng nhập được khi đã nhập tên và chọn cấp độ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Tên người chơi")
            {
                MessageBox.Show("Vui lòng nhập tên người chơi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbLevel.Text == "CHỌN CẤP ĐỘ")
            {
                MessageBox.Show("Vui lòng chọn cấp độ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();

            int level = 0;
            if (cmbLevel.Text == "DÊ")              level = 1;
            else if (cmbLevel.Text == "TRUNG BÌNH") level = 2;
            else if (cmbLevel.Text == "KHÓ")        level = 3;
            else if (cmbLevel.Text == "CỰC KHÓ")    level = 4;
            else if (cmbLevel.Text == "SIÊU CẤP")   level = 5;

            frmChessBoard chessBoard = new frmChessBoard(txtUserName.Text, "Máy tính", 1, level); // Truyền tên qua constructor
            chessBoard.StartPosition = FormStartPosition.CenterScreen;
            chessBoard.ShowDialog();

        }

        /// <summary>
        /// Thoát form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

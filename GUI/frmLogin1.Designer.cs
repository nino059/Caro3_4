
namespace Caro3_4.GUI

{
    partial class frmLogin1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUserName = new TextBox();
            cmbLevel = new ComboBox();
            btnLogin = new Button();
            btnBack = new Button();
            lblLogin1 = new Label();
            SuspendLayout();
            // 
            // txtUserName
            // 
            txtUserName.BackColor = SystemColors.Window;
            txtUserName.Font = new Font("Algerian", 16F, FontStyle.Bold);
            txtUserName.ForeColor = Color.Gray;
            txtUserName.Location = new Point(109, 310);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(254, 50);
            txtUserName.TabIndex = 1;
            txtUserName.Text = "Tên người chơi";
            txtUserName.Enter += txtUserName_Enter;
            txtUserName.KeyDown += txtUserName_KeyDown;
            txtUserName.Leave += txtUserName_Leave;
            // 
            // cmbLevel
            // 
            cmbLevel.BackColor = SystemColors.Window;
            cmbLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLevel.FlatStyle = FlatStyle.Flat;
            cmbLevel.Font = new Font("Times New Roman", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbLevel.ForeColor = Color.ForestGreen;
            cmbLevel.Items.AddRange(new object[] { "CHỌN CẤP ĐỘ", "DÊ", "TRUNG BÌNH", "KHÓ", "CỰC KHÓ", "SIÊU CẤP" });
            cmbLevel.SelectedIndex = 0;
            cmbLevel.Location = new Point(109, 403);
            cmbLevel.Name = "cmbLevel";
            cmbLevel.Size = new Size(254, 43);
            cmbLevel.TabIndex = 2;
            cmbLevel.SelectedIndexChanged += cmbLevel_SelectedIndexChanged;
            // 
            // btnLogin
            // 
            btnLogin.AutoSize = true;
            btnLogin.Font = new Font("Algerian", 16F, FontStyle.Bold);
            btnLogin.ForeColor = Color.ForestGreen;
            btnLogin.Location = new Point(109, 493);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(254, 45);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "VÀO CHƠI";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnBack
            // 
            btnBack.AutoSize = true;
            btnBack.Font = new Font("Algerian", 16F, FontStyle.Bold);
            btnBack.ForeColor = Color.ForestGreen;
            btnBack.Location = new Point(109, 581);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(254, 45);
            btnBack.TabIndex = 6;
            btnBack.Text = "Thoát";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblLogin1
            // 
            lblLogin1.AutoSize = true;
            lblLogin1.BackColor = Color.Transparent;
            lblLogin1.Font = new Font("Algerian", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogin1.ForeColor = Color.Yellow;
            lblLogin1.Location = new Point(37, 141);
            lblLogin1.Name = "lblLogin1";
            lblLogin1.Size = new Size(426, 80);
            lblLogin1.TabIndex = 1;
            lblLogin1.Text = "ĐĂNG NHẬP";
            // 
            // frmLogin1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.loginGround;
            ClientSize = new Size(475, 754);
            Controls.Add(btnBack);
            Controls.Add(btnLogin);
            Controls.Add(cmbLevel);
            Controls.Add(txtUserName);
            Controls.Add(lblLogin1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmLogin1";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form1Player";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Button btnLogin;
        private Button btnBack;
        private Label lblLogin1;
    }
}

namespace Caro3_4.GUI
{
    partial class frmLogin2
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
            btnLogin = new Button();
            btnBack = new Button();
            txtUserName2 = new TextBox();
            txtUserName1 = new TextBox();
            lblLogin2 = new Label();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.AutoSize = true;
            btnLogin.Font = new Font("Algerian", 16F, FontStyle.Bold);
            btnLogin.ForeColor = Color.ForestGreen;
            btnLogin.Location = new Point(102, 461);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(282, 45);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "VÀO CHƠI";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnBack
            // 
            btnBack.AutoSize = true;
            btnBack.Font = new Font("Algerian", 16F, FontStyle.Bold);
            btnBack.ForeColor = Color.ForestGreen;
            btnBack.Location = new Point(102, 575);
            btnBack.Margin = new Padding(3, 4, 3, 4);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(282, 45);
            btnBack.TabIndex = 4;
            btnBack.Text = "Thoát";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // txtUserName2
            // 
            txtUserName2.BackColor = SystemColors.Window;
            txtUserName2.Font = new Font("Algerian", 16F, FontStyle.Bold);
            txtUserName2.ForeColor = Color.Gray;
            txtUserName2.Location = new Point(102, 348);
            txtUserName2.Name = "txtUserName2";
            txtUserName2.Size = new Size(282, 50);
            txtUserName2.TabIndex = 3;
            txtUserName2.Text = "Tên người chơi 2";
            txtUserName2.Enter += txtUserName2_Enter;
            txtUserName2.KeyDown += txtUserName2_KeyDown;
            txtUserName2.Leave += txtUserName2_Leave;
            // 
            // txtUserName1
            // 
            txtUserName1.BackColor = SystemColors.Window;
            txtUserName1.Font = new Font("Algerian", 16F, FontStyle.Bold);
            txtUserName1.ForeColor = Color.Gray;
            txtUserName1.Location = new Point(102, 233);
            txtUserName1.Name = "txtUserName1";
            txtUserName1.Size = new Size(282, 50);
            txtUserName1.TabIndex = 2;
            txtUserName1.Text = "Tên người chơi 1";
            txtUserName1.Enter += txtUserName1_Enter;
            txtUserName1.KeyDown += txtUserName1_KeyDown;
            txtUserName1.Leave += txtUserName1_Leave;
            // 
            // lblLogin2
            // 
            lblLogin2.AutoSize = true;
            lblLogin2.BackColor = Color.Transparent;
            lblLogin2.Font = new Font("Algerian", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLogin2.ForeColor = Color.Yellow;
            lblLogin2.Location = new Point(24, 70);
            lblLogin2.Name = "lblLogin2";
            lblLogin2.Size = new Size(426, 80);
            lblLogin2.TabIndex = 5;
            lblLogin2.Text = "ĐĂNG NHẬP";
            // 
            // frmLogin2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.loginGround;
            ClientSize = new Size(475, 754);
            Controls.Add(lblLogin2);
            Controls.Add(txtUserName1);
            Controls.Add(txtUserName2);
            Controls.Add(btnBack);
            Controls.Add(btnLogin);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmLogin2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form2Player";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Button btnBack;
        private TextBox txtUserName2;
        private TextBox txtUserName1;
        private Label lblLogin2;
    }
}
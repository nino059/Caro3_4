
namespace Caro3_4.GUI
{
    partial class frmStart
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
            lblNameGame = new Label();
            btnOnePlayer = new Button();
            btnTwoPlayer = new Button();
            lbl2Player = new Label();
            lbl1Player = new Label();
            btnRules = new Button();
            SuspendLayout();
            // 
            // lblNameGame
            // 
            lblNameGame.AutoSize = true;
            lblNameGame.BackColor = Color.Transparent;
            lblNameGame.Font = new Font("Algerian", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNameGame.ForeColor = Color.SpringGreen;
            lblNameGame.Location = new Point(29, 31);
            lblNameGame.Name = "lblNameGame";
            lblNameGame.Size = new Size(352, 63);
            lblNameGame.TabIndex = 0;
            lblNameGame.Text = "Game Caro";
            // 
            // btnOnePlayer
            // 
            btnOnePlayer.BackgroundImage = Properties.Resources._1Player;
            btnOnePlayer.BackgroundImageLayout = ImageLayout.Stretch;
            btnOnePlayer.Location = new Point(122, 166);
            btnOnePlayer.Margin = new Padding(3, 4, 3, 4);
            btnOnePlayer.Name = "btnOnePlayer";
            btnOnePlayer.Size = new Size(141, 186);
            btnOnePlayer.TabIndex = 2;
            btnOnePlayer.UseVisualStyleBackColor = true;
            btnOnePlayer.Click += btnOnePlayer_Click;
            // 
            // btnTwoPlayer
            // 
            btnTwoPlayer.BackgroundImage = Properties.Resources._2Player;
            btnTwoPlayer.BackgroundImageLayout = ImageLayout.Stretch;
            btnTwoPlayer.Location = new Point(316, 166);
            btnTwoPlayer.Margin = new Padding(3, 4, 3, 4);
            btnTwoPlayer.Name = "btnTwoPlayer";
            btnTwoPlayer.Size = new Size(141, 186);
            btnTwoPlayer.TabIndex = 3;
            btnTwoPlayer.UseVisualStyleBackColor = true;
            btnTwoPlayer.Click += btnTwoPlayer_Click;
            // 
            // lbl2Player
            // 
            lbl2Player.AutoSize = true;
            lbl2Player.BackColor = Color.Transparent;
            lbl2Player.Font = new Font("Algerian", 13F);
            lbl2Player.ForeColor = Color.SpringGreen;
            lbl2Player.Location = new Point(307, 390);
            lbl2Player.Name = "lbl2Player";
            lbl2Player.Size = new Size(165, 29);
            lbl2Player.TabIndex = 4;
            lbl2Player.Text = "2 người chơi";
            // 
            // lbl1Player
            // 
            lbl1Player.AutoSize = true;
            lbl1Player.BackColor = Color.Transparent;
            lbl1Player.Font = new Font("Algerian", 13F);
            lbl1Player.ForeColor = Color.SpringGreen;
            lbl1Player.Location = new Point(112, 390);
            lbl1Player.Name = "lbl1Player";
            lbl1Player.Size = new Size(165, 29);
            lbl1Player.TabIndex = 5;
            lbl1Player.Text = "1 người chơi";
            // 
            // btnRules
            // 
            btnRules.AutoSize = true;
            btnRules.BackColor = Color.SkyBlue;
            btnRules.BackgroundImageLayout = ImageLayout.Stretch;
            btnRules.Font = new Font("Algerian", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRules.ForeColor = Color.Black;
            btnRules.Location = new Point(556, 166);
            btnRules.Margin = new Padding(3, 4, 3, 4);
            btnRules.Name = "btnRules";
            btnRules.Size = new Size(228, 76);
            btnRules.TabIndex = 6;
            btnRules.Text = "LUẬT CHƠI";
            btnRules.UseVisualStyleBackColor = false;
            // 
            // frmStart
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuHighlight;
            BackgroundImage = Properties.Resources.startGround;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(889, 562);
            Controls.Add(btnRules);
            Controls.Add(lbl1Player);
            Controls.Add(lbl2Player);
            Controls.Add(btnTwoPlayer);
            Controls.Add(btnOnePlayer);
            Controls.Add(lblNameGame);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmStart";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblNameGame;
        private System.Windows.Forms.Button btnOnePlayer;
        private System.Windows.Forms.Button btnTwoPlayer;
        private Label lbl2Player;
        private Label lbl1Player;
        private Button btnRules;
    }
}
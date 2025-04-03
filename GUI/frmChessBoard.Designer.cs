
namespace Caro3_4.GUI
{
    partial class frmChessBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChessBoard));
            pnlChessBoard = new Panel();
            picChess = new Panel();
            picAvatar = new PictureBox();
            panel3 = new Panel();
            label1 = new Label();
            picbMark = new PictureBox();
            txbPlayerName = new TextBox();
            menuStrip1 = new MenuStrip();
            mnuChessBoard = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            proTimer = new ProgressBar();
            picChess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picbMark).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlChessBoard.BackColor = SystemColors.Control;
            pnlChessBoard.Location = new Point(14, 39);
            pnlChessBoard.Margin = new Padding(5, 6, 5, 6);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(855, 875);
            pnlChessBoard.TabIndex = 0;
            // 
            // picChess
            // 
            picChess.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picChess.Controls.Add(picAvatar);
            picChess.Location = new Point(920, 39);
            picChess.Margin = new Padding(5, 6, 5, 6);
            picChess.Name = "picChess";
            picChess.Size = new Size(485, 519);
            picChess.TabIndex = 1;
            // 
            // picAvatar
            // 
            picAvatar.BackColor = SystemColors.Control;
            picAvatar.BackgroundImageLayout = ImageLayout.Stretch;
            picAvatar.Location = new Point(-5, 53);
            picAvatar.Margin = new Padding(5, 6, 5, 6);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(485, 519);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            picAvatar.TabIndex = 0;
            picAvatar.TabStop = false;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Right;
            panel3.BackColor = SystemColors.Control;
            panel3.Controls.Add(label1);
            panel3.Controls.Add(picbMark);
            panel3.Controls.Add(txbPlayerName);
            panel3.Location = new Point(915, 623);
            panel3.Margin = new Padding(5, 6, 5, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(490, 347);
            panel3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Mistral", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(112, 437);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(255, 38);
            label1.TabIndex = 5;
            label1.Text = "Five in a line to win";
            // 
            // picbMark
            // 
            picbMark.BackColor = SystemColors.Control;
            picbMark.Location = new Point(3, 6);
            picbMark.Margin = new Padding(5, 6, 5, 6);
            picbMark.Name = "picbMark";
            picbMark.Size = new Size(482, 344);
            picbMark.SizeMode = PictureBoxSizeMode.StretchImage;
            picbMark.TabIndex = 2;
            picbMark.TabStop = false;
            // 
            // txbPlayerName
            // 
            txbPlayerName.Location = new Point(38, 375);
            txbPlayerName.Margin = new Padding(5, 6, 5, 6);
            txbPlayerName.Name = "txbPlayerName";
            txbPlayerName.ReadOnly = true;
            txbPlayerName.Size = new Size(182, 31);
            txbPlayerName.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuChessBoard });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(1607, 33);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuChessBoard
            // 
            mnuChessBoard.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, undoToolStripMenuItem, quitToolStripMenuItem });
            mnuChessBoard.Name = "mnuChessBoard";
            mnuChessBoard.Size = new Size(73, 29);
            mnuChessBoard.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newGameToolStripMenuItem.Size = new Size(263, 34);
            newGameToolStripMenuItem.Text = "New game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(263, 34);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            quitToolStripMenuItem.Size = new Size(263, 34);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // proTimer
            // 
            proTimer.Location = new Point(918, 580);
            proTimer.Maximum = 15000;
            proTimer.Name = "proTimer";
            proTimer.Size = new Size(314, 34);
            proTimer.Step = 1;
            proTimer.Style = ProgressBarStyle.Continuous;
            proTimer.TabIndex = 7;
            // 
            // frmChessBoard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1607, 1050);
            Controls.Add(proTimer);
            Controls.Add(panel3);
            Controls.Add(picChess);
            Controls.Add(pnlChessBoard);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(5, 6, 5, 6);
            MaximizeBox = false;
            Name = "frmChessBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caro PvB";
            FormClosing += Form1_FormClosing;
            picChess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picbMark).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Panel picChess;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picbMark;
        private System.Windows.Forms.TextBox txbPlayerName;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuChessBoard;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private ProgressBar proTimer;
    }
}


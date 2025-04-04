
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
            pnlPlayer2Name = new Panel();
            txbPlayer2Name = new TextBox();
            pictureBox1 = new PictureBox();
            pnlPlayer1Info = new Panel();
            proTimer = new ProgressBar();
            picbMark1 = new PictureBox();
            txbPlayer1Name = new TextBox();
            menuStrip1 = new MenuStrip();
            mnuChessBoard = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            quitToolStripMenuItem = new ToolStripMenuItem();
            pnlPlayer2Name.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlPlayer1Info.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picbMark1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlChessBoard
            // 
            pnlChessBoard.BackColor = Color.Khaki;
            pnlChessBoard.Location = new Point(319, 28);
            pnlChessBoard.Margin = new Padding(4, 5, 4, 5);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(801, 801);
            pnlChessBoard.TabIndex = 0;
            // 
            // pnlPlayer2Name
            // 
            pnlPlayer2Name.BackColor = Color.Transparent;
            pnlPlayer2Name.Controls.Add(txbPlayer2Name);
            pnlPlayer2Name.Controls.Add(pictureBox1);
            pnlPlayer2Name.Dock = DockStyle.Right;
            pnlPlayer2Name.Location = new Point(1122, 28);
            pnlPlayer2Name.Margin = new Padding(4, 5, 4, 5);
            pnlPlayer2Name.Name = "pnlPlayer2Name";
            pnlPlayer2Name.Size = new Size(314, 800);
            pnlPlayer2Name.TabIndex = 1;
            // 
            // txbPlayer2Name
            // 
            txbPlayer2Name.Location = new Point(51, 318);
            txbPlayer2Name.Margin = new Padding(4, 5, 4, 5);
            txbPlayer2Name.Name = "txbPlayer2Name";
            txbPlayer2Name.ReadOnly = true;
            txbPlayer2Name.Size = new Size(218, 27);
            txbPlayer2Name.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.Control;
            pictureBox1.Location = new Point(51, 49);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(218, 207);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pnlPlayer1Info
            // 
            pnlPlayer1Info.BackColor = Color.Transparent;
            pnlPlayer1Info.Controls.Add(proTimer);
            pnlPlayer1Info.Controls.Add(picbMark1);
            pnlPlayer1Info.Controls.Add(txbPlayer1Name);
            pnlPlayer1Info.Dock = DockStyle.Left;
            pnlPlayer1Info.Location = new Point(0, 28);
            pnlPlayer1Info.Margin = new Padding(4, 5, 4, 3);
            pnlPlayer1Info.Name = "pnlPlayer1Info";
            pnlPlayer1Info.Size = new Size(317, 800);
            pnlPlayer1Info.TabIndex = 2;
            // 
            // proTimer
            // 
            proTimer.Location = new Point(51, 273);
            proTimer.Margin = new Padding(2);
            proTimer.Maximum = 15000;
            proTimer.Name = "proTimer";
            proTimer.Size = new Size(218, 27);
            proTimer.Step = 1;
            proTimer.Style = ProgressBarStyle.Continuous;
            proTimer.TabIndex = 7;
            // 
            // picbMark1
            // 
            picbMark1.BackColor = SystemColors.Control;
            picbMark1.Location = new Point(51, 49);
            picbMark1.Margin = new Padding(4, 5, 4, 5);
            picbMark1.Name = "picbMark1";
            picbMark1.Size = new Size(218, 207);
            picbMark1.SizeMode = PictureBoxSizeMode.StretchImage;
            picbMark1.TabIndex = 2;
            picbMark1.TabStop = false;
            // 
            // txbPlayer1Name
            // 
            txbPlayer1Name.Location = new Point(51, 318);
            txbPlayer1Name.Margin = new Padding(4, 5, 4, 5);
            txbPlayer1Name.Name = "txbPlayer1Name";
            txbPlayer1Name.ReadOnly = true;
            txbPlayer1Name.Size = new Size(218, 27);
            txbPlayer1Name.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuChessBoard });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1436, 28);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuChessBoard
            // 
            mnuChessBoard.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, undoToolStripMenuItem, quitToolStripMenuItem });
            mnuChessBoard.Name = "mnuChessBoard";
            mnuChessBoard.Size = new Size(60, 24);
            mnuChessBoard.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newGameToolStripMenuItem.Size = new Size(217, 26);
            newGameToolStripMenuItem.Text = "New game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(217, 26);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // quitToolStripMenuItem
            // 
            quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            quitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            quitToolStripMenuItem.Size = new Size(217, 26);
            quitToolStripMenuItem.Text = "Quit";
            quitToolStripMenuItem.Click += quitToolStripMenuItem_Click;
            // 
            // frmChessBoard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.theem1;
            ClientSize = new Size(1436, 828);
            Controls.Add(pnlPlayer2Name);
            Controls.Add(pnlPlayer1Info);
            Controls.Add(pnlChessBoard);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "frmChessBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caro PvB";
            FormClosing += Form1_FormClosing;
            Load += frmChessBoard_Load;
            pnlPlayer2Name.ResumeLayout(false);
            pnlPlayer2Name.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlPlayer1Info.ResumeLayout(false);
            pnlPlayer1Info.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picbMark1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pnlChessBoard;
        private System.Windows.Forms.Panel pnlPlayer2Name;
        private System.Windows.Forms.Panel pnlPlayer1Info;
        private System.Windows.Forms.PictureBox picbMark1;
        private System.Windows.Forms.TextBox txbPlayer1Name;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuChessBoard;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private ProgressBar proTimer;
        private TextBox txbPlayer2Name;
        private PictureBox pictureBox1;
    }
}


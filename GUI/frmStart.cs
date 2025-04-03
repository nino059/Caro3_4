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
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
        }

        private void btnOnePlayer_Click(object sender, EventArgs e)
        {
            frmLogin1 form1Player = new frmLogin1();
            form1Player.ShowDialog();
        }

        private void btnTwoPlayer_Click(object sender, EventArgs e)
        {
            frmLogin2 form2Player = new frmLogin2();
            form2Player.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TienLen
{
    public partial class Begin : Form
    {
        public Begin()
        {
            InitializeComponent();
        }

        private void btnplay_Click(object sender, EventArgs e)
        {
            Play p = new Play();
            p.Show();
            Visible = false;
        }

        private void btnrule_Click(object sender, EventArgs e)
        {
            Rule r = new Rule();
            r.ShowDialog();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Begin_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

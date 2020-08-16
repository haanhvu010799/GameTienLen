using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TienLen
{
    public partial class SoNguoiChoi : Form
    {
        public SoNguoiChoi()
        {
            InitializeComponent();
        }

        private void SoNguoiChoi_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Play p = new Play();
            p.ShowDialog();

            if (radioButton1.Checked)
            {

                _Mode = 2;
            }
            else if (radioButton2.Checked)
            {
                _Mode = 3;

            }
            else if (radioButton3.Checked)
            {

                _Mode = 4;
            }
            Thread str = new Thread(StartMulti);
            str.SetApartmentState(ApartmentState.STA);
            str.Start();
            //Thread thread = new Thread(MultiTable);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton1.ForeColor = Color.Orange;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton2.ForeColor = Color.Green;
            radioButton1.Checked = false;
            radioButton3.Checked = false;
        }
        private void radioButton3_Click(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
            radioButton3.ForeColor = Color.Magenta;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

    }
}

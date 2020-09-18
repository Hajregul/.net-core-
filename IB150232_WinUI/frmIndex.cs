using IB150232_WinUI.f_Korisnici;
using IB150232_WinUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IB150232_WinUI
{
    public partial class frmIndex : Form
    {
        private int childFormNumber = 0;

        public frmIndex()
        {
            InitializeComponent();
        }


        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private bool isCollapsed;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                button5.Image = Resources.icons8_expand_arrow_48;
                panelDropDovnKorisnici.Height += 10;
                if (panelDropDovnKorisnici.Size == panelDropDovnKorisnici.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                button5.Image = Resources.icons8_expand_arrow_48;
                panelDropDovnKorisnici.Height -= 10;
                if (panelDropDovnKorisnici.Size == panelDropDovnKorisnici.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            frmKorisnici form1 = new frmKorisnici();
            form1.Show();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NoviKorisnik_Click(object sender, EventArgs e)
        {
            frmAdd form1 = new frmAdd();
            form1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}

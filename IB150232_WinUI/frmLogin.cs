using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150232_WinUI
{
    public partial class frmLogin : Form
    {
        APIService _service = new APIService("Korisnici"); 
        public frmLogin()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
           APIService.Username = txtUsername.Text;
           APIService.Password = txtPassword.Text;
            try
            {
              var d=  await _service.Get<dynamic>(null);
                frmIndex frm = new frmIndex();
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Autentifikacija", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

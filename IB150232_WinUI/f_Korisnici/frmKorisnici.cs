using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;
using Flurl;
using IB150232_Model;
using IB150232_Model.Request;

namespace IB150232_WinUI.f_Korisnici
{
    public partial class frmKorisnici : Form
    {
        private readonly APIService _apiService = new APIService("Korisnici");
        public frmKorisnici()
        {
            InitializeComponent();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            var search = new KorisniciSearchRequest()
            {
                Ime = textBox1.Text
            };
            var result = await _apiService.Get<List<Korisnici>>(search);
            dataGridView1.DataSource = result;
            dataGridView1.Columns[9].Visible = false;

        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView1.SelectedRows[0].Cells[1].Value;
            int _id = Convert.ToInt32(id);
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Da li ste sigurni da želite obrisati ovaj zapis ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var result = await _apiService.Delete<Korisnici>(_id);
                    if (result != null)
                    {
                        MessageBox.Show("Uspješno obrisan korisnik!");
                    }
                }

            }
            else
            {
                frmDetails form1 = new frmDetails(_id);
                form1.Show();
            }
        }

        private void frmKorisnici_Load(object sender, EventArgs e)
        {

        }
    }
}

using IB150232_Model;
using IB150232_Model.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IB150232_WinUI.f_Korisnici
{
    public partial class frmAdd : Form
    {
        private readonly APIService service = new APIService("Korisnici");
        APIService _ulogeService = new APIService("Uloge");
        APIService _korisniciUlogeService = new APIService("KorisniciUloge");
        Uloge selectedValueUloge;
        IB150232_Model.Korisnici korisnik { get; set; }
        public frmAdd()
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                var request = new KorisniciInsertRequest()
                {
                    Adresa = txtAdresa.Text,
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    KorisnickoIme = txtKorisnickoime.Text,
                    Telefon = txttelefon.Text,
                    Mail = txtMail.Text,
                    Password = txtLozinka.Text,
                    PasswordConfirm = txtPotvrda.Text,
                    Status = true
                };
                IB150232_Model.Korisnici entity = null;
                IB150232_Model.KorisniciUloge entity2 = null;
                if (request != null)
                {
                    entity = await service.Insert<IB150232_Model.Korisnici>(request);
                    SendMail();
                }
                var result = await service.Get<List<Korisnici>>(null);
                korisnik = result.LastOrDefault();
                foreach (Uloge item in clbRole.CheckedItems)
                {
                    selectedValueUloge = item;
                }
                var request2 = new KorisniciUlogeInsert()
                {
                    DatumIzmjene = DateTime.Now,
                    UlogaId = selectedValueUloge.UlogaId,
                    KorisnikId = korisnik.KorisnikId,
                    Korisnik = null,
                    Uloga = null
                };

                if (request2 != null)
                {
                    entity2 = await _korisniciUlogeService.Insert<IB150232_Model.KorisniciUloge>(request2);
                }
                if (entity != null && entity != null)
                {
                    MessageBox.Show("Uspješno izvršeno");
                    Clean();
                }
            }
        }

        private void Clean()
        {
            txtMail.Text = txtLozinka.Text = txtPotvrda.Text = txtKorisnickoime.Text = txtIme.Text = txtAdresa.Text = txtPrezime.Text = txttelefon.Text = "";
        
            for (int i =0;i< clbRole.CheckedItems.Count;i++)
            {
                clbRole.SetItemChecked(i,false);
            }
            
        }

        private void SendMail()
        {
            try
            {
                string fromaddr = "seminarskirs2@gmail.com";

                string password = "seminarskiRs2!";

                MailMessage msg = new MailMessage();
                msg.Subject = "Pristupni podaci";
                msg.From = new MailAddress(fromaddr, "eRent a flat Sarajevo");
                msg.Body = GetFormattedMessageHTML();
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.To.Add(new MailAddress(txtMail.Text.Trim()));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential nc = new NetworkCredential(fromaddr, password);

                smtp.Credentials = nc;
                smtp.EnableSsl = true;
                smtp.Send(msg);
                MessageBox.Show("E-mail uspješno poslan!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetFormattedMessageHTML()
        {
            return "<!DOCTYPE html> " +
               "<head>" +
                   "<title>Email</title>" +
               "</head>" +
               "<body style=\"font-family:'Century Gothic'\">" +
                   "<h1 style=\"text-align:center;\"> " + "Podrška korisnicima" + "</h1>" +
                   "<p>" + "Poštovani u nastavku mail-a nalaze se pristupni podaci za aplikaciju eRent:" + "</p>" +
                   "<h2 style=\"font-size:14px;\">" +
                       "Korisnicko ime : " + txtKorisnickoime.Text.Trim() + "<br />" +
                       "Lozinka : " + txtLozinka.Text.Trim() + "<br />" +
                   "</h2>" +
               "</body>" +
               "</html>";
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtIme,Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtIme, null);
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPrezime, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtPrezime, null);
            }
        }

        private void txtAdresa_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAdresa.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtAdresa, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtAdresa, null);
            }
        }

        private void txttelefon_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txttelefon.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txttelefon, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txttelefon, null);
            }
        }

        private void txtMail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtMail, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtMail, null);
            }
        }

        private void txtKorisnickoime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKorisnickoime.Text) || txtKorisnickoime.Text.Length < 3)
            {
                e.Cancel = true;
                errorProvider.SetError(txtKorisnickoime, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtKorisnickoime, null);
            }
        }

        private void txtLozinka_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtLozinka, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtLozinka, null);
            }
        }

        private void txtPotvrda_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPotvrda.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPotvrda, Properties.Resources.Validation_RequiredField);
            }
            else
            {
                errorProvider.SetError(txtPotvrda, null);
            }
        }

        private async void frmAdd_Load(object sender, EventArgs e)
        {
            
            var ulogeList = await _ulogeService.Get<List<IB150232_Model.Uloge>>(null);

            clbRole.DataSource = ulogeList;
            clbRole.DisplayMember = "Naziv";
        }
    }
}

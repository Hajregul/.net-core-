using IB150232_Model.Request;
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

using System.Net;
using System.Net.Mail;
using MimeKit;
using MimeKit.Text;
using IB150232_Model;
using System.Security.Authentication;

namespace IB150232_WinUI.f_Korisnici
{
    public partial class frmDetails : Form
    {
        private readonly APIService service = new APIService("Korisnici");
        APIService _ulogeService = new APIService("Uloge");
        APIService _korisniciUlogeService = new APIService("KorisniciUloge");
        APIService _korisniciUlogeInsertService = new APIService("BaseCRUDController");
        private int? _id = null;
        Uloge selectedValueUloge;
        int d = 0;
        IB150232_Model.KorisniciUloge korisnikUloga { get; set; }
        string postojeciMail;
        IB150232_Model.Korisnici korisnik { get; set; }
        public frmDetails(int? id = null)
        {
            InitializeComponent();
            this.AutoValidate = AutoValidate.Disable;
            _id = id;
        }

        private async void frmDetails_Load(object sender, EventArgs e)
        {
            var ulogeList = await _ulogeService.Get<List<IB150232_Model.Uloge>>(null);

            clbRole.DataSource = ulogeList;
            clbRole.DisplayMember = "Naziv";
            if (_id.HasValue)
            {
                korisnik = await service.GetById<IB150232_Model.Korisnici>(_id);
                korisnikUloga = await _korisniciUlogeService.GetById<IB150232_Model.KorisniciUloge>(_id);
                txtAdresa.Text = korisnik.Adresa;
                txtIme.Text = korisnik.Ime;
                txtKorisnickoime.Text = korisnik.KorisnickoIme;
                txtMail.Text = korisnik.Mail;
                txtPrezime.Text = korisnik.Prezime;
                txttelefon.Text = korisnik.Telefon;
                txtLozinka.Text = "";
                txtPotvrda.Text = "";
                postojeciMail = korisnik.Mail;
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPrezime, "Obavezno polje");
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
                errorProvider.SetError(txtAdresa, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtAdresa, null);
            }
        }

        private void txttelefon_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtMail, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtMail, null);
            }
        }

        private void txtMail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtMail, "Obavezno polje");
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
                errorProvider.SetError(txtKorisnickoime, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtKorisnickoime, null);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                var roleList = clbRole.CheckedItems.Cast<IB150232_Model.Uloge>().Select(x => x.UlogaId).ToList();

                var request = new KorisniciInsertRequest()
                {
                    Adresa = txtAdresa.Text,
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    KorisnickoIme = txtKorisnickoime.Text,
                    Telefon = txtMail.Text,
                    Mail = txtMail.Text,
                    Password = txtLozinka.Text,
                    PasswordConfirm = txtPotvrda.Text,
                    Status = true,
                    Uloge = roleList
                };
          
            
                foreach (Uloge item in clbRole.CheckedItems)
                {
                    selectedValueUloge = item;
                }
                var request2 = new KorisniciUlogeInsert()
                {
                    DatumIzmjene = DateTime.Now,
                    UlogaId = selectedValueUloge.UlogaId,
                    KorisnikId = korisnik.KorisnikId,
                    Korisnik = korisnik,
                    Uloga=selectedValueUloge                
            };
                IB150232_Model.Korisnici entity = null;
                IB150232_Model.KorisniciUloge entity2 = null;

                if (_id.HasValue)
                {
                  entity=  await service.Update<IB150232_Model.Korisnici>(Convert.ToInt32(_id), request);
                    korisnikUloga.UlogaId = selectedValueUloge.UlogaId;
                    entity2 = await _korisniciUlogeService.Update<IB150232_Model.KorisniciUloge>(korisnikUloga.KorisnikUlogaId,korisnikUloga);

                    if (postojeciMail != txtMail.Text)
                    {
                        SendMail();
                    }

                }
                else
                {
                    entity = await service.Insert<IB150232_Model.Korisnici>(request);
                    entity2= await _korisniciUlogeService.Insert<IB150232_Model.KorisniciUloge>(request2);
                }
                if (entity != null && entity != null)
                {
                    MessageBox.Show("Uspješno izvršeno");
                    this.Close();
                }
            }
        }

        private   void SendMail()
        {
            try
            {
                string fromaddr = "seminarskirs2@gmail.com";
                
                string password = "seminarskiRs2!";

                MailMessage msg = new MailMessage();
                msg.Subject = "Pristupni podaci";
                msg.From = new MailAddress(fromaddr,"eRent a flat Sarajevo");
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
        private String GetFormattedMessageHTML()
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

        private void txtLozinka_TextChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtLozinka, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtLozinka, null);
            }
        }

        private void txtPotvrda_TextChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPotvrda.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPotvrda, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtPotvrda, null);
            }
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtIme, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtIme, null);
            }
        }

        private void txtLozinka_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtLozinka, "Obavezno polje");
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
                errorProvider.SetError(txtPotvrda, "Obavezno polje");
            }
            else
            {
                errorProvider.SetError(txtPotvrda, null);
            }
        }

        private void clbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
        int iSelectedIndex = clbRole.SelectedIndex;
            for (int iIndex = 0; iIndex < clbRole.Items.Count; iIndex++)
                clbRole.SetItemCheckState(iIndex, CheckState.Unchecked);
            if (iSelectedIndex == -1)
                return;
            for (int iIndex = 0; iIndex < clbRole.Items.Count; iIndex++)
                clbRole.SetItemCheckState(iIndex, CheckState.Unchecked);
            clbRole.SetItemCheckState(iSelectedIndex, CheckState.Checked);
        }
    }


    
}
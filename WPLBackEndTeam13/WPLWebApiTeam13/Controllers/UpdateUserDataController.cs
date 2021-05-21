using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using Newtonsoft.Json;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using System.Net;
using System.Net.Mail;
using System.Data;

namespace WPLWebApiTeam13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateUserDataController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryUserData qls = new QueryUserData();
            return JsonConvert.SerializeObject(qls.GetData().DataTable);
        }
        [HttpPost]
        public string update([FromForm] UpdateDataForm form)
        {
            bool isWachtwoordVeranderd = false;
            UserData User = new UserData();
            User.KlantNummer = form.Klantnummer;
            User.Naam = form.Naam;
            User.VoorNaam = form.Voornaam;
            User.EmailAdres = form.Emailadres;
            User.Telefoon = form.Telefoon;
            User.StraatNaam = form.Straatnaam;
            User.HuisNummer = form.Huisnummer;
            User.PostCode = form.Postcode;
            //User.Plaats = form.Plaats;
            //User.Land = form.Land;
            User.Aangemaakt = form.Aangemaakt;
            User.Wachtwoord = form.Wachtwoord;
            QueryUserData qls = new QueryUserData();
            DataTable rows = qls.getID(Convert.ToInt32(User.KlantNummer));
            foreach (DataRow row in rows.Rows)
            {
                if (row["Wachtwoord"].ToString() != form.Wachtwoord)
                {
                    isWachtwoordVeranderd = true;
                    break;
                }
            }
            User.Update();

            if (isWachtwoordVeranderd)
            {
                StuurMail(form.Emailadres, form.Naam);
            }

            //get new gegevens
            QueryUserData ql = new QueryUserData();
            var result = ql.GetData(form.Emailadres, form.Wachtwoord);
            //vermijden dat de wachtwoord word gestuurd naar front end
            DataTable resultDt = result.DataTable;
            foreach (DataRow row in resultDt.Rows)
            {
                row["wachtwoord"] = "";
            }
            return JsonConvert.SerializeObject(resultDt);
        }
        public void StuurMail(string ToEmail, string NaamGebruiker)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("belgietechnologie@gmail.com");
                mail.To.Add(ToEmail);
                mail.Subject = "wachtwoord";
                mail.Body = $"<div style='background-color: rgba(0,0,0,.8); font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;' >" +
                    $"<h1 style='color:#44d62c;' >BELTECH</h1>" +
                    $"<div style='color: #999'>" +
                    $"<h3> Beste mijnheer,mevrouw <span style ='text-transform: capitalize;' >{ NaamGebruiker}</span></h3>" +
                    $"<p> Je hebt uw wachtwoord veranderd in geval dat u die actie niet hebt gedaan" +
                    $"contcateer ons zo snel mogelijke via " +
                    $"<a href='#'>here</a></p> " +
                    $"</div>" +
                    $"</div>";
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("belgietechnologie@gmail.com", "belgieTechnologie5");
                    smtp.Send(mail);
                }
            }
        }
    }
    public class UpdateDataForm
    {
        public int Klantnummer { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Emailadres { get; set; }
        public string Telefoon { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }
        public string Land { get; set; }
        public string Wachtwoord { get; set; }
        public DateTime Aangemaakt { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using Newtonsoft.Json;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using System.Data;

namespace WPLWebApiTeam13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserdataController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryUserData ql = new QueryUserData();
            return JsonConvert.SerializeObject(ql.GetData().DataTable);;
        }
        [HttpPost]
        public string Create([FromForm] FormUserdataObject form)
        {
            UserData User = new UserData();

            User.Naam = form.Naam;
            User.VoorNaam = form.Voornaam;
            User.EmailAdres = form.Emailadres;
            User.Telefoon = form.Telefoon;
            User.StraatNaam = form.Straatnaam;
            User.HuisNummer = form.Huisnummer;
            User.PostCode = form.Postcode;
            //User.Plaats = form.Plaats;
            //User.Land = form.Land;
            User.Aangemaakt = DateTime.Now;
            User.Wachtwoord = form.Wachtwoord;

            var resultInsert = User.Insert();
            if (resultInsert.Count == -1)
            {
                return JsonConvert.SerializeObject("");
            }
            else
            {
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
        }
    }
    public class FormUserdataObject
    {
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
    }
}

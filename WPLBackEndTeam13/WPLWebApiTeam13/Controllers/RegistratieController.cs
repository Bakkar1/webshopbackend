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
    public class RegistratieController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryUserData qls = new QueryUserData();
            return JsonConvert.SerializeObject(qls.GetData().DataTable);
        }
        [HttpPost]
        public string PostRegistratie([FromForm] RegistratieFoorm form)
        {

            UserData user = new UserData();
            user.EmailAdres = form.Emailadres;
            user.Wachtwoord = form.Wachtwoord;
            user.Aangemaakt = DateTime.Now;

            var resultInsert =  user.Insert();
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
    public class RegistratieFoorm
    {
        public string Emailadres { get; set; }
        public string Wachtwoord { get; set; }
        public string Aangemaakt { get; set; }

    }
}

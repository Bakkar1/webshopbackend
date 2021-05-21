using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using Newtonsoft.Json;
using System.Data;
using WPLClassLibraryTeam13.Query;

namespace WPLWebApiTeam13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryUserData ql = new QueryUserData();
            var result = ql.GetData();
            return JsonConvert.SerializeObject(result.DataTable);
        }
        [HttpPost]
        public string GetLogin([FromForm] FormLogin form)
        {
            QueryUserData ql = new QueryUserData();
            var result = ql.GetData(form.Emailadres, form.Wachtwoord);
            //vermijden dat de wachtwoord word gestuurd naar front end
            DataTable resultDt  = result.DataTable;
            foreach(DataRow row in resultDt.Rows)
            {
                row["wachtwoord"]= "";
            }
            return JsonConvert.SerializeObject(resultDt);
        }
        public class FormUserDataObject
        {
            public string UserMail { get; set; }
            public string UserPassword { get; set; }
        }
    }
    public class FormLogin
    {
        public string Emailadres { get; set; }
        public string Wachtwoord { get; set; }

    }
}


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using Newtonsoft.Json;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Entity;
using System.Data;

namespace WPLWebApiTeam13.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuisController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryMuis ql = new QueryMuis();
            return JsonConvert.SerializeObject(ql.getJoinData());
        }
        [HttpPost]
        public string post([FromBody] MuisForm form)
        {
            QueryMuis ql = new QueryMuis();
            int id = Convert.ToInt32(form.Id);
            return JsonConvert.SerializeObject(ql.getSingelJoin(id));
        }
    }
    public class MuisForm
    {
        public string Id { get; set; }
    }
}

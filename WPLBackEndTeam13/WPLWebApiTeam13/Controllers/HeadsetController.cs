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
    public class HeadsetController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryHeadset ql = new QueryHeadset();
            return JsonConvert.SerializeObject(ql.getJoinData());
        }
        [HttpPost]
        public string post([FromBody] HeadsetForm form)
        {
            QueryHeadset ql = new QueryHeadset();
            int id = Convert.ToInt32(form.Id);
            return JsonConvert.SerializeObject(ql.getSingelJoin(id));
        }
    }
    public class HeadsetForm
    {
        public string Id { get; set; }
    }
}

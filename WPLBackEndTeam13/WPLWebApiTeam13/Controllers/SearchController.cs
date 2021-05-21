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
    public class SearchController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryProductDetails qrProduct = new QueryProductDetails();
            return JsonConvert.SerializeObject(qrProduct.getJoinData());
        }
        [HttpPost]
        public string post([FromBody] FormSeach form)
        {
            QueryProductDetails ql = new QueryProductDetails();
            return JsonConvert.SerializeObject(ql.GetDataSearch(form.SeatchVeld));
        }
    }
    public class FormSeach
    {
        public string SeatchVeld { get; set; }

    }
}

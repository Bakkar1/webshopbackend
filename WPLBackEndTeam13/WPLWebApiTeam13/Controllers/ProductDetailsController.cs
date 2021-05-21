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
    public class ProductDetailsController : Controller
    {
        [HttpGet]
        public string Get()
        {
            QueryProductDetails qrProduct = new QueryProductDetails();
            return JsonConvert.SerializeObject(qrProduct.getJoinData());
        }
        [HttpPost]
        public string post([FromBody] FormId form)
        {
            QueryProductDetails ql = new QueryProductDetails();
            int id = Convert.ToInt32(form.id);
            return JsonConvert.SerializeObject(ql.getJoinData(id));
        }
        public class FormId
        {
            public string id { get; set; }

        }
    }
}

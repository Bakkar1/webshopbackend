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
    public class OrdersController : Controller
    {
        [HttpGet]
        public string GetOrders()
        {
            QueryOrders qlOrders = new QueryOrders();
            return JsonConvert.SerializeObject(qlOrders.getJoinData());
        }
        [HttpPost]
        public string post([FromBody] OrdersForm form)
        {
            Orders order = new Orders();
            int id = Convert.ToInt32(form.Id);
            order.KlantNummer = id;
            order.OrderDatum = DateTime.Now;
            order.StraatNaam = form.StraatNaam;
            order.Postcode = form.Postcode;
            order.LeveringMethod = form.LeveringMethod;
            order.LeveringOpmerking = form.LeveringOpmerking;
            order.OrderStatus = "Pending";

            order.Insert();


            QueryOrders qlOrders = new QueryOrders();
            return JsonConvert.SerializeObject(qlOrders.getJoinData(id));
        }
    }
    public class OrdersForm
    {
        public string Id { get; set; }
        public string StraatNaam { get; set; }
        public string Postcode { get; set; }
        public string LeveringMethod { get; set; }
        public string LeveringOpmerking { get; set; }
        public string OrderStatus { get; set; }
        
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Query;
using Newtonsoft.Json;
using WPLClassLibraryTeam13.Entity;
using System.Data;

namespace WPLWebApiTeam13.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class WinkelwagenController : Controller
    {
       
        [HttpGet]

        public string Get()
        {
            Productdetails pd = new Productdetails();
            SQLCommandResult result = pd.Read();
            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(result.DataTable);
            return JsonString;
        }

        [HttpPost]

        
        public string GetOrderedProducts([FromBody] string subst)
        {
            // Er komt een string met de bestelde productId's binnen via de Json Body, (id1, id2, id5,...)
            // die wordt omgezet in een string in de parameter
            // string wordt in array gezet met split > loop over array, omzetten naar int en in list zetten
            // list meegeven met GetDataForWinkelmand(list) > hier komt list van DataTables van terug
            // list van DataTables omzetten naar Json en terugsturen naar Front-End

            string[] prod = subst.Split('|');
            List<int> ids = new List<int>();
            foreach (string item in prod)
            {
                if (item != string.Empty)
                {
                    ids.Add(Convert.ToInt32(item));
                }
                
            }
            List<DataTable> dt = new List<DataTable>();
            QueryWinkelmand qwm = new QueryWinkelmand();
            dt = qwm.GetProductsForWinkelmand(ids);
            
            
            return JsonConvert.SerializeObject(dt);
        }

        


    }
}

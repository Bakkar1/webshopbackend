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
    public class TestWinkelController : Controller
    {
        [HttpGet]
        //get

        [HttpPost]

        public IActionResult PostProductGegevens([FromBody] WinkelMandje mandje)
        {

            return Ok();
        }

    }
    public class WinkelMandje
    {
        public string Klantnummer { get; set; }
        public string ImageUrl { get; set; }
        public string Producttype { get; set; }
        public string PlusPunten { get; set; }
        public string Productprijs { get; set; }
        public string Aantal { get; set; }

    }
}

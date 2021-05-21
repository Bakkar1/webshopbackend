using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WPLWebApiTeam13.Controllers
{
    public class QueryHistorieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

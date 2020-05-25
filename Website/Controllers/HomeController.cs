using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("savedata"))
            {
                HttpContext.Response.Cookies.Append("savedata", "");
                return View("~/Views/Game/Index.cshtml");
            }
            else
            {
                //new JsonObject.Deserialize(HttpContext.Request.Cookies["savedata"]);
                return View("~/Views/Game/Saved.cshtml");
            }



            //return View();

            //return game view
            //return View("~/Views/Game/Index.cshtml");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Logging;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Cookies

            //check for 'first_request' on client
            if (!HttpContext.Request.Cookies.ContainsKey("first_request"))
            {
                // Insert whatever data we want in a cookie on the client
                HttpContext.Response.Cookies.Append("first_request", DateTime.Now.ToString());
                //return Content("Welcome, new visitor!");
                return View("~/Views/Game/Index.cshtml");
                
            } else
            {
                //read client's cookie
                DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["first_request"]);
                //return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString());
                return View("~/Views/Game/Index.cshtml");
               
            }
            
            


            //return game view
            //return View("~/Views/Game/Index.cshtml");
        }
    }
}

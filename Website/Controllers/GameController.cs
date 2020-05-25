using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Website.Models;

namespace Website.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            //if (!HttpContext.Request.Cookies.ContainsKey("savedata"))
            //{
            //    return View("~/Views/Game/Index.cshtml");
            //} else
            //{
            //    return View("~/Views/Game/Index.cshtml");
            //}
            //return View("~/Views/Game/Index.cshtml");
            return View();

        }

        public IActionResult Saved()
        {
            return View();
        }

        public void SaveData(int turn, int cid, int mil, int hap, int eco, int rel)
        {
            SaveData sd = new SaveData(1, 2, 3, 4, 5, 6);
            HttpContext.Response.Cookies.Append("savedata", JsonConvert.SerializeObject(sd));
        }


        public string GetData()
        {
            //return JsonConvert.SerializeObject(new SaveData(turn, cid, mil, hap, eco, rel));
            //new SaveData(turn, cid, mil, hap, eco, rel);
            return HttpContext.Request.Cookies["savedata"];
        }


       
        //public IActionResult Cookie()
        //{
        //    if (!HttpContext.Request.Cookies.ContainsKey("first_request"))
        //    {
        //        // Insert whatever data we want in a cookie on the client
        //        HttpContext.Response.Cookies.Append("first_request", DateTime.Now.ToString());
        //        //return Content("Welcome, new visitor!");
        //        return View("~/Views/Game/Index.cshtml");
        //    }
        //    else
        //    {
        //        //read client's cookie
        //        DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["first_request"]);
        //        //return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString());
        //        return View("~/Views/Game/Index.cshtml");
        //    }
        //}
    }
}

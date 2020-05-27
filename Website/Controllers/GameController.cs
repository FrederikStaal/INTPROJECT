/*
 * Group 6
 * Rasmus, Joseph, Tony and Frederik
 * Class type: Controller
 * - Controller for the Game view
 */

using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Saved()
        //{
        //    return View();
        //}

        //public void SaveData(int turn, int cid, int mil, int hap, int eco, int rel)
        //{
        //    SaveData sd = new SaveData(1, 2, 3, 4, 5, 6);
        //    HttpContext.Response.Cookies.Append("savedata", JsonConvert.SerializeObject(sd));
        //}

        //public string GetData()
        //{
        //    return HttpContext.Request.Cookies["savedata"];
        //}
    }
}

/*
 * Group 6
 * Rasmus, Joseph, Tony and Frederik
 * Class type: Controller
 * - 
 */

using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

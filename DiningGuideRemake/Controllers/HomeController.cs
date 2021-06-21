using DiningGuideRemake.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiningGuideRemake.Data;

namespace DiningGuideRemake.Controllers
{
    public class HomeController : Controller
    {
        private readonly DiningGuideData database;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string search)
        {
            if (search != null) return RedirectToAction("Index","Restaurants", new { sortOrder = "rating", currentFilter = search });
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Register()
        {
            return RedirectToAction("Index", "Register");
        }
        public ActionResult Login()
        {
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Logout", "Login");
        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Search()
        {
            return RedirectToAction("Index","Restaurants");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

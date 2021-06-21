using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiningGuideRemake.Data;
using DiningGuideRemake.Models;
using Microsoft.AspNetCore.Http;

namespace DiningGuideRemake.Controllers
{
    public class LoginController : Controller
    {
        private readonly DiningGuideData _context;

        public LoginController(DiningGuideData context)
        {
            _context = context;
        }

        // GET: Search
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("_Email") != null) return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(String error)
        {
            if (HttpContext.Session.GetString("_Email") != null) return RedirectToAction("Index", "Home");
            ViewData["Error"] = error;
            return View();
        }

        // GET: Search/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {
            
            if (ModelState.IsValid && Verify(user.Email,user.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Index),new { error = "Username or password invalid!" });
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }


        private bool Verify(string email, string password)
        {
            var user = _context.Users.Where(e => e.Email.ToLower().Equals(email.ToLower()));
            bool passVerify = false;
            foreach(User c in user){
                passVerify= BCrypt.Net.BCrypt.Verify(password, c.Password);
                if (passVerify)
                {
                    HttpContext.Session.SetString("_Email", c.Email);
                    HttpContext.Session.SetString("_FirstName", c.FirstName);
                    HttpContext.Session.SetString("_LastName", c.LastName);
                    HttpContext.Session.SetInt32("_Type", (int)c.Type);
                }
            }
            return _context.Users.Any(e => e.Email.ToLower().Equals(email.ToLower())&&passVerify);
        }
    }
}

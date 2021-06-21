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
    public class RegisterController : Controller
    {
        private readonly DiningGuideData _context;

        public RegisterController(DiningGuideData context)
        {
            _context = context;
        }

        // GET: Search
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString("_Email") != null) return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Create([Bind("idUser,FirstName,LastName,Email,Password,Type")] User user)
        {
            bool stateValid = ModelState.IsValid;
            bool userExists = UserExists(user.Email);
            if (stateValid && !userExists)
            {
                user.Type = 0;
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Login");
            }
            if(userExists) return RedirectToAction(nameof(Index), new { error = "Email already exist!" });
            if (!stateValid) return RedirectToAction(nameof(Index), new { error = "Register information not valid!" });
            return RedirectToAction(nameof(Index), new { error = "Unknown error!" });
        }

        private bool UserExists(string Email)
        {
            return _context.Users.Any(e => e.Email.ToLower().Equals(Email.ToLower()));
        }
    }
}

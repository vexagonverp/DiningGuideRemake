using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiningGuideRemake.Data;
using DiningGuideRemake.Models;

namespace DiningGuideRemake.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly DiningGuideData _context;

        public RestaurantsController(DiningGuideData context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewData["AdressSortParm"] = String.IsNullOrEmpty(sortOrder) ? "adress" : "";
            ViewData["DescriptionSortParm"] = String.IsNullOrEmpty(sortOrder) ? "description" : "";
            ViewData["RatingSortParm"] = String.IsNullOrEmpty(sortOrder) ? "rating" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var restaurants = from r in _context.Restaurants
                           select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Name.Contains(searchString)
                                       || r.Address.Contains(searchString)
                                       || r.Decription.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    restaurants = restaurants.OrderByDescending(r => r.Name);
                    break;
                case "address":
                    restaurants = restaurants.OrderByDescending(r => r.Address);
                    break;
                case "description":
                    restaurants = restaurants.OrderByDescending(r => r.Decription);
                    break;
                case "rating":
                    restaurants = restaurants.OrderByDescending(r => r.Rating);
                    break;
                default:
                    restaurants = restaurants.OrderByDescending(r => r.Name);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Restaurant>.CreateAsync(restaurants.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Address,Decription,Rating,IDUser")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Address,Decription,Rating,IDUser")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.ID == id);
        }
    }
}

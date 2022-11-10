using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMovie.Models;
using Microsoft.AspNetCore.Authorization;

namespace Appmovie.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly AppMovieContext _context;

        public LocationsController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Location.Include(l => l.Countries);
            return View(await appMovieContext.ToListAsync());
        }

        // // GET: Locations/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.Location == null)
        //     {
        //         return NotFound();
        //     }

        //     var location = await _context.Location
        //         .Include(l => l.Country)
        //         .FirstOrDefaultAsync(m => m.LocationID == id);
        //     if (location == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(location);
        // }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryName");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocationID,LocationName,CountryID")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryName", location.CountryID);
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryName", location.CountryID);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocationID,LocationName,CountryID")] Location location)
        {
            if (id != location.LocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.LocationID))
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
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "CountryName", location.CountryID);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Location == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .Include(l => l.Countries)
                .FirstOrDefaultAsync(m => m.LocationID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            if (location != null)
            {
                var locationInPartner = (from a in _context.Partner where a.LocationID == id select a).ToList();
                if (locationInPartner.Count == 0)
                {
                    _context.Location.Remove(location);
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    
                }
               
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
          return (_context.Location?.Any(e => e.LocationID == id)).GetValueOrDefault();
        }
    }
}

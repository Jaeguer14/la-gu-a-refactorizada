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
    public class GendersController : Controller
    {
        private readonly AppMovieContext _context;

        public GendersController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Genders
        public async Task<IActionResult> Index()
        {
              return _context.Gender != null ? 
                          View(await _context.Gender.ToListAsync()) :
                          Problem("Entity set 'AppMovieContext.Gender'  is null.");
        }

        // // GET: Genders/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.Gender == null)
        //     {
        //         return NotFound();
        //     }

        //     var gender = await _context.Gender
        //         .FirstOrDefaultAsync(m => m.GenderID == id);
        //     if (gender == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(gender);
        // }

        // GET: Genders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenderID,GenderName")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // GET: Genders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Genders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenderID,GenderName")] Gender gender)
        {
            if (id != gender.GenderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.GenderID))
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
            return View(gender);
        }

        // GET: Genders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gender == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.GenderID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Genders/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Genders = await _context.Gender.FindAsync(id);
            
            if (Genders != null)
            {
                var GenderInMovie = (from a in _context.Movie where a.GenderID == id select a).ToList();
                if (GenderInMovie.Count == 0)
                {
                    _context.Gender.Remove(Genders);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(int id)
        {
          return (_context.Gender?.Any(e => e.GenderID == id)).GetValueOrDefault();
        }
    }
}

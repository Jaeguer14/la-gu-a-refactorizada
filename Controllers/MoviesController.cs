using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMovie.Models;

namespace Appmovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppMovieContext _context;

        public MoviesController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Movie.Include(m => m.Gender).Include(m => m.Producer).Include(m => m.Section);
            return View(await appMovieContext.ToListAsync());
        }

        // // GET: Movies/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.Movie == null)
        //     {
        //         return NotFound();
        //     }

        //     var movie = await _context.Movie
        //         .Include(m => m.Gender)
        //         .Include(m => m.Producer)
        //         .Include(m => m.Section)
        //         .FirstOrDefaultAsync(m => m.MovieID == id);
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(movie);
        // }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderID", "GenderName");
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerID", "ProducerName");
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionID", "SectionName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,MovieName,MovieDescription,MovieDate,SectionID,GenderID,ProducerID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderID", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerID", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionID", "SectionName", movie.SectionID);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderID", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerID", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionID", "SectionName", movie.SectionID);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,MovieName,MovieDescription,MovieDate,SectionID,GenderID,ProducerID")] Movie movie)
        {
            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
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
            ViewData["GenderID"] = new SelectList(_context.Gender, "GenderID", "GenderName", movie.GenderID);
            ViewData["ProducerID"] = new SelectList(_context.Producer, "ProducerID", "ProducerName", movie.ProducerID);
            ViewData["SectionID"] = new SelectList(_context.Section, "SectionID", "SectionName", movie.SectionID);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Gender)
                .Include(m => m.Producer)
                .Include(m => m.Section)
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movie == null)
            {
                return Problem("Entity set 'AppMovieContext.Movie'  is null.");
            }
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
                 await _context.SaveChangesAsync();
            }
            
           
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
          return (_context.Movie?.Any(e => e.MovieID == id)).GetValueOrDefault();
        }
    }
}

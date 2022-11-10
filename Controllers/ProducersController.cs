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
    public class ProducersController : Controller
    {
        private readonly AppMovieContext _context;

        public ProducersController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
              return _context.Producer != null ? 
                          View(await _context.Producer.ToListAsync()) :
                          Problem("Entity set 'AppMovieContext.Producer'  is null.");
        }

        // // GET: Producers/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.Producer == null)
        //     {
        //         return NotFound();
        //     }

        //     var producer = await _context.Producer
        //         .FirstOrDefaultAsync(m => m.ProducerID == id);
        //     if (producer == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(producer);
        // }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerID,ProducerName")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerID,ProducerName")] Producer producer)
        {
            if (id != producer.ProducerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerID))
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
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producer == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer
                .FirstOrDefaultAsync(m => m.ProducerID == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Productora = await _context.Producer.FindAsync(id);
            
            if (Productora != null)
            {
                var ProducturaInMovie = (from a in _context.Movie where a.ProducerID == id select a).ToList();
                if (ProducturaInMovie.Count == 0)
                {
                    _context.Producer.Remove(Productora);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
          return (_context.Producer?.Any(e => e.ProducerID == id)).GetValueOrDefault();
        }
    }
}

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
    public class PartnersController : Controller
    {
        private readonly AppMovieContext _context;

        public PartnersController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Partner.Include(p => p.Locations);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Partners/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null || _context.Partner == null)
        //     {
        //         return NotFound();
        //     }

        //     var partner = await _context.Partner
        //         .Include(p => p.Locations)
        //         .FirstOrDefaultAsync(m => m.PartnerID == id);
        //     if (partner == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(partner);
        // }

        // GET: Partners/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationName");
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartnerID,PartnerName,PartnerDirection,PartnerPhone,PartnerBirthDate,LocationID")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationName", partner.LocationID);
            return View(partner);
        }

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationName", partner.LocationID);
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerID,PartnerName,PartnerDirection,PartnerPhone,PartnerBirthDate,LocationID")] Partner partner)
        {
            if (id != partner.PartnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.PartnerID))
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
            ViewData["LocationID"] = new SelectList(_context.Location, "LocationID", "LocationName", partner.LocationID);
            return View(partner);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Partner == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Locations)
                .FirstOrDefaultAsync(m => m.PartnerID == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partner.FindAsync(id);
            if (partner != null)
            {
                var partnerInRental = (from a in _context.Rental where a.PartnerID == id select a).ToList();
                if (partnerInRental.Count == 0)
                {
                     _context.Partner.Remove(partner);
                     await _context.SaveChangesAsync();
                }
                else
                {
                    
                }
               
            }
            
            return RedirectToAction(nameof(Index));
        }
        private bool PartnerExists(int id)
        {
          return (_context.Partner?.Any(e => e.PartnerID == id)).GetValueOrDefault();
        }
    }
}

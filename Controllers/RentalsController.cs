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
    public class RentalsController : Controller
    {
        private readonly AppMovieContext _context;

        public RentalsController(AppMovieContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var appMovieContext = _context.Rental.Include(r => r.Partner);
            return View(await appMovieContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
     {
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.EstaAlquilada == false), "MovieID", "MovieName");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create([Bind("RentalID,RentalDate,PartnerID")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                using (var transaccion = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(rental);
                        await _context.SaveChangesAsync();

                        var moviesTemp = (from a in _context.RentalDetailTemp select a).ToList();
                        foreach (var item in moviesTemp)
                        {
                            var  details = new RentalDetail
                            {
                                RentalID = rental.RentalID,
                                MovieID = item.MovieID,
                                MovieName = item.MovieName
                            };
                            _context.RentalDetail.Add(details);
                            _context.SaveChanges();
                        }
                        _context.RentalDetailTemp.RemoveRange(moviesTemp);
                        _context.SaveChanges();

                        transaccion.Commit();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (System.Exception ex)
                    {
                        transaccion.Rollback();
                        var error = ex;
                    }
                    
                }
                
            }
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieName");
            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.EstaAlquilada == false), "MovieID", "MovieName");
            return View(rental);
        }

        // // GET: Rentals/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null || _context.Rental == null)
        //     {
        //         return NotFound();
        //     }

        //     var rental = await _context.Rental.FindAsync(id);
        //     if (rental == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
        //     return View(rental);
        // }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,RentalDate,PartnerID")] Rental rental)
        {
            if (id != rental.RentalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalID))
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
            ViewData["PartnerID"] = new SelectList(_context.Partner, "PartnerID", "PartnerName", rental.PartnerID);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Partner)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'AppMovieContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
                 await _context.SaveChangesAsync();
            }
            
           
            return RedirectToAction(nameof(Index));
        }

public JsonResult AddMovieTemp(int MovieID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {
                    var movie = (from a in _context.Movie where a.MovieID == MovieID select a).SingleOrDefault();
                    movie.EstaAlquilada = true;
                    _context.SaveChanges();

                    var movieTemp = new RentalDetailTemp
                    {
                        MovieID = movie.MovieID,
                        MovieName = movie.MovieName
                    };
                    _context.RentalDetailTemp.Add(movieTemp);
                    _context.SaveChanges();

                    transaccion.Commit();

                }
                catch (System.Exception)
                {

                    transaccion.Rollback();
                    resultado = false;
                }

            }

            ViewData["MovieID"] = new SelectList(_context.Movie.Where(x => x.EstaAlquilada == false), "MovieID", "MovieName");

            return Json(resultado);

        }


        public JsonResult CancelRental()
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {


                    var rentalTemp = (from a in _context.RentalDetailTemp select a).ToList();

                    foreach (var item in rentalTemp)
                    {
                        var movie = (from a in _context.Movie where a.MovieID == item.MovieID select a).SingleOrDefault();
                        movie.EstaAlquilada = false;
                        _context.SaveChanges();
                    }



                    _context.RentalDetailTemp.RemoveRange(rentalTemp);
                    _context.SaveChanges();

                    transaccion.Commit();
                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }



            return Json(resultado);
        }

         public JsonResult QuitarMovie(int MovieID)
        {
            var resultado = true;

            using (var transaccion = _context.Database.BeginTransaction())
            {
                try
                {


                    var movie = (from a in _context.Movie where a.MovieID == MovieID select a).SingleOrDefault();
                    movie.EstaAlquilada = false; 
                    _context.SaveChanges();
                    
                    
                    var rentalTemp = (from a in _context.RentalDetailTemp where a.MovieID == MovieID select a).SingleOrDefault();
                    _context.RentalDetailTemp.RemoveRange(rentalTemp);
                    _context.SaveChanges();

                    transaccion.Commit();
                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = false;
                }
            }



            return Json(resultado);
        }



        public JsonResult SearchMovieTemp()
        {
          List<RentalDetailTemp> ListadoMovieTemp = new List<RentalDetailTemp>();


          var RentalDetailTemp = (from a in _context.RentalDetailTemp select a).ToList();
          foreach (var item in RentalDetailTemp)
          {
            ListadoMovieTemp.Add(item);
          }

          return Json(ListadoMovieTemp);

        }

          public JsonResult SearchMovie(int RentalID)
        {
          List<RentalDetail> ListadoMovie = new List<RentalDetail>();


          var RentalDetail = (from a in _context.RentalDetail where a.RentalID == RentalID select a).ToList();
          foreach (var item in RentalDetail)
          {
            ListadoMovie.Add(item);
          }

          return Json(ListadoMovie);

        }
        private bool RentalExists(int id)
        {
          return (_context.Rental?.Any(e => e.RentalID == id)).GetValueOrDefault();
        }
    }
}

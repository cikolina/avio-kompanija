using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvioKompanija.Models;

namespace AvioKompanija.Controllers
{
    public class PutnikController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public PutnikController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Putnik
        public async Task<IActionResult> Index()
        {
            return View(await _context.Putnik.ToListAsync());
        }

        // GET: Putnik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnik = await _context.Putnik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (putnik == null)
            {
                return NotFound();
            }

            return View(putnik);
        }

        // GET: Putnik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Putnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,BrojPasosa,Pol,DatumRodjenja")] Putnik putnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(putnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(putnik);
        }

        // GET: Putnik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnik = await _context.Putnik.FindAsync(id);
            if (putnik == null)
            {
                return NotFound();
            }
            return View(putnik);
        }

        // POST: Putnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,BrojPasosa,Pol,DatumRodjenja")] Putnik putnik)
        {
            if (id != putnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(putnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PutnikExists(putnik.Id))
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
            return View(putnik);
        }

        // GET: Putnik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnik = await _context.Putnik
                .FirstOrDefaultAsync(m => m.Id == id);
            if (putnik == null)
            {
                return NotFound();
            }

            return View(putnik);
        }

        // POST: Putnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var putnik = await _context.Putnik.FindAsync(id);
            _context.Putnik.Remove(putnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PutnikExists(int id)
        {
            return _context.Putnik.Any(e => e.Id == id);
        }
    }
}

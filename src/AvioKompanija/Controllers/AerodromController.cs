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
    public class AerodromController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public AerodromController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Aerodrom
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aerodrom.ToListAsync());
        }

        // GET: Aerodrom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aerodrom = await _context.Aerodrom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aerodrom == null)
            {
                return NotFound();
            }

            return View(aerodrom);
        }

        // GET: Aerodrom/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aerodrom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Grad,Drzava")] Aerodrom aerodrom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aerodrom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aerodrom);
        }

        // GET: Aerodrom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aerodrom = await _context.Aerodrom.FindAsync(id);
            if (aerodrom == null)
            {
                return NotFound();
            }
            return View(aerodrom);
        }

        // POST: Aerodrom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Grad,Drzava")] Aerodrom aerodrom)
        {
            if (id != aerodrom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aerodrom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AerodromExists(aerodrom.Id))
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
            return View(aerodrom);
        }

        // GET: Aerodrom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aerodrom = await _context.Aerodrom
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aerodrom == null)
            {
                return NotFound();
            }

            return View(aerodrom);
        }

        // POST: Aerodrom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aerodrom = await _context.Aerodrom.FindAsync(id);
            _context.Aerodrom.Remove(aerodrom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AerodromExists(int id)
        {
            return _context.Aerodrom.Any(e => e.Id == id);
        }
    }
}

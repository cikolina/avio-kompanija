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
    public class SluzbenikController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public SluzbenikController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Sluzbenik
        public async Task<IActionResult> Index()
        {
            var avioKompanijaContext = _context.Sluzbenik.Include(s => s.Kompanija);
            return View(await avioKompanijaContext.ToListAsync());
        }

        // GET: Sluzbenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sluzbenik = await _context.Sluzbenik
                .Include(s => s.Kompanija)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sluzbenik == null)
            {
                return NotFound();
            }

            return View(sluzbenik);
        }

        // GET: Sluzbenik/Create
        public IActionResult Create()
        {
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv");
            return View();
        }
        

        // POST: Sluzbenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KompanijaId,Ime,Prezime,RadnoMjesto")] Sluzbenik sluzbenik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sluzbenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", sluzbenik.KompanijaId);
            return View(sluzbenik);
        }

        // GET: Sluzbenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sluzbenik = await _context.Sluzbenik.FindAsync(id);
            if (sluzbenik == null)
            {
                return NotFound();
            }
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", sluzbenik.KompanijaId);
            return View(sluzbenik);
        }

        // POST: Sluzbenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KompanijaId,Ime,Prezime,RadnoMjesto")] Sluzbenik sluzbenik)
        {
            if (id != sluzbenik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sluzbenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SluzbenikExists(sluzbenik.Id))
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
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", sluzbenik.KompanijaId);
            return View(sluzbenik);
        }

        // GET: Sluzbenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sluzbenik = await _context.Sluzbenik
                .Include(s => s.Kompanija)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sluzbenik == null)
            {
                return NotFound();
            }

            return View(sluzbenik);
        }

        // POST: Sluzbenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sluzbenik = await _context.Sluzbenik.FindAsync(id);
            _context.Sluzbenik.Remove(sluzbenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SluzbenikExists(int id)
        {
            return _context.Sluzbenik.Any(e => e.Id == id);
        }
    }
}

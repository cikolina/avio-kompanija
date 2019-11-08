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
    public class KartaController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public KartaController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Karta
        public async Task<IActionResult> Index()
        {
            var avioKompanijaContext = _context.Karta.Include(k => k.Let).Include(k => k.Putnik).Include(k => k.Sluzbenik);
            return View(await avioKompanijaContext.ToListAsync());
        }

        // GET: Karta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Karta
                .Include(k => k.Let)
                .Include(k => k.Putnik)
                .Include(k => k.Sluzbenik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // GET: Karta/Create
        public IActionResult Create()
        {
            ViewData["LetId"] = new SelectList(_context.Let, "Id", "Id");
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "Ime");
            ViewData["SluzbenikId"] = new SelectList(_context.Sluzbenik, "Id", "Ime");
            return View();
        }

        // POST: Karta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PutnikId,LetId,SluzbenikId,BrojSjedista,DatumProdaje,Cijena,Popust,Storn")] Karta karta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LetId"] = new SelectList(_context.Let, "Id", "Id", karta.LetId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "Ime", karta.PutnikId);
            ViewData["SluzbenikId"] = new SelectList(_context.Sluzbenik, "Id", "Ime", karta.SluzbenikId);
            return View(karta);
        }

        // GET: Karta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Karta.FindAsync(id);
            if (karta == null)
            {
                return NotFound();
            }
            ViewData["LetId"] = new SelectList(_context.Let, "Id", "Id", karta.LetId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa", karta.PutnikId);
            ViewData["SluzbenikId"] = new SelectList(_context.Sluzbenik, "Id", "Ime", karta.SluzbenikId);
            return View(karta);
        }

        // POST: Karta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PutnikId,LetId,SluzbenikId,BrojSjedista,DatumProdaje,Cijena,Popust,Storn")] Karta karta)
        {
            if (id != karta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartaExists(karta.Id))
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
            ViewData["LetId"] = new SelectList(_context.Let, "Id", "Id", karta.LetId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa", karta.PutnikId);
            ViewData["SluzbenikId"] = new SelectList(_context.Sluzbenik, "Id", "Ime", karta.SluzbenikId);
            return View(karta);
        }

        // GET: Karta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karta = await _context.Karta
                .Include(k => k.Let)
                .Include(k => k.Putnik)
                .Include(k => k.Sluzbenik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karta == null)
            {
                return NotFound();
            }

            return View(karta);
        }

        // POST: Karta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karta = await _context.Karta.FindAsync(id);
            _context.Karta.Remove(karta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartaExists(int id)
        {
            return _context.Karta.Any(e => e.Id == id);
        }
    }
}

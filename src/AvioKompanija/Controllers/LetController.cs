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
    public class LetController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public LetController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Let
        public async Task<IActionResult> Index()
        {
            var avioKompanijaContext = _context.Let.Include(x => x.Kompanija).Include(x => x.KrajnjaDestinacija).Include(x => x.PocetnaDestinacija).Include(x => x.Terminal);
            return View(await avioKompanijaContext.ToListAsync());
        }

        // GET: Let/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var let = await _context.Let
                .Include(x => x.Kompanija)
                .Include(x => x.KrajnjaDestinacija)
                .Include(x => x.PocetnaDestinacija)
                .Include(x => x.Terminal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (let == null)
            {
                return NotFound();
            }

            return View(let);
        }

        // GET: Let/Create
        public IActionResult Create()
        {
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv");
            ViewData["KrajnjaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad");
            ViewData["PocetnaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad");
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Naziv");
            return View();
        }

        // POST: Let/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PocetnaDestinacijaId,KrajnjaDestinacijaId,TerminalId,KompanijaId,DatumPolaska,BrojMjesta,BrojLeta")] Let let)
        {
            if (ModelState.IsValid)
            {
                _context.Add(let);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", let.KompanijaId);
            ViewData["KrajnjaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.KrajnjaDestinacijaId);
            ViewData["PocetnaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.PocetnaDestinacijaId);
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Naziv", let.TerminalId);
            return View(let);
        }

        // GET: Let/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var let = await _context.Let.FindAsync(id);
            if (let == null)
            {
                return NotFound();
            }
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", let.KompanijaId);
            ViewData["KrajnjaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.KrajnjaDestinacijaId);
            ViewData["PocetnaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.PocetnaDestinacijaId);
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Naziv", let.TerminalId);
            return View(let);
        }

        // POST: Let/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PocetnaDestinacijaId,KrajnjaDestinacijaId,TerminalId,KompanijaId,DatumPolaska,BrojMjesta,BrojLeta")] Let let)
        {
            if (id != let.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(let);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetExists(let.Id))
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
            ViewData["KompanijaId"] = new SelectList(_context.Kompanija, "Id", "Naziv", let.KompanijaId);
            ViewData["KrajnjaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.KrajnjaDestinacijaId);
            ViewData["PocetnaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad", let.PocetnaDestinacijaId);
            ViewData["TerminalId"] = new SelectList(_context.Terminal, "Id", "Naziv", let.TerminalId);
            return View(let);
        }

        // GET: Let/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var let = await _context.Let
                .Include(x => x.Kompanija)
                .Include(x => x.KrajnjaDestinacija)
                .Include(x => x.PocetnaDestinacija)
                .Include(x => x.Terminal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (let == null)
            {
                return NotFound();
            }

            return View(let);
        }

        // POST: Let/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var let = await _context.Let.FindAsync(id);
            _context.Let.Remove(let);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetExists(int id)
        {
            return _context.Let.Any(e => e.Id == id);
        }
    }
}

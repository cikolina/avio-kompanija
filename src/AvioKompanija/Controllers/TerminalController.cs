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
    public class TerminalController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public TerminalController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Terminal
        public async Task<IActionResult> Index()
        {
            var avioKompanijaContext = _context.Terminal.Include(t => t.Aerodrom);
            return View(await avioKompanijaContext.ToListAsync());
        }

        // GET: Terminal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .Include(t => t.Aerodrom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // GET: Terminal/Create
        public IActionResult Create()
        {
            ViewData["AerodromId"] = new SelectList(_context.Aerodrom, "Id", "Grad");
            return View();
        }

        // POST: Terminal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AerodromId,Naziv")] Terminal terminal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terminal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AerodromId"] = new SelectList(_context.Aerodrom, "Id", "Grad", terminal.AerodromId);
            return View(terminal);
        }

        // GET: Terminal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal.FindAsync(id);
            if (terminal == null)
            {
                return NotFound();
            }
            ViewData["AerodromId"] = new SelectList(_context.Aerodrom, "Id", "Grad", terminal.AerodromId);
            return View(terminal);
        }

        // POST: Terminal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AerodromId,Naziv")] Terminal terminal)
        {
            if (id != terminal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terminal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminalExists(terminal.Id))
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
            ViewData["AerodromId"] = new SelectList(_context.Aerodrom, "Id", "Grad", terminal.AerodromId);
            return View(terminal);
        }

        // GET: Terminal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terminal = await _context.Terminal
                .Include(t => t.Aerodrom)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (terminal == null)
            {
                return NotFound();
            }

            return View(terminal);
        }

        // POST: Terminal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terminal = await _context.Terminal.FindAsync(id);
            _context.Terminal.Remove(terminal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminalExists(int id)
        {
            return _context.Terminal.Any(e => e.Id == id);
        }
    }
}

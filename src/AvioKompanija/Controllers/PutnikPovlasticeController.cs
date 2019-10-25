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
    public class PutnikPovlasticeController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public PutnikPovlasticeController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: PutnikPovlastice
        public async Task<IActionResult> Index()
        {
            var avioKompanijaContext = _context.PutnikPovlastice.Include(p => p.Povlastice).Include(p => p.Putnik);
            return View(await avioKompanijaContext.ToListAsync());
        }

        // GET: PutnikPovlastice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnikPovlastice = await _context.PutnikPovlastice
                .Include(p => p.Povlastice)
                .Include(p => p.Putnik)
                .FirstOrDefaultAsync(m => m.PutnikId == id);
            if (putnikPovlastice == null)
            {
                return NotFound();
            }

            return View(putnikPovlastice);
        }

        // GET: PutnikPovlastice/Create
        public IActionResult Create()
        {
            ViewData["PovlasticeId"] = new SelectList(_context.Povlastice, "Id", "Id");
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa");
            return View();
        }

        // POST: PutnikPovlastice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PutnikId,PovlasticeId")] PutnikPovlastice putnikPovlastice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(putnikPovlastice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PovlasticeId"] = new SelectList(_context.Povlastice, "Id", "Id", putnikPovlastice.PovlasticeId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa", putnikPovlastice.PutnikId);
            return View(putnikPovlastice);
        }

        // GET: PutnikPovlastice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnikPovlastice = await _context.PutnikPovlastice.FindAsync(id);
            if (putnikPovlastice == null)
            {
                return NotFound();
            }
            ViewData["PovlasticeId"] = new SelectList(_context.Povlastice, "Id", "Id", putnikPovlastice.PovlasticeId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa", putnikPovlastice.PutnikId);
            return View(putnikPovlastice);
        }

        // POST: PutnikPovlastice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PutnikId,PovlasticeId")] PutnikPovlastice putnikPovlastice)
        {
            if (id != putnikPovlastice.PutnikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(putnikPovlastice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PutnikPovlasticeExists(putnikPovlastice.PutnikId))
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
            ViewData["PovlasticeId"] = new SelectList(_context.Povlastice, "Id", "Id", putnikPovlastice.PovlasticeId);
            ViewData["PutnikId"] = new SelectList(_context.Putnik, "Id", "BrojPasosa", putnikPovlastice.PutnikId);
            return View(putnikPovlastice);
        }

        // GET: PutnikPovlastice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var putnikPovlastice = await _context.PutnikPovlastice
                .Include(p => p.Povlastice)
                .Include(p => p.Putnik)
                .FirstOrDefaultAsync(m => m.PutnikId == id);
            if (putnikPovlastice == null)
            {
                return NotFound();
            }

            return View(putnikPovlastice);
        }

        // POST: PutnikPovlastice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var putnikPovlastice = await _context.PutnikPovlastice.FindAsync(id);
            _context.PutnikPovlastice.Remove(putnikPovlastice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PutnikPovlasticeExists(int id)
        {
            return _context.PutnikPovlastice.Any(e => e.PutnikId == id);
        }
    }
}

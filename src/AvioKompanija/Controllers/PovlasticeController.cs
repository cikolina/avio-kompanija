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
    public class PovlasticeController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public PovlasticeController(AvioKompanijaContext context)
        {
            _context = context;
        }

        // GET: Povlastice
        public async Task<IActionResult> Index()
        {
            return View(await _context.Povlastice.ToListAsync());
        }

        // GET: Povlastice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var povlastice = await _context.Povlastice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (povlastice == null)
            {
                return NotFound();
            }

            return View(povlastice);
        }

        // GET: Povlastice/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Povlastice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Procenat,Detalji")] Povlastice povlastice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(povlastice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(povlastice);
        }

        // GET: Povlastice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var povlastice = await _context.Povlastice.FindAsync(id);
            if (povlastice == null)
            {
                return NotFound();
            }
            return View(povlastice);
        }

        // POST: Povlastice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Procenat,Detalji")] Povlastice povlastice)
        {
            if (id != povlastice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(povlastice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PovlasticeExists(povlastice.Id))
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
            return View(povlastice);
        }

        // GET: Povlastice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var povlastice = await _context.Povlastice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (povlastice == null)
            {
                return NotFound();
            }

            return View(povlastice);
        }

        // POST: Povlastice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var povlastice = await _context.Povlastice.FindAsync(id);
            _context.Povlastice.Remove(povlastice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PovlasticeExists(int id)
        {
            return _context.Povlastice.Any(e => e.Id == id);
        }
    }
}

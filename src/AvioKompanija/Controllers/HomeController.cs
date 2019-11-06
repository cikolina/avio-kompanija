using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AvioKompanija.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AvioKompanija.Controllers
{
    public class HomeController : Controller
    {
        private readonly AvioKompanijaContext _context;

        public HomeController(AvioKompanijaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["KrajnjaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad");
            ViewData["PocetnaDestinacijaId"] = new SelectList(_context.Destinacija, "Id", "Grad");
            return View();
        }

        public async Task<IActionResult> Search (Pretraga pretraga)
        {
            if (ModelState.IsValid)
            {
                var datumPolaska = pretraga.DatumPolaska;
                var datumPovratka = pretraga.DatumPovratka;
                var pocetnaDestinacija = pretraga.PocetnaDestinacijaId;
                var krajnjaDestinacija = pretraga.KrajnjaDestinacijaId;
                var brojPutnika = pretraga.BrojPutnika;

                if (datumPovratka != null)
                {
                    //povratni
                    var letPrviSmjer = _context.Let
                        .Where(x => x.PocetnaDestinacijaId == pocetnaDestinacija
                            && x.KrajnjaDestinacijaId == krajnjaDestinacija
                            && x.DatumPolaska == datumPolaska)
                        .Select(x => x.Id)
                        .FirstOrDefault();

                    var letDrugiSmjer = _context.Let
                        .Where(x => x.PocetnaDestinacijaId == krajnjaDestinacija
                            && x.KrajnjaDestinacijaId == pocetnaDestinacija
                            && x.DatumPolaska == datumPovratka)
                        .Select(x => x.Id)
                        .FirstOrDefault();

                    if (letPrviSmjer > 0 && letDrugiSmjer > 0)
                    {
                        TempData["IsPovratni"] = true;
                        TempData["PovratniLetPrviSmjer"] = letPrviSmjer;
                        TempData["PovratniLetDrugiSmjer"] = letDrugiSmjer;

                        return RedirectToAction("Create", "Karta");
                    }
                    else
                    {
                        ViewData["ErrorPovratniLet"] = "Ne postoje trazeni let i povratni let!!";
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    //jedan smijer
                    var letPrviSmjer = _context.Let
                        .Where(x => x.PocetnaDestinacijaId == pocetnaDestinacija
                            && x.KrajnjaDestinacijaId == krajnjaDestinacija
                            && x.DatumPolaska == datumPolaska)
                        .Select(x => x.Id)
                        .FirstOrDefault();

                    if (letPrviSmjer > 0)
                    {
                        TempData["IsPovratni"] = true;
                        TempData["PovratniLetPrviSmjer"] = letPrviSmjer;

                        return RedirectToAction("Create", "Karta");
                    }
                    else
                    {
                        ViewData["ErrorPovratniLet"] = "Ne postoji trazeni let!!";
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewData["ErrorPovratniLet"] = "Pogresna pretraga!!";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

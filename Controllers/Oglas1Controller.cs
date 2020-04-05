using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalExamNew.Dal;
using FinalExamNew.Data;

namespace FinalExamNew.Controllers
{
    public class Oglas1Controller : Controller
    {
        private readonly FinalExamDbContext _context;

        public Oglas1Controller(FinalExamDbContext context)
        {
            _context = context;
        }

        // GET: Oglas1
        public async Task<IActionResult> Index()
        {
            var finalExamDbContext = _context.Oglasi.Include(o => o.Cena);
            return View(await finalExamDbContext.ToListAsync());
        }

        // GET: Oglas1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglasi
                .Include(o => o.Cena)
                .FirstOrDefaultAsync(m => m.OglasId == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // GET: Oglas1/Create
        public IActionResult Create()
        {
            ViewData["CenaId"] = new SelectList(_context.Cene, "CenaId", "CenaId");
            return View();
        }

        // POST: Oglas1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OglasId,Naslov,Tekst,CenaId,DatumKreiranja")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CenaId"] = new SelectList(_context.Cene, "CenaId", "CenaId", oglas.CenaId);
            return View(oglas);
        }

        // GET: Oglas1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglasi.FindAsync(id);
            if (oglas == null)
            {
                return NotFound();
            }
            ViewData["CenaId"] = new SelectList(_context.Cene, "CenaId", "CenaId", oglas.CenaId);
            return View(oglas);
        }

        // POST: Oglas1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OglasId,Naslov,Tekst,CenaId,DatumKreiranja")] Oglas oglas)
        {
            if (id != oglas.OglasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oglas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OglasExists(oglas.OglasId))
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
            ViewData["CenaId"] = new SelectList(_context.Cene, "CenaId", "CenaId", oglas.CenaId);
            return View(oglas);
        }

        // GET: Oglas1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglasi
                .Include(o => o.Cena)
                .FirstOrDefaultAsync(m => m.OglasId == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // POST: Oglas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var oglas = await _context.Oglasi.FindAsync(id);
            _context.Oglasi.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OglasExists(string id)
        {
            return _context.Oglasi.Any(e => e.OglasId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalExamNew.Data;
using FinalExamNew.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalExamNew.Controllers
{
    public class OglasController : Controller
    {
        private readonly FinalExamDbContext _context;

        public OglasController(FinalExamDbContext context)
        {
            _context = context;
        }
        // GET: Oglas
        public ActionResult Index()
        {
            var oglasi = _context.Oglasi.Include(o => o.Cena);
            List<OglasViewModel> oglasiView = new List<OglasViewModel>();
            foreach (var oglas in oglasi)
            {
                OglasViewModel o = new OglasViewModel()
                {
                    ID = oglas.OglasId,
                    Naslov=oglas.Naslov,
                    Cena=oglas.Cena.ToString(),
                    UploadFiles=new List<IFormFile>()
                };
            }
            return View(oglasiView);
        }

        // GET: Oglas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Oglas/Create
        public ActionResult Create()
        {
            var tipoviOglasa = _context.TipoviOglasa;
            ViewData["TipoviOglasa"] = new SelectList(tipoviOglasa, "TipOglasaId", "NazivTipaOglasa");
            return View();
        }

        // POST: Oglas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Oglas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Oglas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Oglas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Oglas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
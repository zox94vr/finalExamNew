using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalExamNew.Areas.Identity.Data;
using FinalExamNew.Dal;
using FinalExamNew.Data;
using FinalExamNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalExamNew.Controllers
{
    public class OglasController : Controller
    {
        private readonly FinalExamDbContext _context;
        private readonly UserManager<FinalExamNewUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;
        //private readonly UserManager<ApplicationUser> _userManager;
        public OglasController(FinalExamDbContext context, UserManager<FinalExamNewUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Oglas
        public ActionResult Index()
        {
            //this takes request parameters only from the query string
            //Request.QueryString["parameter1"];


            //this one works for bot - form and query string
            //Request["parameter1"];
            string search = HttpContext.Request.Query["Search"].ToString();
            List<OglasViewModel> oglasiView = new List<OglasViewModel>();
            if(search==null || search == "")
            {
                foreach (var oglas in _context.Oglasi.Include(o => o.Cena).ToList())
                {
                    OglasViewModel o = new OglasViewModel()
                    {
                        ID = oglas.OglasId,
                        Naslov = oglas.Naslov,
                        Cena = oglas.Cena.ToString(),
                    };
                    oglasiView.Add(o);
                }
            }
            else
            {
                var oglasi = (from kr in _context.KljucneReci
                              join kro in _context.KljucneReciOglasa on kr.KljucnaRecId equals kro.KljucnaRec.KljucnaRecId
                              join o in _context.Oglasi on kro.Oglas.OglasId equals o.OglasId
                              join c in _context.Cene on o.Cena.CenaId equals c.CenaId
                              where kr.Rec.Contains(search)
                              select new OglasViewModel()
                              {
                                  ID = o.OglasId,
                                  Naslov = o.Naslov,
                                  Cena = o.Cena.ToString(),
                              }).ToList();
                if (oglasi != null)
                {
                    oglasiView = oglasi;
                }
            }
            return View(oglasiView);
        }

        // GET: Oglas/Details/5
        public ActionResult Details(string id)
        {
            Oglas o1 = _context.Oglasi.Where(m => m.OglasId == id).Include(a => a.Slike).Include(m => m.KljucneReciOglasa).Include(m => m.User).Include(m => m.Cena).FirstOrDefault();
            List<string> adreseSlika = new List<string>();
            foreach (var item in o1.Slike)
            {
                string webRoothPath = _hostingEnvironment.WebRootPath;
                adreseSlika.Add(item.AdresaSlike.Replace(webRoothPath,""));
            }

            OglasViewModel ovm = new OglasViewModel()
            {
                Cena = o1.Cena.ToString(),
                //DatumOd = o1.DatumKreiranja,
                ID = o1.OglasId,
                KljucneReci = String.Join(" ", o1.KljucneReciOglasa),
                Naslov = o1.Naslov,
                Tekst = o1.Tekst,
                User = o1.User.ToString(),
                AdreseSlika = adreseSlika

            };
            return View(ovm);
        }

        // GET: Oglas/Create

        [Authorize]
        public ActionResult Create()
        {
            var tipoviOglasa = _context.TipoviOglasa;
            ViewData["TipoviOglasa"] = new SelectList(tipoviOglasa, "TipOglasaId", "NazivTipaOglasa");

            return View();
        }

        // POST: Oglas/Create

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OglasViewModel oglasView)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Oglas oglas = new Oglas();
                    oglas.DatumKreiranja = DateTime.Now;
                    oglas.Naslov = oglasView.Naslov;
                    oglas.Tekst = oglasView.Tekst;
                    Cena cena = new Cena();
                    cena.Valuta = "RSD";
                    cena.Vrednost = Convert.ToDecimal(oglasView.Cena);
                    oglas.Cena = cena;
                    List<Slika> slike = new List<Slika>();
                    if (Request.Form.Files.Count > 0)
                    {
                        for (int i = 0; i < Request.Form.Files.Count; i++)
                        {
                            string currentFileName = "";
                            var fileContent = Request.Form.Files[i];
                            string webRoothPath = _hostingEnvironment.WebRootPath;
                            string fullFileName = webRoothPath + string.Format("/ProizvodUpload/{0}/{1}", oglas.OglasId.ToString(), fileContent.FileName);
                            string dirName = webRoothPath + (string.Format("/ProizvodUpload/{0}/", oglas.OglasId.ToString()));

                            if (!System.IO.Directory.Exists(dirName))
                            {
                                System.IO.Directory.CreateDirectory(dirName);
                            }

                            int countFileNames = 0;
                            while (System.IO.File.Exists(fullFileName))
                            {
                                countFileNames++;
                                fullFileName = fullFileName.Replace(System.IO.Path.GetExtension(fullFileName), "") + "_" + countFileNames.ToString() + System.IO.Path.GetExtension(fullFileName);
                            }

                            if (fileContent != null && fileContent.Length > 0)
                            {
                                var stream = fileContent;
                                using (var fileStream = System.IO.File.Create(fullFileName))
                                {
                                    await stream.CopyToAsync(fileStream);
                                }
                            }

                            currentFileName = System.IO.Path.GetFileName(fullFileName);
                            Slika s = new Slika()
                            {
                                AdresaSlike = fullFileName,
                                NaslovSlike = fileContent.FileName,
                                VremePostavljanjaSlike = DateTime.Now,
                                Oglas = oglas
                            };
                            slike.Add(s);

                        }
                    }

                    // TODO: Add insert logic here
                    oglas.Slike = slike;
                    List<KljucneReciOglasa> kro = new List<KljucneReciOglasa>();
                    var kljucneReci = oglasView.KljucneReci.Split(" ");

                    if (kljucneReci.Length > 0)
                    {
                        foreach (var item in kljucneReci)
                        {
                            KljucnaRec kr = new KljucnaRec()
                            {
                                Rec = item
                            };
                            if (_context.KljucneReci.Where(m => m.Rec == kr.Rec).Count() > 0)
                            {
                                kr = _context.KljucneReci.Select(m => m).Where(m => m.Rec == kr.Rec).FirstOrDefault();

                            }
                            else
                            {
                                _context.KljucneReci.Add(kr);

                            }
                            KljucneReciOglasa kljucnaRecOglasa = new KljucneReciOglasa()
                            {
                                Oglas = oglas,
                                KljucnaRec = kr
                            };
                            kro.Add(kljucnaRecOglasa);

                        }

                    }
                    oglas.KljucneReciOglasa = kro;
                    List<Oglasavanje> oglasavanja = new List<Oglasavanje>();
                    Oglasavanje oglasavanje = new Oglasavanje()
                    {
                        DatumOd = oglasView.DatumOd,
                        DatumDo = oglasView.DatumDo,
                        TipOglasa = _context.TipoviOglasa.Where(m => m.TipOglasaId == oglasView.TipoviOglasa.ToString()).FirstOrDefault()
                    };
                    oglasavanja.Add(oglasavanje);
                    oglas.Oglasavanja = oglasavanja;
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    oglas.UserId = currentUser.Id;
                    await _context.Oglasi.AddAsync(oglas);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View();
            }

        }
        //GET: Oglas/DeletePicture/5
        [Authorize]
        public ActionResult DeletePicture(string id)
        {
            try
            {
                Slika s = _context.Slike.Where(m => m.SlikaId == id).FirstOrDefault();
                if (s != null)
                {
                    if (System.IO.File.Exists(s.AdresaSlike))
                    {
                        System.IO.File.Delete(s.AdresaSlike);
                    }
                    _context.Slike.Remove(s);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return Redirect(Request.Headers["Referer"].ToString());
        }
        // GET: Oglas/Edit/5

        [Authorize]
        public ActionResult Edit(string id)
        {
            Oglas o1 = _context.Oglasi.Where(m => m.OglasId == id).Include(a => a.Slike).Include(m => m.KljucneReciOglasa).Include(m => m.User).Include(m => m.Cena).Include(m=>m.Oglasavanja).FirstOrDefault();
            //Dictionary<string, string> adreseSlika = new Dictionary<string, string>();
            List<string> adreseSlika = new List<string>();
            foreach (var item in o1.Slike)
            {
                string webRoothPath = _hostingEnvironment.WebRootPath;
                adreseSlika.Add( item.AdresaSlike.Replace(webRoothPath, "")+"@"+item.SlikaId);
            }

            List<string> kljucneReciOglasaIds = new List<string>();
            foreach (var item in o1.KljucneReciOglasa)
            {
                kljucneReciOglasaIds.Add(item.KljucneReciOglasaId);
            }
            List<string> kljucneReci = new List<string>();
            kljucneReci = _context.KljucneReciOglasa.Where(m => kljucneReciOglasaIds.Contains(m.KljucneReciOglasaId)).Include(m => m.KljucnaRec).OrderByDescending(x=>x.KljucnaRec.KljucnaRecId).Select(m => m.KljucnaRec.Rec).ToList();
            var tipoviOglasa = _context.TipoviOglasa.ToList();
            TipOglasa selectedTipOglasa = tipoviOglasa.Where(m => m.TipOglasaId == o1.Oglasavanja.FirstOrDefault().TipOglasa.TipOglasaId).FirstOrDefault();
            ViewData["TipoviOglasa"] = new SelectList(tipoviOglasa, "TipOglasaId", "NazivTipaOglasa",selectedTipOglasa.TipOglasaId);
            OglasViewModel ovm = new OglasViewModel()
            {
                Cena = o1.Cena.ToString(),
                //DatumOd = o1.DatumKreiranja,
                ID = o1.OglasId,
                KljucneReci = String.Join(" ", kljucneReci),
                Naslov = o1.Naslov,
                Tekst = o1.Tekst,
                User = o1.User.ToString(),
                DatumOd=o1.Oglasavanja.FirstOrDefault().DatumOd,
                DatumDo=o1.Oglasavanja.FirstOrDefault().DatumDo,
                AdreseSlika=adreseSlika
                
            };
            return View("Edit",ovm);
        }

        // POST: Oglas/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, OglasViewModel oglas)
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
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Oglas/Delete/5
        [Authorize]
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
        // GET: Oglas/Delete/5
        [Authorize]
        public async Task<ActionResult> MojiOglasi()
        {
            string search = HttpContext.Request.Query["Search"].ToString();
            List<OglasViewModel> oglasiView = new List<OglasViewModel>();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.Owner = true;
            if (search == null || search == "")
            {
                foreach (var oglas in _context.Oglasi.Where(m=>m.UserId==currentUser.Id).Include(o => o.Cena).ToList())
                {
                    OglasViewModel o = new OglasViewModel()
                    {
                        ID = oglas.OglasId,
                        Naslov = oglas.Naslov,
                        Cena = oglas.Cena.ToString(),
                    };
                    oglasiView.Add(o);
                }
            }
            else
            {
                var oglasi = (from kr in _context.KljucneReci
                              join kro in _context.KljucneReciOglasa on kr.KljucnaRecId equals kro.KljucnaRec.KljucnaRecId
                              join o in _context.Oglasi on kro.Oglas.OglasId equals o.OglasId
                              join c in _context.Cene on o.Cena.CenaId equals c.CenaId
                              where kr.Rec.Contains(search) && o.UserId==currentUser.Id
                              select new OglasViewModel()
                              {
                                  ID = o.OglasId,
                                  Naslov = o.Naslov,
                                  Cena = o.Cena.ToString(),
                              }).ToList();
                if (oglasi != null)
                {
                    oglasiView = oglasi;
                }
            }
            return View("Index",oglasiView);
        }
    }
}
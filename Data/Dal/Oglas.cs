using FinalExamNew.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class Oglas
    {
        [Key]
        public string OglasId { get; set; }
        public string Naslov { get; set; }

        public string Tekst { get; set; }

        public Cena Cena { get; set; }

        public string CenaId { get { return CenaId; } set {CenaId=Cena.CenaId; } }

        public List<Slika> Slike { get; set; }

        public List<KljucneReciOglasa> KljucneReciOglasa { get; set; }

        public List<Oglasavanje> Oglasavanja { get; set; }

        public FinalExamNewUser User { get; set; }
        public DateTime DatumKreiranja { get; set; }
        //private Oglas()
        //{
        //    OglasId= Guid.NewGuid().ToString(); 
        //}
        public Oglas()
        {
            OglasId = Guid.NewGuid().ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class TipOglasa
    {
        [Key]
        public string TipOglasaId { get; set; }
        public string NazivTipaOglasa { get; set; }
        public Cena Cena { get; set; }
        //public string CenaId { get { return Cena.CenaId; } set { CenaId = Cena.CenaId; } }

        public List<Oglasavanje> Oglasavanja { get; set; }
        //private TipOglasa()
        //{
        //  TipOglasaId =  Guid.NewGuid().ToString();
        //}
        public TipOglasa()
        {
            TipOglasaId = Guid.NewGuid().ToString();
        }
    }
}

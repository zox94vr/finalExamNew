using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class Cena
    {
        [Key]
        public string CenaId { get; set; }
        [Column(TypeName = "decimal(16,2)")]

        public decimal Vrednost { get; set; }
        public string Valuta { get; set; }
        public  List<TipOglasa> TipoviOglasa { get; set; }
        public  List<Oglas> Oglasi { get; set; }
        //private Cena()
        //{
        //    CenaId = Guid.NewGuid().ToString();
        //}
        public Cena()
        {
            CenaId = Guid.NewGuid().ToString();
        }
        public override string ToString()
        {
            return Vrednost.ToString()+" "+Valuta;
        }
    }
}

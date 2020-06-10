using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class Oglasavanje
    {
        [Key,Column(Order =0)]
        public string OglasavanjeId { get; set; }
        public TipOglasa TipOglasa { get; set; }
        public Oglas Oglas { get; set; }
        //[Key, Column(Order=1)]
        //public string TipOglasaId => TipOglasa.TipOglasaId;
        //[Key, Column(Order = 2)]
        //public string OglasId => Oglas.OglasId;

        public DateTime DatumOd { get; set; }
        public DateTime DatumDo { get; set; }
        //private Oglasavanje()
        //{
        //    OglasavanjeId= Guid.NewGuid().ToString(); 
        //}
        public Oglasavanje()
        {
            OglasavanjeId = Guid.NewGuid().ToString();
        }

    }
}

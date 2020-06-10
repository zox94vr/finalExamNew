using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class KljucneReciOglasa
    {
        [Key,Column(Order =0)]
        public string KljucneReciOglasaId { get; set; }
        public Oglas Oglas { get; set; }
        public KljucnaRec KljucnaRec { get; set; }
        //[Key, Column(Order = 1)]
        //public string OglasId => Oglas.OglasId;

        //[Key, Column(Order = 2)]
        //public string KljucnaRecId => KljucnaRec.KljucnaRecId;

        //private KljucneReciOglasa()
        //{
        //    KljucneReciOglasaId= Guid.NewGuid().ToString();
        //}
        public KljucneReciOglasa()
        {
            KljucneReciOglasaId = Guid.NewGuid().ToString();
        }

    }
}

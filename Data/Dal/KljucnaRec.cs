using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class KljucnaRec
    {
        [Key]
        public string KljucnaRecId { get; set; }
        public string Rec { get; set; }
        public List<KljucneReciOglasa> KljucneReciOglasa { get; set; }
        //private KljucnaRec()
        //{
        //    KljucnaRecId= Guid.NewGuid().ToString();
        //}
        public KljucnaRec()
        {
            KljucnaRecId = Guid.NewGuid().ToString();
        }
        public override string ToString()
        {
            return Rec; 
        }
    }
}

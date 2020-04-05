using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Dal
{
    public class Slika
    {
        [Key,Column(Order =0)]
        public string SlikaId { get; set; }
        public string AdresaSlike { get; set; }
        public string NaslovSlike { get; set; }
        public DateTime VremePostavljanjaSlike { get; set; }
        public Oglas Oglas { get; set; }
        //[Key, Column(Order = 1)]
        public string OglasId { get { return OglasId; }set { OglasId = Oglas.OglasId; } }
        //private Slika()
        //{
        //    SlikaId= Guid.NewGuid().ToString();
        //}
        public Slika()
        {
            SlikaId = Guid.NewGuid().ToString();
        }
    }
}

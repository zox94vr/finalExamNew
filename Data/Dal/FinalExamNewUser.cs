using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalExamNew.Dal;
using Microsoft.AspNetCore.Identity;

namespace FinalExamNew.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the FinalExamNewUser class
    public class FinalExamNewUser : IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumUclanjenja { get; set; }
        public string JMBG { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string UlicaIBroj { get; set; }
        public List<Oglas> Oglasi { get; set; }


    }
}

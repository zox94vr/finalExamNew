using FinalExamNew.Dal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalExamNew.Models
{
    public class OglasViewModel
    {
        [Browsable(false)]
        public string ID { get; set; }
        [Display(Name = "Naslov oglasa")]
        public string Naslov { get; set; }
        [Display(Name = "Tekst oglasa")]
        public string Tekst { get; set; }
        [Display(Name = "Cena ")]
        public string Cena { get; set; }
        [Display(Name = "Datum vazenja od")]
        public DateTime DatumOd { get; set; }
        [Display(Name = "Datum vazenja do")]
        public DateTime DatumDo { get; set; }
        [Display(Name = "Tip oglasa")]
        public List<TipOglasa> TipoviOglasa { get; set; }
        [Display(Name = "Ubaci slike")]
        public List<IFormFile> UploadFiles { get; set; }
        [Display(Name ="Kljucne reci (odvojene razmakom)")]
        public string KljucneReci { get; set; }
        public OglasViewModel()
        {
            UploadFiles = new List<IFormFile>();
            TipoviOglasa = new List<TipOglasa>();

        }
    }
}

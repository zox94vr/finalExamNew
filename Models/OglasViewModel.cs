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
        public decimal Cena { get; set; }
        [Display(Name = "Valuta ")]
        public string Valuta { get; set; }

        [Display(Name = "Datum vazenja od")]
        [DataType(DataType.DateTime)]
        public DateTime DatumOd { get; set; }
        [Display(Name = "Datum vazenja do")]
        [DataType(DataType.DateTime)]
        public DateTime DatumDo { get; set; }

        [Display(Name = "Tip oglasa")]
        public string TipOglasa { get; set; }
        public List<TipOglasa> TipoviOglasa { get; set; }
        [Display(Name = "Ubaci slike")]
        public List<IFormFile> UploadFiles { get; set; }
        [Display(Name ="Kljucne reci (odvojene razmakom)")]
        public string KljucneReci { get; set; }
        public string User { get; set; }
        public List<string> AdreseSlika { get; set; }
        public Dictionary<string,string>Slike { get; set; }
        public OglasViewModel()
        {
            TipoviOglasa = new List<TipOglasa>();

        }
    }
}

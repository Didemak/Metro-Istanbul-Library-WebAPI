using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Tur
    {
        public int Id { get; set; }
        public string TurAdi { get; set; }

        public virtual List<Kitap> Kitaplar { get; set; }
    }
}
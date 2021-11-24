using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Uye
    {
        public int Id { get; set; }
        public int SicilNo { get; set; }
        public string UyeAdi { get; set; }
        public string UyeSoyadi { get; set; }
        public string TelefonNo { get; set; }
        public string Cinsiyet{ get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

    }
}
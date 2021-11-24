using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class Odunc
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public virtual Kitap Kitap { get; set; }
        public virtual Uye Uye { get; set; }
      
    }
}
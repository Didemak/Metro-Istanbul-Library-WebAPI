using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class KutuphaneContext : DbContext
    {
        public KutuphaneContext() : base("kütüphaneConnection")
        {
            Database.SetInitializer(new KutuphaneInitializer()); //benim tanımladığım initializerden haberi var
        }

        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Tur> Turler { get; set; }
        public DbSet<Odunc> Oduncler { get; set; }
        public DbSet<YetkiliGiris> YetkiliGiris { get; set; }
    }
}
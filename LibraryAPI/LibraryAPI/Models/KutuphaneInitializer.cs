using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibraryMVC.Models
{
    public class KutuphaneInitializer:DropCreateDatabaseIfModelChanges<KutuphaneContext>
    {
        protected override void Seed(KutuphaneContext context)
        {
            List<Tur> tur = new List<Tur>()
            {
                new Tur(){ TurAdi ="Roman"},
                new Tur(){ TurAdi ="Şiir" },
                new Tur(){ TurAdi= "Anı Kitapları" },
                new Tur(){ TurAdi="Gezi " },
                new Tur(){ TurAdi="Biyografi " },
                new Tur(){ TurAdi="Bilgi " },
                new Tur(){ TurAdi="Din " },
                new Tur(){ TurAdi="Çocuk " }
            };
            foreach (var item in tur)
            {

                context.Turler.Add(item);
            }
            context.SaveChanges();


            base.Seed(context);
        }
    }
}
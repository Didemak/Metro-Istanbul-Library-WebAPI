using LibraryAPI.Models;
using LibraryMVC.Models;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAPI.Controllers
{
    public class KutuphaneController : ApiController
    {
        KutuphaneContext context = new KutuphaneContext();

        // ----------------Giriş İşlemleri------------------
        [Route("api/Giris")]
        [HttpGet]
        public IHttpActionResult GetGiris()
        {
            return Ok();
        }


        [Route("api/Giris")]
        [HttpPost]
        public IHttpActionResult PostGiris(YetkiliGiris entity)
        {
            var data = (from giris in context.YetkiliGiris
                        where giris.SicilNo == entity.SicilNo & giris.sifre == entity.sifre
                        select giris).FirstOrDefault();

            if (data != null)
            {
                return Ok();
            }

            return Ok();
        }

        //-----------------Üye İşlemleri-----------------
        [Route("api/Kutuphane/UyeList")]
        [HttpGet]
        public IHttpActionResult GetUyeList()
        {
            var data = (from uye in context.Uyeler
                        select uye).ToList();
            return Ok(data);
        }

        [Route("api/Kutuphane/UyeEkle")]
        [HttpPost]
        public IHttpActionResult PostUyeEkle(Uye entity)
        {
            entity.IsActive = true;
            context.Uyeler.Add(entity);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteUye(int Id)
        {
            var silinecek = context.Uyeler.Find(Id);

            var odunc = (from odun in context.Oduncler
                         where odun.Uye.Id == Id
                         select odun).FirstOrDefault();

            if (odunc == null)
            {
                silinecek.IsActive = false;
                //context.Uyeler.Remove(silinecek);
            }
            else
            {
                silinecek.IsActive = false;
                //context.Uyeler.Remove(silinecek);
                odunc.Kitap.StokDurumu += 1;
                odunc.IsActive = false;
                //context.Oduncler.Remove(odunc);
            }

            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetUyeGuncel(int Id)
        {
            var guncellenecek = context.Uyeler.Find(Id);
            return Ok(guncellenecek);
        }
        [HttpPut] //??
        public IHttpActionResult PutUyeGuncel(Uye entity)
        {
            var vericek = context.Uyeler.Find(entity.Id);
            vericek.Id = entity.Id;
            vericek.SicilNo = entity.SicilNo;
            vericek.UyeAdi = entity.UyeAdi;
            vericek.UyeSoyadi = entity.UyeSoyadi;
            vericek.TelefonNo = entity.TelefonNo;
            vericek.Cinsiyet = entity.Cinsiyet;
            vericek.Email = entity.Email;

            context.SaveChanges();

            return Ok();
        }

        [Route("api/Kutuphane/UyeAra")]
        public IHttpActionResult GetUyeAra(string UyeAdi)
        {
            List<Uye> bulunanlar;
            if (UyeAdi.Contains(" "))
            {
                string[] AdSoyad = UyeAdi.Split(' ');
                string isim = AdSoyad[0];
                string soyisim = AdSoyad[1];

                bulunanlar = (from uye in context.Uyeler
                                  where uye.UyeAdi.Contains(isim) & uye.UyeSoyadi.Contains(soyisim)
                                  select uye).ToList();
            }
            else
            {
                bulunanlar = (from uye in context.Uyeler
                                  where uye.UyeAdi.Contains(UyeAdi) || uye.UyeSoyadi.Contains(UyeAdi)
                                  select uye).ToList();
            }

            return Ok(bulunanlar);
        }
        //-----------------Kitap İşlemleri-----------------

        [Route("api/Kutuphane/KitapEkle")]
        [HttpPost]
        public IHttpActionResult PostKitapEkle(Kitap entity)
        {
            Kitap yeniKitap = new Kitap();
            Tur turid = (from tur in context.Turler
                         where tur.Id == entity.TurID
                         select tur).FirstOrDefault();
            yeniKitap.Tur = turid;
            entity.IsActive = true;
            yeniKitap = entity;

            context.Kitaplar.Add(yeniKitap);
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("api/Kutuphane/KitapList")]
        public IHttpActionResult GetKitapList()
        {
            var data = context.Kitaplar.ToList();
            return Ok(data);
        }
       
        [HttpDelete]
        public IHttpActionResult DeleteKitap(int Id)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                try
                {
                    var silinecek = context.Kitaplar.Find(Id);
                    var odunc = (from odun in context.Oduncler
                                 where odun.Kitap.Id == Id
                                 select odun).FirstOrDefault();


                    if (odunc == null)
                    {
                        silinecek.IsActive = false;
                        //context.Kitaplar.Remove(odunc);
                    }
                    else
                    {
                        silinecek.IsActive = false;
                        //throw new Exception("aaa"); //alttakileri çalıştırma demek
                        odunc.IsActive = false;
                        
                        //context.Oduncler.Remove(odunc);


                    }
                    context.SaveChanges();
                    trans.Commit();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            
            return Ok();

        }

        [HttpGet]
        public IHttpActionResult GetKitapGuncel(int Id)
        {
            var guncellenecek = context.Kitaplar.Find(Id);
            return Ok(guncellenecek);
        }
        public IHttpActionResult PutKitapGuncel(Kitap entity)
        {
            Tur turid = (from tur in context.Turler
                         where tur.Id == entity.TurID
                         select tur).FirstOrDefault();

            var vericek = context.Kitaplar.Find(entity.Id);

            vericek.Id = entity.Id;
            vericek.KitapAdi = entity.KitapAdi;
            vericek.KitapYazari = entity.KitapYazari;
            vericek.Aciklama = entity.Aciklama;
            vericek.BaskiYil = entity.BaskiYil;
            vericek.YayinEvi = entity.YayinEvi;
            vericek.StokDurumu = entity.StokDurumu;
            vericek.Tur = turid;
            vericek.TurID = entity.TurID;

            context.SaveChanges();
            return Ok();
        }

        [Route("api/Kutuphane/KitapAra")]
        public IHttpActionResult GetKitapAra(string KitapAdi){

            var bulunanlar = (from kitap in context.Kitaplar
                              where kitap.KitapAdi.Contains(KitapAdi) //Bize gelen kitap adını içeren kitapları seçmek için
                              select kitap).ToList();                 //(harf hassasiyeti var)

            return Ok(bulunanlar);
        }

        //-----------------Ödünç İşlemleri-----------------

        //[Route("api/Kutuphane/OduncVer")]
        //[HttpGet]
        //public IHttpActionResult GetOduncVer(int id)
        //{
        //    return Ok(id);
        //}

        [HttpPost]
        public IHttpActionResult PostOduncVer(List<int> liste)
        {

            int Id = liste[0];
            int SicilNo = liste[1];
            Kitap kitapId = (from kitap in context.Kitaplar
                             where kitap.Id == Id
                             select kitap).FirstOrDefault();

            Uye uyeId = (from uye in context.Uyeler
                         where uye.SicilNo == SicilNo
                         select uye).FirstOrDefault();

            kitapId.StokDurumu -= 1;

            Odunc yeniodunc = new Odunc();

            yeniodunc.Kitap = kitapId;
            yeniodunc.Uye = uyeId;
            yeniodunc.IsActive = true;

            context.Oduncler.Add(yeniodunc);
            context.SaveChanges();
            return Ok();
        }

        [Route("api/Kutuphane/OduncList")]
        [HttpGet] 
        public IHttpActionResult GetOduncList()
        {
            var data = context.Oduncler.ToList();
            return Ok(data);
        }

        [HttpDelete]
        public IHttpActionResult DeleteKitapIade(int Id)
        {
            var silinecek = context.Oduncler.Find(Id);

            silinecek.Kitap.StokDurumu += 1;
            silinecek.IsActive = false;
            //context.Oduncler.Remove(silinecek);
            context.SaveChanges();
            return Ok();
        }

    }
}

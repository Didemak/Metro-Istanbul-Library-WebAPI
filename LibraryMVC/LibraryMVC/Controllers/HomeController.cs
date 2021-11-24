using LibraryMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class HomeController : Controller
    {
        //KutuphaneContext context = new KutuphaneContext(); //soru yazıcam önce bunu koyuyorum
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        ////İlk başta KitapEkle View'inin açılması için 
        //[HttpGet]
        //public ActionResult KitapEkle()
        //{
        //    List<SelectListItem> dinamik_degerler = (from x2 in context.Turler.ToList()
        //                                             select new SelectListItem
        //                                             {
        //                                                 Text = x2.TurAdi,
        //                                                 Value = x2.Id.ToString()
        //                                             }
        //                                            ).ToList();
        //    ViewBag.degerlerim = dinamik_degerler;
        //    return View();
        //}
        ////KitapEkle Viewinda kaydet butonuna basıldığında(form post edildiğinde) çalışacak kısım
        //[HttpPost]
        //public ActionResult KitapEkle(Kitap entity)
        //{
        //    Kitap yeniKitap = new Kitap();
        //    Tur turid = (from tur in context.Turler
        //                 where tur.Id == entity.TurID
        //                 select tur).FirstOrDefault();


        //    yeniKitap.Tur = turid;
        //    yeniKitap.TurID = entity.TurID;
        //    yeniKitap.KitapAdi = entity.KitapAdi;
        //    yeniKitap.KitapYazari = entity.KitapYazari;
        //    yeniKitap.BaskiYil = entity.BaskiYil;
        //    yeniKitap.EklenmeTarihi = entity.EklenmeTarihi;
        //    yeniKitap.YayinEvi = entity.YayinEvi;
        //    yeniKitap.SayfaSayi = entity.SayfaSayi;
        //    yeniKitap.StokDurumu = entity.StokDurumu;

        //    context.Kitaplar.Add(entity);
        //    context.SaveChanges();

        //    return View("KitapListele", context.Kitaplar.ToList());

        //}


        ////public ActionResult KitapEkle(string KitapAdi, string KitapYazari, int SayfaSayi, int BaskiYil, DateTime EklenmeTarihi, string YayinEvi, int StokDurumu, int Tur)
        ////{
        ////    Kitap yeniKitap = new Kitap();

        ////    Tur turid = (from tur in context.Turler //bu linq sorgusunda context.Turler'in takma adını from tur ile yazarız
        ////                 where tur.Id == Tur        //View'de seçilen Türün id si veritabanında hangi türe denk geliyor sorgusunu yazdık
        ////                 select tur).FirstOrDefault();  //sonra o denk gelen türü seç

        ////    yeniKitap.Tur = turid;
        ////    yeniKitap.KitapAdi = KitapAdi;
        ////    yeniKitap.KitapYazari = KitapYazari;
        ////    yeniKitap.BaskiYil = BaskiYil;
        ////    yeniKitap.EklenmeTarihi = EklenmeTarihi;
        ////    yeniKitap.YayinEvi = YayinEvi;
        ////    yeniKitap.SayfaSayi = SayfaSayi;
        ////    yeniKitap.StokDurumu = StokDurumu;

        ////    context.Kitaplar.Add(yeniKitap);
        ////    context.SaveChanges();

        ////    return RedirectToAction("KitapListele");
        ////}
        //[HttpGet]
        //public ActionResult UyeEkle()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult UyeEkle(Uye entity)
        //{
        //    context.Uyeler.Add(entity);
        //    context.SaveChanges();

        //    return View("UyeListele", context.Uyeler.ToList());
        //}

        //public ActionResult KitapListele()
        //{
        //    return View(context.Kitaplar.ToList());
        //}
        //public ActionResult UyeListele()
        //{
        //    return View(context.Uyeler.ToList());
        //}

        //public ActionResult UyeSil(int Id)
        //{


        //    var silinecek = context.Uyeler.Find(Id);

        //    var odunc = (from odun in context.Oduncler
        //                 where odun.Uye.Id == Id
        //                 select odun).FirstOrDefault();

        //    if (odunc == null)
        //    {
        //        context.Uyeler.Remove(silinecek);
        //    }
        //    else
        //    {
        //        context.Uyeler.Remove(silinecek);
        //        odunc.Kitap.StokDurumu += 1;
        //        context.Oduncler.Remove(odunc);
        //    }


        //    context.SaveChanges();

        //    return RedirectToAction("UyeListele");//burada RedirectToAction kullanınca context.Uyeler.ToList() bunu kullanmamıza gerek kalmıyor
        //}

        //[HttpGet]
        //public ActionResult UyeGuncelle(int Id)
        //{
        //    var guncellenecek = context.Uyeler.Find(Id);
        //    return View(guncellenecek);
        //}

        //[HttpPost]
        //public ActionResult UyeGuncelle(Uye entity)
        //{
        //    var vericek = context.Uyeler.Find(entity.Id);
        //    vericek.Id = entity.Id;
        //    vericek.SicilNo = entity.SicilNo;
        //    vericek.UyeAdi = entity.UyeAdi;
        //    vericek.UyeSoyadi = entity.UyeSoyadi;
        //    vericek.TelefonNo = entity.TelefonNo;
        //    vericek.Cinsiyet = entity.Cinsiyet;
        //    vericek.Email = entity.Email;

        //    context.SaveChanges();
        //    return RedirectToAction("UyeListele");
        //}
        //public ActionResult KitapSil(int Id)
        //{
        //    var silinecek = context.Kitaplar.Find(Id);
        //    var odunc = (from odun in context.Oduncler
        //                 where odun.Kitap.Id == Id
        //                 select odun).FirstOrDefault();


        //    if (odunc==null)
        //    {
        //        context.Kitaplar.Remove(silinecek);
        //    } 
        //    else
        //    {
        //        context.Kitaplar.Remove(silinecek);
        //        context.Oduncler.Remove(odunc);
        //    }

        //    context.SaveChanges();
        //    return RedirectToAction("KitapListele");
        //}

        //[HttpGet]
        //public ActionResult KitapGuncelle(int Id)

        //{
        //    List<SelectListItem> dinamik_degerler = (from x2 in context.Turler.ToList()
        //                                             select new SelectListItem
        //                                             {
        //                                                 Text = x2.TurAdi,
        //                                                 Value = x2.Id.ToString()
        //                                             }
        //                                            ).ToList();
        //    ViewBag.degerlerim = dinamik_degerler;
        //    var guncellenecek = context.Kitaplar.Find(Id);
        //    return View(guncellenecek);
        //}

        //[HttpPost]
        //public ActionResult KitapGuncelle(Kitap entity)
        //{

        //    Tur turid = (from tur in context.Turler
        //                 where tur.Id == entity.TurID
        //                 select tur).FirstOrDefault();

        //    var vericek = context.Kitaplar.Find(entity.Id);

        //    vericek.Id = entity.Id;
        //    vericek.KitapAdi = entity.KitapAdi;
        //    vericek.KitapYazari = entity.KitapYazari;
        //    vericek.SayfaSayi = entity.SayfaSayi;
        //    vericek.BaskiYil = entity.BaskiYil;
        //    vericek.YayinEvi = entity.YayinEvi;
        //    vericek.StokDurumu = entity.StokDurumu;
        //    vericek.Tur = turid;
        //    vericek.TurID = entity.TurID;

        //    context.SaveChanges();
        //    return RedirectToAction("KitapListele");
        //}




        //[HttpGet]
        //public ActionResult OduncVer(int Id)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult OduncVer(int Id, int SicilNo)
        //{
        //    Kitap kitapId = (from kitap in context.Kitaplar
        //                     where kitap.Id == Id
        //                     select kitap).FirstOrDefault();

        //    Uye uyeId = (from uye in context.Uyeler
        //                 where uye.SicilNo == SicilNo
        //                 select uye).FirstOrDefault();

        //    kitapId.StokDurumu -= 1;

        //    Odunc yeniodunc = new Odunc();

        //    yeniodunc.Kitap = kitapId;
        //    yeniodunc.Uye = uyeId;

        //    context.Oduncler.Add(yeniodunc);
        //    context.SaveChanges();

        //    return RedirectToAction("KitapListele");
        //}





        //public ActionResult OduncListele()
        //{
        //    return View(context.Oduncler.ToList());
        //}


        //public ActionResult KitapIade(int Id)
        //{
        //    var silinecek = context.Oduncler.Find(Id);

        //    silinecek.Kitap.StokDurumu += 1;
        //    context.Oduncler.Remove(silinecek);
        //    context.SaveChanges();

        //    return RedirectToAction("OduncListele");
        //}
    }
}
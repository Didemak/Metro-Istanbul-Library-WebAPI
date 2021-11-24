using LibraryMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryMVC.Controllers
{
    public class KutuphaneController : Controller
    {
        KutuphaneContext context = new KutuphaneContext();
        // GET: Kutuphane
        public ActionResult Index()
        {
            return View();
        }

        string Baseurl = "https://localhost:44373/";

        [HttpGet]
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Giris(YetkiliGiris entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/Giris");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<YetkiliGiris>("Giris", entity);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Giris");
        }

        //-----------------Üye İşlemleri-----------------
        public async Task<ActionResult> UyeListele()
        {
            List<Uye> EmpInfo = new List<Uye>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();//??
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Kutuphane/UyeList");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result; //resul^t'a erişiliyor
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Uye>>(EmpResponse); //yukardakiyle ilişkisi ne???
                }
                //returning the employee list to view
                return View(EmpInfo);
            }
        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UyeEkle(Uye entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/UyeEkle");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Uye>("UyeEkle", entity);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("UyeListele");
                }
            }
            return View("UyeListele");
        }

        public ActionResult UyeSil(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteUye/" + Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("UyeListele");
                }
            }
            return RedirectToAction("UyeListele");
        }

        [HttpGet]
        public ActionResult UyeGuncelle(int Id)
        {

            Uye yeniuye = null;

            using (var client = new HttpClient())//HttpClint ın nesnesini oluşturuyoruz.
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/");// ve web apimizin temel adresini oluşturuyoruz.
                //HTTP GET
                var responseTask = client.GetAsync("GetUyeGuncel?id=" + Id.ToString());// GetASync yöntemi belirtilen Url ye bir http GET isteği gönderir.
                responseTask.Wait();//Wait, GetAsync yöntemi yürütmeyi tamamlayıp bir sonuç döndürene kadar yürütmeyi askıya alır.

                var result = responseTask.Result; // Task'ın sonucunu alırız.
                if (result.IsSuccessStatusCode)//HTTP yanıtının durumunu kontrol ederiz.(IsSuccessStatusCode 200-299 arasında olup olmadığını kontrol eder resulın 200 se içeri girersin )
                {
                    yeniuye = result.Content.ReadAsAsync<Uye>().Result; //ReadAsAsync kullanarak sonucun içeriği okunur.//Deserilize yapıyor


                    /*  yeniuye = readTask;*/ //?? readtask
                }
            }

            return View(yeniuye);
        }

        [HttpPost]
        public ActionResult UyeGuncelle(Uye entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/PutUyeGuncel");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Uye>("PutUyeGuncel", entity);//serilize yapılıyor
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)//Bu talebim başarılıysa içeri gir
                {                             //Başarılı mı başarısız mı olduğunu try catchle mi yapıcak?

                    return RedirectToAction("UyeListele");
                }
            }
            return View("UyeListele");
        }

        public async Task<ActionResult> UyeAra(string UyeAdi)
        {
            List<Uye> EmpInfo = new List<Uye>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Kutuphane/UyeAra?UyeAdi=" + UyeAdi);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Uye>>(EmpResponse);
                }
                //returning the employee list to view
                return View("UyeListele", EmpInfo);
            }
        }


        //-----------------Kitap İşlemleri-----------------

        public async Task<ActionResult> KitapListele()
        {
            List<Kitap> EmpInfo = new List<Kitap>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Kutuphane/KitapList");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Kitap>>(EmpResponse);
                }
                //returning the employee list to view
                return View(EmpInfo);
            }
        }



        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> dinamik_degerler = (from x2 in context.Turler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x2.TurAdi,
                                                         Value = x2.Id.ToString()
                                                     }
                                                    ).ToList();
            ViewBag.degerlerim = dinamik_degerler;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> KitapEkle(Kitap entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/KitapEkle");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Kitap>("KitapEkle", entity);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("KitapListele");
                }
            }

            return View("KitapListele");
        }

        public ActionResult KitapSil(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteKitap/" + Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("KitapListele");
                }
            }

            return RedirectToAction("KitapListele");
        }

        [HttpGet]
        public ActionResult KitapGuncelle(int Id)
        {

            Kitap yenikitap = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/");
                //HTTP GET
                var responseTask = client.GetAsync("GetKitapGuncel?id=" + Id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Kitap>();//deserilize çevirme yapıyor //kitap nesnesi döndüğün taahhüt ediyorum
                    readTask.Wait();

                    yenikitap = readTask.Result;
                }
            }

            List<SelectListItem> dinamik_degerler = (from x2 in context.Turler.ToList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x2.TurAdi,
                                                         Value = x2.Id.ToString()
                                                     }
                                                    ).ToList();
            ViewBag.degerlerim = dinamik_degerler;

            return View(yenikitap);
        }

        [HttpPost]
        public ActionResult KitapGuncelle(Kitap entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/PutKitapGuncel");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Kitap>("PutKitapGuncel", entity);  //apiye //<Kitap> silik oldğu için silsekde bir şey olmaz
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("KitapListele");
                }
            }
            return View("KitapListele");
        }

        [HttpPost]
        public async Task<ActionResult> KitapAra(string KitapAdi)
        {
            List<Kitap> EmpInfo = new List<Kitap>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Kutuphane/KitapAra?KitapAdi=" + KitapAdi);
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    EmpInfo = JsonConvert.DeserializeObject<List<Kitap>>(EmpResponse);
                }
                //returning the employee list to view
                return View("KitapListele", EmpInfo);
            }
        }


        //-----------------Ödünç İşlemleri-----------------

        [HttpGet]
        public ActionResult OduncVer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> OduncVer(int Id, int SicilNo)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/PostOduncVer");

                List<int> liste = new List<int> { Id, SicilNo };
                //HTTP POST
                var postTask = client.PostAsJsonAsync<List<int>>("PostOduncVer", liste);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("OduncListele");
                }
            }

            return View("OduncListele");
        }

        [HttpGet]
        public async Task<ActionResult> OduncListele()
        {
            List<Odunc> EmpInf = new List<Odunc>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Kutuphane/OduncList");

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    EmpInf = JsonConvert.DeserializeObject<List<Odunc>>(EmpResponse);
                }
                return View(EmpInf);
            }
        }

        public ActionResult KitapIade(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44373/api/Kutuphane/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteKitapIade/" + Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("OduncListele");
                }
            }

            return RedirectToAction("OduncListele");
        }
    }
}
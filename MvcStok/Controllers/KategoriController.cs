using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; //Entity klasörüne ulaşmak için bu kodu eklememiz gerekiyor.
using PagedList;  //Sayfalama için bu iki kütüphaneyi eklememiz gerekiyor
using PagedList.Mvc; //Sayfalama 1.İşlem Kütüphaneler
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MvcDbStokEntities db = new MvcDbStokEntities(); //Entity klasörü içerisindeki Model'e ulaşmak için ise bu kod gerekli.
        public ActionResult Index(int sayfa=1)                 //db nesnemiz. MvcDbStokEntities ten türettiğimiz bir nesne. 
                                                   //Veritabanımızdaki tablolara db nesnesi sayesinde erişeceğiz.
        {
            //var degerler = db.TBLKATEGORILER.ToList();
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 6);  //Sayfalama 2.İşlem
            return View(degerler);
        }

        //HttpPost ve HttpGet arasındaki fark ve önemi nedir?
        //YeniKategori ekle butonuna tıkladığımda YeniKategori sayfası geliyor herhangi bir ekleme yapmasamda Null boş 
        //bir satır kayıt eklemiş oluyor. Bu sorunun önüne geçmek için HttpGet ve HttpPost kodları gerekmektedir.
        //Ben kaydet butonuna basana kadar yeni bir kayıt getirme işleminin yapılmaması isteniyor.

        [HttpPost]   //Kaydet butonuna bastığım zaman bu kod bloğu çalışacak ve yeni bir kayıt eklenecek
        public ActionResult YeniKategori(TBLKATEGORILER p1) 
        {

            //3.Validation(Doğrulama) İşlemi
            if (!ModelState.IsValid) // Modelde Doğrulama işlemi yapılmadıysa yenikategori sayfasını döndür

            {
                return View("YeniKategori");
            }

            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]  //Kaydet butonuna basmıyorum eski sayfaya geri dönmek istediğimde bu kod çalışacak yeni kayıt eklenmeyecek
                  //Boş bir kayıtında önüne geçilmiş olacak bu sayede
        public ActionResult YeniKategori()
        {

            return View();
        }

        public ActionResult Sil(int id)
        {
            var degerler = db.TBLKATEGORILER.Find(id);
            db.TBLKATEGORILER.Remove(degerler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult KategoriGetir(int id)
        {
            var degerler = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir",degerler);
        }

        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIID = p1.KATEGORIID;
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
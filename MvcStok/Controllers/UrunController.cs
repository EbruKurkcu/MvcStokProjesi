using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]  //Sayfa yüklendiğinde gelsin yani Yeni Ürün sayfasına geçtiğimizde DropdownList kodları olsun. 
        public ActionResult YeniUrun()
        {

            //Dropdownlist kullanımı get alanını ile yapılmaktadır.


            List<SelectListItem> deger = (from i in db.TBLKATEGORILER.ToList()

                                          select new SelectListItem
                                          {
                                              Text = i.KATEGORIAD,
                                              Value = i.KATEGORIID.ToString()
                                          }).ToList();
            ViewBag.dgr = deger;  //Buradaki deger değişkenini diğer sayfaya Viewbag komutu ile taşıyabiliriz.
            return View();
        }


        [HttpPost]  //Sayfada butona tıkladığımızda bu kod çalışşın ve ekleme işlemi yapılsın.
        public ActionResult YeniUrun( TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Sil(int id)
        {
            var deger = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(deger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var degerler = db.TBLURUNLER.Find(id);

            List<SelectListItem> deger = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }). ToList();


            ViewBag.dgr = deger;


            return View("UrunGetir", degerler);
        }

        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNID = p1.URUNID;
            urun.URUNAD = p1.URUNAD;
            //urun.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            urun.FIYAT = p1.FIYAT;
            urun.MARKA = p1.MARKA;
            urun.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
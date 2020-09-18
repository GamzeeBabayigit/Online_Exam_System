using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineSinav.Models;
using System.Web.Helpers;
using System.Web.Security;
using System.Data.Entity;
using System.Data;

namespace OnlineSinav.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        DbOnlineSinav db = new DbOnlineSinav();
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public PartialViewResult IndexNew()
        {
            return PartialView(GetCart());
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddToQuestion(int Id)
        {
            var soru = db.Sorulars.FirstOrDefault(i => i.Id == Id);
            if (soru != null)
            {
                GetCart().AddQuestion(soru, 1);
            }
            return RedirectToAction("AddExam", "Cart");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var soru = db.Sorulars.FirstOrDefault(i => i.Id == Id);
            if (soru != null)
            {
                GetCart().DelecetQuestion(soru);
                return RedirectToAction("AddExam", "Cart");
            }
            return RedirectToAction("AddExam", "Cart");
            
        }
        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult AddExam()
        {
            var sorular = db.Sorulars.Include(x => x.Ogretmens);
            return View(sorular.ToList());
        }
        [HttpPost]
        public ActionResult AddExam(Sinav sinav)
        {
            var cart = GetCart();


            if (ModelState.IsValid)
            {
                SaveExam(cart, sinav);

                cart.Clear();
                TempData["AddExamm"] = "Sisteme Yeni Sınav Eklediniz..";

                return RedirectToAction("Process", "Home");
            }
            else
            {
                return View(sinav);
            }
        }

        private void SaveExam(Cart cart, Sinav sinav)
        {

            var exam = new Exam();

            exam.ExamNumber = "A"+(new Random()).Next(11111, 99999).ToString();

            exam.SinavAdi = sinav.SinavAdi;
            exam.SinavSuresi = sinav.SinavSuresi;
            exam.OgretmenAdi = sinav.OgretmenAdi;
            exam.KacinciSinif = sinav.KacinciSinif;
            exam.BaslangicSaati = sinav.BaslangicSaati;
            exam.BitisSaati = sinav.BitisSaati;

            exam.ExamLines = new List<ExamLinesModels>();

            foreach (var exm in cart.CartLines)
            {
                var examline = new ExamLinesModels();
                examline.SoruId = exm.Soru.Id;
                examline.SoruAdi = exm.Soru.SoruAdi;
                examline.DogruCevap = exm.Soru.DogruCevap;
                examline.YanlisCevap1 = exm.Soru.YanlisCevap1;
                examline.YanlisCevap2 = exm.Soru.YanlisCevap2;
                examline.YanlisCevap3 = exm.Soru.YanlisCevap3;
                examline.YanlisCevap4 = exm.Soru.YanlisCevap4;
                examline.ZorlukDerecesi = exm.Soru.ZorlukDerecesi;
                examline.Puan = exm.Soru.Puan;
                exam.ExamLines.Add(examline);
            }
            db.Exam.Add(exam);
            db.SaveChanges();

        }

        public ActionResult ExamDetails()
        {
            var sinav = db.Sinavs.ToList();
            return View(sinav);
        }
        [HttpPost]
        public ActionResult ExamDetails(Sinav sinav)
        {
            var cart = GetCart();

            if (ModelState.IsValid)
            {
                SaveExam(cart, sinav);

                cart.Clear();
                TempData["AddExamm"] = "Sisteme Yeni Bir Soru Eklediniz";
                return RedirectToAction("Process","Home");
            }
            else
            {
                return View(sinav);
            }
        }

    }
}

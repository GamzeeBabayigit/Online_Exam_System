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
    public class OgrenciController : Controller
    {
        //
        // GET: /Ogrenci/

        DbOnlineSinav db = new DbOnlineSinav();

      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult newExam()
        {
            var exa = db.Exam
            .Select(i => new ExamModel()
            {
                Id = i.Id,
                ExamNumber = i.ExamNumber,
                BaslangicSaati = i.BaslangicSaati.ToString(),
                BitisSaati = i.BitisSaati.ToString(),
                SinavAdi = i.SinavAdi,
                SinavSuresi = i.SinavSuresi


            }).OrderByDescending(i => i.Id).ToList();
            return View(exa);
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Ogrenci ogr)
        {
            db.Ogrencis.Add(ogr);
            db.SaveChanges();
            return RedirectToAction("Login","Home");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
 

        public ActionResult Info(int id)
        {
            var infoexam = db.Exam.Find(id);
            if (infoexam == null)
            {
                return HttpNotFound();
            }

            return View("Info", infoexam);
        }

        public ActionResult StartExam(int id)
        {
            var detail = db.Exam.Where(i => i.Id == id)
               .Select(i => new ExamDetailsModel()
               {
                   ExamId = i.Id,
                   ExamNumber = i.ExamNumber,
                   SinavAdi = i.SinavAdi,
                   BaslangicSaati = i.BaslangicSaati,
                   BitisSaati = i.BitisSaati,
                   SinavSuresi = i.SinavSuresi,
                   KacinciSinif = i.KacinciSinif,
                   
                   ExamLines = i.ExamLines.Select(a => new ExamLinesModel()
                   {
                       ExamId = a.ExamId,
                       SoruId = a.SoruId,
                       SoruAdi = a.SoruAdi,
                       DogruCevap = a.DogruCevap,
                       YanlisCevap1 = a.YanlisCevap1,
                       YanlisCevap2 = a.YanlisCevap2,
                       YanlisCevap3 = a.YanlisCevap3,
                       YanlisCevap4 = a.YanlisCevap4,
                       Puan = a.Puan,
                       ZorlukDerecesi = a.ZorlukDerecesi


                   }).ToList()
                   
               }).FirstOrDefault();
            TempData["StartExam"] = detail.SinavSuresi;
            return View(detail);
        }

        [HttpPost]
        public ActionResult StartExam(GirilenSinavlar girilen)
        {

            db.GirilenSinavlars.Add(girilen);
            db.SaveChanges();
            return RedirectToAction("EndOfExam");
        }
        public ActionResult EndOfExam()
        {
            return View();
        }

        public ActionResult historyExam()
        {
            var exams_s = db.GirilenSinavlars.Include(s=>s.Exams);
            return View(exams_s.ToList());
        }
 
    }
}

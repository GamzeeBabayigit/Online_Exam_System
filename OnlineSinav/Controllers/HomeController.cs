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
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        DbOnlineSinav db = new DbOnlineSinav();

        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Ogrenci ogr)
        {
            try
            {
                var varmi = db.Ogrencis.Where(i => i.OgrenciNo == ogr.OgrenciNo).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                }
                if (varmi.TC == ogr.TC)
                {
                    Session["username"] = varmi.AdSoyad;
                    Session["Id"] = varmi.Id;
                    return RedirectToAction("Index", "Ogrenci");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginOgr()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginOgr(Ogretmen ogretmen)
        {
            try
            {
                var varmi = db.Ogretmens.Where(i => i.TC == ogretmen.TC).SingleOrDefault();
                if (varmi == null)
                {
                    return View();
                }
                if (varmi.Sifre == ogretmen.Sifre)
                {
                    Session["username"] = varmi.AdSoyad;
                    Session["Id"] = varmi.Id;
                    Session["Alani"] = varmi.Alani;
                    Session["Mail"] = varmi.Email;
                    return RedirectToAction("Process", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Ogretmen ogretmen)
        {
            db.Ogretmens.Add(ogretmen);
            db.SaveChanges();
            return RedirectToAction("LoginOgr", "Home");
        }

        public ActionResult Process(Soru model)
        {
            var soru = db.Sorulars.ToList();
            return View(soru);
        }

        public ActionResult SoruEkle()
        {
            return View("SoruEkle");
        }
        [HttpPost]

        public ActionResult SoruEkle(Soru sorular)
        {
            db.Sorulars.Add(sorular);
            db.SaveChanges();

            TempData["CreateQuestion"] = sorular;
            return RedirectToAction("Process");
        }
        public ActionResult Sil(int id)
        {
            var deletequestion = db.Sorulars.Find(id);
            if (deletequestion == null)
                return HttpNotFound();
            db.Sorulars.Remove(deletequestion);
            db.SaveChanges();
            return RedirectToAction("Process");
        }


        public ActionResult Guncelle(int id)
        {
            var model = db.Sorulars.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("Guncelle", model);
        }

        [HttpPost]
        public ActionResult Guncelle(Soru Soru)
        {
            var updatequestion = db.Sorulars.Find(Soru.Id);
            if (updatequestion == null)
            {
                return HttpNotFound();
            }
            updatequestion.SoruAdi = Soru.SoruAdi;
            updatequestion.DogruCevap = Soru.DogruCevap;
            updatequestion.YanlisCevap1 = Soru.YanlisCevap1;
            updatequestion.YanlisCevap2 = Soru.YanlisCevap2;
            updatequestion.YanlisCevap3 = Soru.YanlisCevap3;
            updatequestion.YanlisCevap4 = Soru.YanlisCevap4;
            updatequestion.ZorlukDerecesi = Soru.ZorlukDerecesi;
            updatequestion.Puan = Soru.Puan;
            updatequestion.Alani = Soru.Alani;
            db.SaveChanges();

            TempData["UpdateQuestion"] = updatequestion;

            return RedirectToAction("Process", "Home");
        }

        public ActionResult AddExam()
        {
            var sorular = db.Sorulars.Include(x => x.Ogretmens);

            return View(sorular.ToList());
        }
        [HttpPost]

        public ActionResult AddExam(Sinav sorular)
        {
            db.Sinavs.Add(sorular);
            db.SaveChanges();
            return RedirectToAction("Process");
        }

        public ActionResult HataliSoru(int id)
        {
            var model = db.Sorulars.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("HataliSoru", model);

        }
        [HttpPost]
        public ActionResult HataliSoru(Soru Soru)
        {
            var updatequestion = db.Sorulars.Find(Soru.Id);
            if (updatequestion == null)
            {
                return HttpNotFound();
            }
            updatequestion.Hatalimi = Soru.Hatalimi;
            updatequestion.HataNedeni = Soru.HataNedeni;
            db.SaveChanges();

           
            List<string> listdata = (from i in db.Ogretmens
                                    where i.Alani == Soru.Alani
                                    select i.Email).ToList();
            int j = 2;

            for (int h = 0; h < j; h++)
            {

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "mail@gmail.com";
                WebMail.Password = "MailSifre"; 
                WebMail.SmtpPort = 587;
                WebMail.Send(
                      listdata[h],
                      subject: "Hatalı Soru Bildirimi",
                      body: "Sistemde hatalı bir soru tespit edilmiştir. Lütfen siz de sistemdeki soruyu inceleyerek hatalı olup olmadığı konusunda yorum yapınız. İyi çalışmalar dileriz.",
                      replyTo: Session["Mail"].ToString()

                    );
            }
            return RedirectToAction("Process", "Home");
        }
        public ActionResult HataliSoruList(Soru model)
        {
            var soru = db.Sorulars.ToList();
            return View(soru);
        }

        public ActionResult HataliSoruDegerlendir(int id)
        {
            var errorquestion = db.Sorulars.Find(id);
        
            ViewBag.soru = errorquestion.Id.ToString();
            if (errorquestion == null)
                return HttpNotFound();
            return View();
        }

        [HttpPost]
        public ActionResult HataliSoruDegerlendir(HataliSoruDegerlendirme hata)
        {
            db.HataliSoruDegerlendirmes.Add(hata);
            db.SaveChanges();
            return RedirectToAction("Process");
        }

        public ActionResult HataliDegerlendirmeSonuc(int id)
        {
            var hatalisorular = db.HataliSoruDegerlendirmes.Include(s => s.Soru)
                .Include(s => s.Ogretmen);
            var errorquestion = db.Sorulars.Find(id);
            ViewBag.soruhatali = errorquestion.Id.ToString();
            if (errorquestion == null)
                return HttpNotFound();
            return View(hatalisorular.ToList());
            
        }

        public ActionResult MyExamList()
        {
            var username=Session["username"];
            var exa = db.Exam
                .Where(i => i.OgretmenAdi == i.OgretmenAdi)
                .Select(i => new ExamModel()
                {
                    Id = i.Id,
                    ExamNumber=i.ExamNumber,
                    BaslangicSaati = i.BaslangicSaati.ToString(),
                    BitisSaati = i.BitisSaati.ToString(),
                    SinavAdi=i.SinavAdi,
                    SinavSuresi=i.SinavSuresi,
                    OgretmenAdi=i.OgretmenAdi

                }).OrderByDescending(i=>i.Id).ToList();
            return View(exa);
        }

        public ActionResult DetailsExam(int id)
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
                        ExamId=a.ExamId,
                        SoruId = a.SoruId,
                        SoruAdi=a.SoruAdi,
                        DogruCevap=a.DogruCevap,
                        YanlisCevap1=a.YanlisCevap1,
                        YanlisCevap2=a.YanlisCevap2,
                        YanlisCevap3=a.YanlisCevap3,
                        YanlisCevap4=a.YanlisCevap4,
                        Puan=a.Puan,
                        ZorlukDerecesi=a.ZorlukDerecesi

                       
                    }).ToList()
                }).FirstOrDefault();

            return View(detail);
        }

        public ActionResult Students()
        {
            var exams_Student = db.GirilenSinavlars
                .Include(m => m.Ogrencis)
                .Where(i=>i.ExamId==i.ExamId)
                .Select(i => new ExamDoneModel()
                {
                    
                    ExamId=i.ExamId,
                    ExamNumber=i.Exams.ExamNumber,
                    SinavAdi=i.Exams.SinavAdi,
                    SinavTarih=i.Exams.BaslangicSaati

                }).Distinct().OrderByDescending(i=>i.SinavTarih).ToList();
            return View(exams_Student);
        }
        public ActionResult DetailsStudent(int id)
        {
            var details = db.GirilenSinavlars.Where(i => i.ExamId == id).Include(m => m.Ogrencis);
            return View(details.ToList());
        }
    }
}

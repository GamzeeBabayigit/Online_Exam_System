using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using OnlineSinav.Models;

namespace OnlineSinav.Models
{
    public class DbOnlineSinav : DbContext
    {
        public  DbSet<Ogrenci> Ogrencis { get; set; }
        public  DbSet<Ogretmen> Ogretmens { get; set; }
        public  DbSet<Sinav> Sinavs { get; set; }
        public  DbSet<SistemYoneticisi> SistemYoneticisis { get; set; }
        public  DbSet<Soru> Sorulars { get; set; }
        public  DbSet<SinavNotlari> SinavNotlaris { get; set; }
        public  DbSet<SinavSorulari> SinavSorularis { get; set; }
        public DbSet<HataliSoruDegerlendirme> HataliSoruDegerlendirmes { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamLinesModels> ExamLine { get; set; }

        public DbSet<GirilenSinavlar> GirilenSinavlars { get; set; }
        public DbOnlineSinav() : base("DbOnlineSinav")
        {
            Database.SetInitializer(new Veritabaniolusturucu());
        }

        public class Veritabaniolusturucu : CreateDatabaseIfNotExists<DbOnlineSinav>
        {
            protected override void Seed(DbOnlineSinav context)
            {
                base.Seed(context);
            }

        }
    }
}
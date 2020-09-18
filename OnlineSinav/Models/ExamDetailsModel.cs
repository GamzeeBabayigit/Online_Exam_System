using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    public class ExamDetailsModel
    {

        public int ExamId { get; set; }
        public string ExamNumber { get; set; }

        public string SinavAdi { get; set; }
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public string OgretmenAdi { get; set; }
        public string KacinciSinif { get; set; }
        public double SinavSuresi { get; set; }
        public virtual List<ExamLinesModel> ExamLines { get; set; }

    }

    public class ExamLinesModel
    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public int SoruId { get; set; }
        public virtual Soru Soru { get; set; }
        public string SoruAdi { get; set; }
        public string DogruCevap { get; set; }
        public string YanlisCevap1 { get; set; }
        public string YanlisCevap2 { get; set; }
        public string YanlisCevap3 { get; set; }
        public string YanlisCevap4 { get; set; }
        public string ZorlukDerecesi { get; set; }
        public double Puan { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    public class GirilenSinavlar
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int OgrenciId { get; set; }
        public double total { get; set; }

        public virtual Exam Exams { get; set; }
        public virtual Ogrenci Ogrencis { get; set; }
    }
}
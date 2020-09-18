using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    public class ExamDoneModel
    {
        public int Id { get; set; }
        public int ExamId { get; set; }

        public string SinavAdi { get; set; }
        public DateTime SinavTarih { get; set; }
        public string ExamNumber { get; set; }
    }
}
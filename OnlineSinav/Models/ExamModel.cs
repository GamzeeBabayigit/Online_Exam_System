using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    public class ExamModel
    {
        public int Id { get; set; }
        public string ExamNumber { get; set; }
        public string SinavAdi { get; set; }
        public string BaslangicSaati { get; set; }
        public string BitisSaati { get; set; }
        public string ZorlukSeviyesi { get; set; }
        public double SinavSuresi { get; set; }
        public string OgretmenAdi { get;set; }
        public int OgrenciId { get; set; }
    }
}
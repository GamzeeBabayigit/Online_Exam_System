using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OnlineSinav.Models
{
    [Table("Ogrenci")]
    public class Ogrenci
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TC { get; set; }
        public string AdSoyad { get; set; }
        public string OgrenciNo { get; set; }
        public string Sinif { get; set; }

        public List<SinavNotlari> SinavNotlari { get; set; }
        public List<Ogretmen> Ogretmen { get; set; }
        public virtual ICollection<SinavNotlari> SinavNotları { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OnlineSinav.Models
{
    [Table("Ogretmen")]
    public class Ogretmen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TC { get; set; }
        public string AdSoyad { get; set; }
        public string Sifre { get; set; }
        public string Alani { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Soru> Sorular { get; set; }

        public Nullable<int> OgrenciId { get; set; }
        public virtual Ogrenci ogrenci { get; set; }

        public virtual ICollection<HataliSoruDegerlendirme> HataliSoruDegerlendirmes { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OnlineSinav.Models
{
    [Table("Sinav")]
    public class Sinav
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SinavAdi { get; set; }
        public DateTime BaslangicSaati { get; set; }
        public DateTime BitisSaati { get; set; }
        public string OgretmenAdi { get; set; }
        public string KacinciSinif { get; set; }
        public double SinavSuresi { get; set; }
        public Boolean Active { get; set; }
        public virtual ICollection<Soru> Soru { get; set; }       
        public virtual ICollection<SinavNotlari> SinavNotları { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OnlineSinav.Models
{
    [Table("Soru")]
    public class Soru
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public string SoruAdi { get; set; }
 
        public string DogruCevap { get; set; }
 
        public string YanlisCevap1 { get; set; }
         
        public string YanlisCevap2 { get; set; }

        public string YanlisCevap3 { get; set; }

        public string YanlisCevap4 { get; set; }

        public string ZorlukDerecesi { get; set; }

        public double Puan { get; set; }


        public string Alani { get; set; }
        public Boolean Hatalimi { get; set; }
        public string HataNedeni { get; set; }
        public Nullable<int> exam_id { get; set; }

        public Nullable<int> OgretmenId { get; set; }
        public virtual Ogretmen Ogretmens { get; set; }

        public Nullable<int> SinavSorulariId { get; set; }
        public virtual SinavSorulari SinavSorularis { get; set; }

        public virtual Sinav Sinav { get; set; }

        public virtual ICollection<HataliSoruDegerlendirme> HataliSoruDegerlendirmes { get; set; }
    }
}
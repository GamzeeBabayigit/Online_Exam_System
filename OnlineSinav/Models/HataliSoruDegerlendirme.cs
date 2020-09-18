using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    [Table("HataliSoruDegerlendirme")]

    public class HataliSoruDegerlendirme
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        
        public Boolean Cevap { get; set; }
        public string Nedeni { get; set; }

        public int SoruId { get; set; }
        public Soru Soru { get; set; }
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }
    }

}
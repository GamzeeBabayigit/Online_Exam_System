using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    [Table("SinavNotlari")]

    public class SinavNotlari
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> OgrenciId { get; set; }
          
        public Nullable<int> SinavId { get; set; }
          
        public Nullable<int> Total { get; set; }

        public string grade { get; set; }
        public string quality { get; set; }


        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Sinav Sinav { get; set; }
    }
}
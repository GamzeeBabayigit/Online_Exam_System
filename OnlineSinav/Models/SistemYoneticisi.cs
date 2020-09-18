using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace OnlineSinav.Models
{
    [Table("SistemYoneticisi")]
    public class SistemYoneticisi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TC { get; set; }
        public string AdSoyad { get; set; }
        public string Sifre { get; set; }
    }
}
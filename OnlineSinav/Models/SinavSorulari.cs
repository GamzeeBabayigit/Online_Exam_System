using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineSinav.Models
{
    [Table("SinavSorulari")]
    public class SinavSorulari
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int SinavId { get; set; }
        public int SinavSorusuId { get; set; }
        public virtual List<Soru> Sorular { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebIamge.Models
{
    [Table("Manage")]
    public class Manage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NameImage { get; set; }
        
        public string Image { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
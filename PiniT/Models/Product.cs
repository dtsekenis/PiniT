using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Category")]
        [DisplayName("Category")]
        public string CategoryId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0,70000)]
        public decimal Price { get; set; }

        [ForeignKey("Restaurant")]
        public string ServedAt { get;set; }
       
        public virtual Restaurant Restaurant { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
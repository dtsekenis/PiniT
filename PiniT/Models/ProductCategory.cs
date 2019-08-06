using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Category")]
        [MinLength(1,ErrorMessage = "Value can't be Empty!")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }  
    }
}
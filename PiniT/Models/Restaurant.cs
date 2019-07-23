using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
            Menu = new HashSet<Product>();
            Type = new HashSet<RestaurantType>();
            Tables = new HashSet<Table>();
        }

        [ForeignKey("Manager")]
        public string RestaurantId { get; set; }

        [Required]
        [DisplayName("Restaurant")]
        public string CompanyName { get; set; }

        [Required]
        [DisplayName("VAT Number")]
        public string VAT { get; set; }

        public virtual ICollection<Product> Menu { get; set; }
        public virtual ICollection<RestaurantType> Type { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
        public virtual PiniTManager Manager { get; set; }

    }
}
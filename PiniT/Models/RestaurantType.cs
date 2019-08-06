using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PiniT.Models
{
    public class RestaurantType
    {
        public RestaurantType()
        {
            Restaurants = new HashSet<Restaurant>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Type")]
        [MinLength(1, ErrorMessage = "Value can't be Empty!")]
        public string Name { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
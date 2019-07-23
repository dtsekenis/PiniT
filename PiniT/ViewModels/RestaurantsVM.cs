using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiniT.ViewModels
{
    public class IndexRestaurantsVM
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Search { get; set; }
        public string Type { get; set; }
    }
}
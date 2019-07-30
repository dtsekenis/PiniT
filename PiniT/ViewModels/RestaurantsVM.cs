using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiniT.ViewModels
{
    public class IndexRestaurantsVM
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Search { get; set; }
        public string Type { get; set; }
    }

    public class CreateRestaurantVM
    {
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        List<string> _selectedTypes;
        public List<string> SelectedTypes
        {
            get
            {
                if (_selectedTypes == null)
                {
                    _selectedTypes = Restaurant.Type.Select(x => x.Name).ToList();
                }
                return _selectedTypes;
            }
            set
            {
                _selectedTypes = value;
            }
        }
    }
}
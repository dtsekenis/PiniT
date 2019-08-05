using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiniT.Models;

namespace PiniT.ViewModels
{
    public class HomeIndexVM
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }

        public IEnumerable<Table> TableCount { get; set; }
    }
}
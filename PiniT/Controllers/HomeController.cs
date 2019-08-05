using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiniT.Managers;
using PiniT.ViewModels;

namespace PiniT.Controllers
{
    public class HomeController : Controller
    {
        RestaurantManager restDb = new RestaurantManager();
        ReservationManager resDb = new ReservationManager();
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                return RedirectToAction("ManagerIndex", "Tables");
            }
            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("CustomerIndex", "Restaurants");
            }

            var restaurants = restDb.GetRestaurantsFull();      
            HomeIndexVM vm = new HomeIndexVM
            {
                Restaurants = restaurants
            };
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
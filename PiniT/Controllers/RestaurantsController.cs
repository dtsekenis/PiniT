using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PiniT.Models;
using PiniT.ViewModels;

namespace PiniT.Controllers
{
    [Authorize]
    public class RestaurantsController : Controller
    {
        private RestaurantManager db = new RestaurantManager();


        //Not Finished
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerIndex()
        {
            if (!HasRestaurant())
            {
               return RedirectToAction("Create");
            }
            return RedirectToAction("ManagerIndex", "Tables");
        }

        //Αγαπημενο μου ημερολογιο. Σημερα δοκιμασα το gitHub και εγω και η Σταυρουλα πιστευουμε πως θα δουλεψει
        //Not Finished
        [Authorize(Roles ="Customer")]
        public ActionResult CustomerIndex(string search, string type)
        {
            IndexRestaurantsVM vm = new IndexRestaurantsVM
            {
                Restaurants = db.GetRestaurantsFull(),
                Search = search,
                Type = type
            };
            return View(vm);
        }

        //Tested
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }


        //Tested
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }

            restaurant.RestaurantId = User.Identity.GetUserId();

            db.CreateRestaurant(restaurant);

            return RedirectToAction("Index", "Home");
        }


        //Tested
        [Authorize(Roles = "Manager")]
        public ActionResult Edit()
        {

            var managerId = User.Identity.GetUserId();
            Restaurant restaurant = db.GetRestaurant(managerId);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }


        //Tested
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return View(restaurant);
            }
            if(User.Identity.GetUserId() != restaurant.RestaurantId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            db.UpdateRestaurant(restaurant);
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete()
        {
            var managerId = User.Identity.GetUserId();
            Restaurant restaurant = db.GetRestaurant(managerId);

            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed()
        {
            var restId = User.Identity.GetUserId();
            db.DeleteRestaurant(restId);

            return RedirectToAction("Index", "Home");
        }




        public bool HasRestaurant()
        {
            Restaurant restaurant = db.GetRestaurant(User.Identity.GetUserId());
            if (restaurant == null)
            {
                return false;
            }
            return true;
        }
    }
}
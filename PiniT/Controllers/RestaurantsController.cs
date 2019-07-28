using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PiniT.ViewModels;

namespace PiniT.Controllers
{
    //Need to check views and add js/ajax if needed
    [Authorize]
    public class RestaurantsController : Controller
    {
        private RestaurantManager db = new RestaurantManager();
        private RestaurantTypeManager typeDb = new RestaurantTypeManager();


        //Not Finished// Not sure
        //Maybe restaurant overview with all tables/products/reservations and data
        [Authorize(Roles = "Manager")]
        public ActionResult ManagerIndex()
        {
            Restaurant restaurant = db.GetRestaurantFull(User.Identity.GetUserId());
            if (restaurant == null)
            {
               return RedirectToAction("Create");
            }
            return View(restaurant);
        }

        //Not Finished
        [Authorize(Roles ="Customer")]
        public ActionResult CustomerIndex(string search, string type)
        {
            ViewBag.RestaurantTypes = new SelectList(typeDb.GetRestaurantTypes(), "Name", "Name");
            IndexRestaurantsVM vm = new IndexRestaurantsVM
            {
                Restaurants = db.GetRestaurantsFull(search,type),
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

            return RedirectToAction("ManagerIndex");
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
            return RedirectToAction("ManagerIndex");
        }

        //Not sure if needed
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


        //not sure if needed
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed()
        {
            var restId = User.Identity.GetUserId();
            db.DeleteRestaurant(restId);

            return RedirectToAction("ManagerIndex");
        }
    }
}
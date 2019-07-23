using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PiniT.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class TablesController : Controller
    {
        private TableManager db = new TableManager();
        public ActionResult ManagerIndex()
        {
            if (User.IsInRole("Manager"))
            {
                var userId = User.Identity.GetUserId();
                var restaurantTables = db.GetTables(userId);
                return View(restaurantTables);
            }
            var allTables = db.GetAllTables();
            return View(allTables);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }

            //Ask Vyron which is better
            table.RestaurantId = User.Identity.GetUserId();
            db.CreateTable(table);

            return RedirectToAction("ManagerIndex");
        }


        public ActionResult Edit(int id)
        {
            Table table = db.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }

            if (table.RestaurantId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(table);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }

          //table.RestaurantId = User.Identity.GetUserId();
            db.UpdateTable(table);

            return RedirectToAction("ManagerIndex");
        }

        public ActionResult Delete(int id)
        {
            Table table = db.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            if (table.RestaurantId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(table);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteTable(id);

            return RedirectToAction("ManagerIndex");
        }

        //This will be completed with AJAX
        public ActionResult ToggleBooked(int id)
        {
            Table table = db.GetTable(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            if(table.RestaurantId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            db.ToggleIsBooked(id);

            return RedirectToAction("ManagerIndex");
        }
    }
}
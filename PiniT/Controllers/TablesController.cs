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
 
    public class TablesController : Controller
    {
        private TableManager db = new TableManager();

        [Authorize(Roles ="Customer")]
        public ActionResult CustomerIndex(string id)
        {
            var tables = db.GetAvailableTables(id);
            return View(tables);
        }

        [Authorize(Roles = "Manager,Admin")]
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

        [Authorize(Roles = "Manager,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }
            table.RestaurantId = User.Identity.GetUserId();
            db.CreateTable(table);

            return RedirectToAction("ManagerIndex");
        }


        [Authorize(Roles = "Manager,Admin")]
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


        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Table table)
        {
            if (!ModelState.IsValid)
            {
                return View(table);
            }
            db.UpdateTable(table);

            return RedirectToAction("ManagerIndex");
        }


        [Authorize(Roles = "Manager,Admin")]
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

        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteTable(id);

            return RedirectToAction("ManagerIndex");
        }

        //This will be completed with AJAX
        [Authorize(Roles = "Manager,Admin")]
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
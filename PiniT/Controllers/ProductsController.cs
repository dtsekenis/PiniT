using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using PiniT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PiniT.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ProductManager db = new ProductManager();
        private ProductCategoryManager pcDb = new ProductCategoryManager();
        private RestaurantManager restDb = new RestaurantManager();

        [Authorize(Roles = "Manager")]
        public ActionResult ManagerIndex(string search, string category)
        {
            Restaurant restaurant = restDb.GetRestaurantFull(User.Identity.GetUserId());
            if (restaurant == null)
            {
                return RedirectToAction("Create", "Restaurants");
            }
            var userId = User.Identity.GetUserId();
            ViewBag.Categories = new SelectList(pcDb.GetProductCategories(), "Name", "Name");
            IndexProductsVM vm = new IndexProductsVM
            {
                Products = db.GetProducts(userId, search, category),
                Search = search,
                Category = category,
                Categories = pcDb.GetProductCategoriesFull()
            };

            return View(vm);
        }

        [Authorize(Roles = "Customer")] 
        public ActionResult CustomerIndex(string id,string search,string category)
        {
            ViewBag.Categories = new SelectList(pcDb.GetProductCategories(), "Name", "Name");
            IndexProductsVM vm = new IndexProductsVM
            {
                Products = db.GetProducts(id, search, category),
                Search = search,
                Category = category,
                Categories = pcDb.GetProductCategories()
            };
            Restaurant restaurant = restDb.GetRestaurant(id);
            ViewBag.Title = $"{restaurant.CompanyName} Menu.";
            return View(vm);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(pcDb.GetProductCategories(), "Name", "Name");

            return View();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.ServedAt = User.Identity.GetUserId();
            db.CreateProduct(product);


            return RedirectToAction("ManagerIndex");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int id)
        {
            Product product = db.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != product.ServedAt)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            ViewBag.Categories = new SelectList(pcDb.GetProductCategories(), "Name", "Name");
            TempData["RestaurantId"] = product.ServedAt;
            return View(product);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            product.ServedAt = (string)TempData["RestaurantId"];
            db.UpdateProduct(product);

            return RedirectToAction("ManagerIndex");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id)
        {
            Product product = db.GetProduct(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.GetUserId() != product.ServedAt)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(product);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteProduct(id);
            return RedirectToAction("ManagerIndex");
        }
    }
}
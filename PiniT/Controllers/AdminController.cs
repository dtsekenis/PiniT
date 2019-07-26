using PiniT.Managers;
using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiniT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ManagerContext manDb = new ManagerContext();
        private CustomerContext cusDb = new CustomerContext();
        private ProductCategoryManager pcDb = new ProductCategoryManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategoryIndex()
        {
            var categories = pcDb.GetProductCategories();
            return View(categories);
        }

        public ActionResult CreateProductCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductCategory(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            pcDb.CreateProductCategory(category);
            return RedirectToAction("ProductCategoryIndex");
        }

        public ActionResult CustomersIndex()
        {
            var customers = cusDb.GetCustomers();

            return View(customers);
        }

        public ActionResult EditCustomer(string id)
        {
            PiniTCustomer customer = cusDb.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(PiniTCustomer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            cusDb.UpdateCustomer(customer);

            return RedirectToAction("CustomersIndex");
        }

        public ActionResult DeleteCustomer(string id)
        {
            PiniTCustomer customer = cusDb.GetCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteCustomer")]
        public ActionResult DeleteConfirmedCustomer(string id)
        {
            cusDb.DeleteCustomer(id);

            return RedirectToAction("CustomersIndex");
        }

        public ActionResult ManagersIndex()
        {
            var managers = manDb.GetManagers();
            return View(managers);
        }

        public ActionResult EditManager(string id)
        {
            PiniTManager manager = manDb.GetManager(id);
            if (manager == null)
            {
                return HttpNotFound();
            }

            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManager(PiniTManager manager)
        {
            if (!ModelState.IsValid)
            {
                return View(manager);
            }
            manDb.UpdateManager(manager);
            return RedirectToAction("ManagersIndex");
        }
        public ActionResult DeleteManager(string id)
        {
            PiniTManager manager = manDb.GetManager(id);
            if (manager == null)
            {
                return HttpNotFound();
            }

            return View(manager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteManager")]
        public ActionResult DeleteConfirmedManager(string id)
        {
            manDb.DeleteManager(id);
            return RedirectToAction("ManagersIndex");
        }


        //Needs to be executed with Ajax

        public ActionResult ToggleAuthorize(string id)
        {
            bool response;
            response = manDb.ToggleRestaurantAuthorize(id);
            return RedirectToAction("ManagersIndex");
        }
    }
}
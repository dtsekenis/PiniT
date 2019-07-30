using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using PiniT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiniT.Controllers
{
    [Authorize]
    public class AccountWalletsController : Controller
    {
        AccountWalletManager db = new AccountWalletManager();
        
        [Authorize(Roles = "Customer")]
        public ActionResult AddCredits()
        {
            AccountWallet wallet = db.GetAccountWallet(User.Identity.GetUserId());
            if (wallet == null)
            {
                return HttpNotFound();
            }
            AddCreditsAccountWalletVM vm = new AddCreditsAccountWalletVM
            {
                Wallet = wallet

            };
            return View(vm);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCredits(AddCreditsAccountWalletVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            db.AddCredits(vm.Wallet.Id, vm.AmountToBeAdded);
            return RedirectToAction("CustomerIndex", "Restaurants");
        }
    }
}
using Microsoft.AspNet.Identity;
using PiniT.Managers;
using PiniT.Models;
using PiniT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace PiniT.Controllers
{
    [Authorize]
    public class AccountWalletsController : Controller
    {
        AccountWalletManager db = new AccountWalletManager();
        

        public ActionResult Index()
        {
            AccountWallet wallet = db.GetAccountWallet(User.Identity.GetUserId());
            if (wallet == null)
            {
                return HttpNotFound();
            }
            return View(wallet);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult AddCredits()
        {
            AccountWallet wallet = db.GetAccountWallet(User.Identity.GetUserId());
            if (wallet == null)
            {
                return HttpNotFound();
            }
            if (wallet.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
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
            if (vm.Wallet.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            db.AddCredits(vm.Wallet.Id, vm.AmountToBeAdded);
            return RedirectToAction("Index");
        }
    }
}
using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class AccountWalletManager
    {
        public ICollection<AccountWallet> GetAccountWallets()
        {
            ICollection<AccountWallet> wallets;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                wallets = db.AccountWallets.Include("User").ToList();
            }
            return wallets;
        }
        public AccountWallet GetAccountWallet(string id)
        {
            AccountWallet wallet;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                wallet = db.AccountWallets.Include("User").FirstOrDefault(x => x.Id == id);
            }
            return wallet;
        }
        public bool CreateAccountWallet(AccountWallet wallet)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.AccountWallets.Find(wallet.Id) == null)
                {
                    db.AccountWallets.Add(wallet);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
        }
        public void UpdateAccountWallet(AccountWallet wallet)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.AccountWallets.Attach(wallet);
                db.Entry(wallet).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public bool PayFee(decimal payment, string customerId, string restaurantId)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                AccountWallet customerWallet = db.AccountWallets.Find(customerId);
                AccountWallet restaurantWallet = db.AccountWallets.Find(restaurantId);

                if (customerWallet != null && restaurantWallet != null)
                {
                    customerWallet.Credits -= payment;
                    restaurantWallet.Credits += payment;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public bool AddCredits(string walletId, decimal credits)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                AccountWallet wallet = db.AccountWallets.Find(walletId);

                if (wallet != null && credits > 0)
                {
                    wallet.Credits += credits;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }


    }
}
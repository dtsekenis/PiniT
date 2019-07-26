using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PiniT.Managers
{
    public class ManagerContext
    {
        private RestaurantManager restDb = new RestaurantManager();
        public ICollection<PiniTManager> GetManagers()
        {
            ICollection<PiniTManager> managers;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                managers = db.Users.OfType<PiniTManager>().Include("Restaurant").ToList();
            }

            return managers;
        }
        public PiniTManager GetManager(string id)
        {
            PiniTManager manager;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                                                
                manager = (PiniTManager)db.Users.Include("Restaurant")
                                                .Where(x => x.Id == id)
                                                .OfType<PiniTManager>()
                                                .FirstOrDefault();
            }
            return manager;
        }
        public void UpdateManager(PiniTManager manager)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Users.Attach(manager);
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public bool DeleteManager(string id)
        {
            
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PiniTManager manager = db.Users.OfType<PiniTManager>().FirstOrDefault(x => x.Id == id);
                if (manager != null)
                {
                    restDb.DeleteRestaurant(id);
                    db.Users.Remove(manager);
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
        public bool ToggleRestaurantAuthorize(string id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                PiniTManager manager = db.Users.OfType<PiniTManager>().FirstOrDefault(x => x.Id == id);
                if (manager != null && manager.RestaurantAuthorized == true)
                {
                    manager.RestaurantAuthorized = false;
                    db.SaveChanges();
                    result = true;
                }
                else if(manager != null && manager.RestaurantAuthorized == false)
                {
                    manager.RestaurantAuthorized = true;
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
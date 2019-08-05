using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class RestaurantTypeManager
    {

        //Not Finished
        public ICollection<RestaurantType> GetRestaurantTypes()
        {
            ICollection<RestaurantType> types;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                types = db.RestaurantTypes.ToList();
            }
            return types;
        }
        public bool CreateRestaurantType(RestaurantType type)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.RestaurantTypes.Find(type.Name) == null)
                {
                    db.RestaurantTypes.Add(type);
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
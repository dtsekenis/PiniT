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
    }
}
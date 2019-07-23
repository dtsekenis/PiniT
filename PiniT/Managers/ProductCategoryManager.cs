using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class ProductCategoryManager
    {
        public ICollection<ProductCategory> GetProductCategories()
        {
            ICollection<ProductCategory> categories;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                categories = db.ProductCategories.ToList();
            }
            return categories;
        }
    }
}
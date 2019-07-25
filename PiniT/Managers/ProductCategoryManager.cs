using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class ProductCategoryManager
    {
        //Not Finished
        public ICollection<ProductCategory> GetProductCategories()
        {
            ICollection<ProductCategory> categories;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                categories = db.ProductCategories.ToList();
            }
            return categories;
        }

       
        public ProductCategory GetProductCategory(string id)
        {
            ProductCategory category;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                category = db.ProductCategories.Include("Products").FirstOrDefault(x => x.Name == id);
            }
            return category;
        }

        public bool CreateProductCategory(ProductCategory category)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (db.ProductCategories.Find(category.Name) == null)
                {
                    db.ProductCategories.Add(category);
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

        public void UpdateProductCategory(ProductCategory category)
        {
           
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.ProductCategories.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }

        }


        public bool DeleteProductCategory(string id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ProductCategory category = db.ProductCategories.Find(id);
                if (category != null)
                {
                    db.ProductCategories.Remove(category);
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
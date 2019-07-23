using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PiniT.Managers
{
    public class ProductManager
    {
        public ICollection<Product> GetProducts(string restaurantId)
        {
            ICollection<Product> products;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                products = db.Products.Include("Category").Where(x=>x.ServedAt == restaurantId).ToList();
            }

            return products;
        }
        public ICollection<Product> GetProducts(string restaurantId,string search, string category)
        {
            ICollection<Product> products;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Products.Where(x=>x.ServedAt == restaurantId).Include("Category").AsQueryable();

                if (!String.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.Name.Contains(search));
                }
                if (!String.IsNullOrEmpty(category))
                {
                    query = query.Where(x => x.CategoryId == category);
                }
                products = query.ToList();
            }
            return products;
        }
        public Product GetProduct(int id)
        {
            Product product;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                product = db.Products.Include("Category")
                                     .FirstOrDefault(x=>x.ProductId == id);
            }
            return product;
        }
        public bool CreateProduct(Product product)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                if (db.Products.Find(product.ProductId) == null)
                {
                    db.Products.Add(product);
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
        public void UpdateProduct(Product product)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                db.Products.Attach(product);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();   
            }
        }
        public bool DeleteProduct(int id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Product product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
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
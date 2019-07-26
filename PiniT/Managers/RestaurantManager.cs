using PiniT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PiniT.Managers
{
    public class RestaurantManager
    {
        public ICollection<Restaurant> GetRestaurants()
        {
            ICollection<Restaurant> restaurants;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                restaurants = db.Restaurants.ToList();
            }
            return restaurants;
        }
        public ICollection<Restaurant> GetRestaurantsFull()
        {
            ICollection<Restaurant> restaurants;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                restaurants = db.Restaurants.Include("Type")
                                            .Include("Menu")
                                            .Include("Tables")
                                            .Include("Manager")
                                            .ToList();
            }
            return restaurants;
        }
        public Restaurant GetRestaurant(string id)
        {
            Restaurant restaurant;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                restaurant = db.Restaurants.Find(id);
            }
            return restaurant;
        }
        public Restaurant GetRestaurantFull(string id)
        {
            Restaurant restaurant;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                restaurant = db.Restaurants.Include("Type")
                                           .Include("Menu")
                                           .Include("Tables")
                                           .Include("Manager")
                                           .FirstOrDefault(x => x.RestaurantId == id);
            }
            return restaurant;
        }
        public bool CreateRestaurant(Restaurant restaurant)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Restaurant rest = db.Restaurants.Find(restaurant.RestaurantId);
                if(rest == null)
                {
                    db.Restaurants.Add(restaurant);
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
        public void UpdateRestaurant(Restaurant restaurant)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Restaurants.Attach(restaurant);
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();

            }
        }
        public bool DeleteRestaurant(string id)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Restaurant restaurant = db.Restaurants.Find(id);
                if (restaurant != null)
                {
                    var restaurantTables = db.Tables.Where(x => x.RestaurantId == restaurant.RestaurantId);
                    var restaurantProducts = db.Products.Where(x => x.ServedAt == restaurant.RestaurantId);
                    foreach (Table table in restaurantTables)
                    {
                        db.Tables.Remove(table);
                    }
                    foreach (Product product in restaurantProducts)
                    {
                        db.Products.Remove(product);
                    }
                    db.Restaurants.Remove(restaurant);
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
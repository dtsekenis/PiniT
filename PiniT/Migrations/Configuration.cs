namespace PiniT.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PiniT.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PiniT.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PiniT.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            ApplicationUserManager userManager = new ApplicationUserManager(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            //Create Role "Manager"
            string restManagerRoleName = "Manager";
            IdentityRole restManagerRole = roleManager.FindByName(restManagerRoleName);
            
            if (restManagerRole == null)
            {
                restManagerRole = new IdentityRole(restManagerRoleName);
                roleManager.Create(restManagerRole);
            }

            //Create Role "Admin"
            string adminRoleName = "Admin";
            IdentityRole adminRole = roleManager.FindByName(adminRoleName);

            if(adminRole == null)
            {
                adminRole = new IdentityRole(adminRoleName);
                roleManager.Create(adminRole);
            }
            //Create Role "Customer"
            string customerRoleName = "Customer";
            IdentityRole customerRole = roleManager.FindByName(customerRoleName);

            if (customerRole == null)
            {
                customerRole = new IdentityRole(customerRoleName);
                roleManager.Create(customerRole);
            }

            //Add Managers
            string restManagerUserName_1 = "Manager1";
            string restManagerEmail_1 = "manager1@gmail.com";
            string restManagerPassword_1 = "iamthemanager1";

            string restManagerUserName_2 = "Manager2";
            string restManagerEmail_2 = "manager2@gmail.com";
            string restManagerPassword_2 = "iamthemanager2";
            
            PiniTManager restManager1 = (PiniTManager)userManager.FindByName(restManagerUserName_1);
            if (restManager1 == null)
            {
                restManager1 = new PiniTManager
                {
                    UserName = restManagerEmail_1,
                    Email = restManagerEmail_1
                };
                userManager.Create(restManager1, restManagerPassword_1);
            }

            if (!userManager.IsInRole(restManager1.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager1.Id, restManagerRole.Name);
            }
            PiniTManager restManager2 = (PiniTManager)userManager.FindByName(restManagerUserName_2);
            if (restManager2 == null)
            {
                restManager2 = new PiniTManager
                {
                    UserName = restManagerEmail_2,
                    Email = restManagerEmail_2
                };
                userManager.Create(restManager2, restManagerPassword_2);
            }

            if (!userManager.IsInRole(restManager2.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager2.Id, restManagerRole.Name);
            }
            //Add Admin
            string adminUserName = "Tasos";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "iamtheadmin";

            ApplicationUser admin = userManager.FindByName(adminUserName);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                userManager.Create(admin, adminPassword);
            }

            if (!userManager.IsInRole(admin.Id, adminRole.Name))
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
            }
            
            




            //Add Products
            //ProductCategory main = new ProductCategory { Name = "Main dishes" };
            //Product p1 = new Product()
            //{
            //    Name = "Pizza Volcano",
            //    Category = main,
            //    Description = "Mozzarella,Tomato,Jalapenos,Chili Sause",
            //    Price = 11
            //};
            //context.Products.AddOrUpdate(x => x.Name, p1);


            //Add Tables
            Table t1 = new Table { Size = 4, Name = "A1" };
            Table t2 = new Table { Size = 6, Name = "A2" };
            Table t3 = new Table { Size = 8, Name = "A3" };

            context.Tables.AddOrUpdate(x => x.TableId, t1, t2, t3);


            //Add Restaurants
            Restaurant restaurant1 = new Restaurant
            {
                CompanyName = "Restaurant1",
                VAT = "VAT1",
                Manager = restManager1
                
            };
            context.Restaurants.AddOrUpdate(x=>x.RestaurantId, restaurant1);
            restaurant1.Tables.Add(t1);
            restaurant1.Tables.Add(t2);
            restaurant1.Tables.Add(t3);

          //  context.SaveChanges();


        }
    }
}

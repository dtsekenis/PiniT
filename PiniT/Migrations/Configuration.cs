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

            //Add Product Categories
            #region MyRegion
            ProductCategory appetizer = new ProductCategory();
            appetizer.Name = "Appetizers";

            ProductCategory salads = new ProductCategory();
            salads.Name = "Salads";

            ProductCategory main = new ProductCategory();
            main.Name = "Main Dishes";

            ProductCategory drinks = new ProductCategory();
            drinks.Name = "Drinks";

            ProductCategory dessert = new ProductCategory();
            dessert.Name = "Desserts";
            #endregion






            //Add Products
            #region Products
            Product p1 = new Product()
            {
                Name = "Pizza Volcano",
                Category = main,
                Description = "Mozzarela,Tomato,Jalapenos,Chilly Sause",
                Price = 11
            };

            Product p2 = new Product()
            {
                Name = "Pizza Margerita",
                Category = main,
                Description = "Mozzarela,Tomato,Basillico",
                Price = 9
            };

            Product p3 = new Product()
            {
                Name = "Pizza Vegeterian",
                Category = main,
                Description = "Mozzarela,Tomato,Olives,Onions,Peppers",
                Price = 10
            };

            Product p4 = new Product()
            {
                Name = "Spaghetti Napolitana",
                Category = main,
                Description = "Tomato Sause,Basillico",
                Price = 6.90m
            };

            Product p5 = new Product()
            {
                Name = "Spaghetti Bolognese",
                Category = main,
                Description = "Tomato Meat Sause",
                Price = 10
            };

            Product p6 = new Product()
            {
                Name = "Spaghetti Carbonara",
                Category = main,
                Description = "Bacon,White Sause",
                Price = 12
            };

            Product p7 = new Product()
            {
                Name = "La Bella Mafia Sallad",
                Category = salads,
                Description = "Lettuce,iceberg,sause",
                Price = 10
            };

            Product p8 = new Product()
            {
                Name = "Ceasars",
                Category = salads,
                Description = "Iceberg,Chicken,Ceasers Sause",
                Price = 7.50m
            };

            Product p9 = new Product()
            {
                Name = "Tiramisu",
                Category = dessert,
                Description = "Espesso Cream,Cacao",
                Price = 5
            };

            Product p10 = new Product()
            {
                Name = "Cheesecake",
                Category = dessert,
                Description = "Cheese Cream with marmelade",
                Price = 5.50m
            };

            Product p11 = new Product()
            {
                Name = "Bruschetta",
                Category = appetizer,
                Description = "Mozzarela,Tomato",
                Price = 4.50m
            };

            Product p12 = new Product()
            {
                Name = "Roasted Potato",
                Category = appetizer,
                Description = "Potato,butter",
                Price = 6.50m
            };

            Product p13 = new Product()
            {
                Name = "White Wine",
                Category = drinks,
                Description = "White Wine Bottle",
                Price = 30
            };

            Product p14 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 30
            };

            Product p15 = new Product()
            {
                Name = "Water",
                Category = drinks,
                Description = "Water Bottle",
                Price = 3
            };

            Product p16 = new Product()
            {
                Name = "Pepsi Colla",
                Category = drinks,
                Description = "Colla Bottle",
                Price = 2.50m
            };

            Product p17 = new Product()
            {
                Name = "Single Espresso",
                Category = drinks,
                Description = "Espresso Coffee",
                Price = 2.50m
            };

            Product p18 = new Product()
            {
                Name = "Double Espresso",
                Category = drinks,
                Description = "Double Espresso Coffee",
                Price = 4.50m
            };

            //Products of Restaurant "Ôhe beautiful Beijing"
            Product p19 = new Product()
            {
                Name = "Chicken Kow",
                Category = main,
                Description = "Chicken with vegetables",
                Price = 15
            };

            Product p20 = new Product()
            {
                Name = "Sweet & Sour Pork",
                Category = main,
                Description = "Pork with sweet & sour pork",
                Price = 9
            };

            Product p21 = new Product()
            {
                Name = "Sesame Chicken",
                Category = main,
                Description = "Chicken with Sesame",
                Price = 10
            };


            Product p25 = new Product()
            {
                Name = "Egg Roll",
                Category = appetizer,
                Description = "Egg Roll",
                Price = 8
            };

            Product p26 = new Product()
            {
                Name = "Buffalo Wing",
                Category = appetizer,
                Description = "Buffalo Wing",
                Price = 7.50m
            };

            Product p27 = new Product()
            {
                Name = "Fried Ice Cream",
                Category = dessert,
                Description = "Fried Ice Cream",
                Price = 5.80m
            };

            Product p28 = new Product()
            {
                Name = "Shrim with Broccolli",
                Category = appetizer,
                Description = "Shrims with Broccolli",
                Price = 8.50m
            };

            Product p29 = new Product()
            {
                Name = "Crab Rangoon",
                Category = appetizer,
                Description = "",
                Price = 9.50m
            };

            Product p30 = new Product()
            {
                Name = "Chicken Nuggets",
                Category = appetizer,
                Description = "Chicken Nuggets 20 pieces",
                Price = 16.50m
            };

            Product p31 = new Product()
            {
                Name = "White Wine",
                Category = drinks,
                Description = "White Wine Bottle",
                Price = 13
            };

            Product p32 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 16
            };

            Product p33 = new Product()
            {
                Name = "Water",
                Category = drinks,
                Description = "Water Bottle",
                Price = 2.50m
            };

            Product p34 = new Product()
            {
                Name = "Pepsi Colla",
                Category = drinks,
                Description = "Colla Bottle",
                Price = 2.50m
            };

            Product p35 = new Product()
            {
                Name = "Soda",
                Category = drinks,
                Description = "Soda 100ml",
                Price = 2.50m
            };

            //Products of Restaurant "God Bless America"
            Product p36 = new Product()
            {
                Name = "Cheeseburger Deluxe",
                Category = main,
                Description = "Simply grilled and tapped with cheese and burger mayo",
                Price = 15.95m
            };

            Product p37 = new Product()
            {
                Name = "Memphis Burger",
                Category = main,
                Description = "Grilled burger topped with BBQ pulled pork",
                Price = 18.95m
            };

            Product p38 = new Product()
            {
                Name = "Classic American",
                Category = main,
                Description = "Grilled with burger mayo",
                Price = 10
            };


            Product p39 = new Product()
            {
                Name = "Mozzarella Dippers",
                Category = appetizer,
                Description = "Crip and golden outside, creamy melted cheese",
                Price = 6.45m
            };

            Product p40 = new Product()
            {
                Name = "Garlic Bread",
                Category = appetizer,
                Description = "With Cheese",
                Price = 4.50m
            };

            Product p41 = new Product()
            {
                Name = "Ice Cream",
                Category = dessert,
                Description = "Strawberry, vanilla, or chocolate flavour",
                Price = 5.80m
            };

            Product p42 = new Product()
            {
                Name = "Chicken Wings",
                Category = appetizer,
                Description = "Chicken Wings",
                Price = 7.50m
            };

            Product p43 = new Product()
            {
                Name = "Boneless Bites",
                Category = appetizer,
                Description = "Crispy pieces of chicken",
                Price = 9.50m
            };

            Product p44 = new Product()
            {
                Name = "Loaded Potato Skins",
                Category = appetizer,
                Description = "Potato skins filled with cheese",
                Price = 7.50m
            };

            Product p45 = new Product()
            {
                Name = "White Wine",
                Category = drinks,
                Description = "White Wine Bottle",
                Price = 13
            };

            Product p46 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 16
            };

            Product p47 = new Product()
            {
                Name = "Water",
                Category = drinks,
                Description = "Water Bottle",
                Price = 2.50m
            };

            Product p48 = new Product()
            {
                Name = "Coca Colla",
                Category = drinks,
                Description = "Colla Bottle",
                Price = 2.50m
            };

            Product p49 = new Product()
            {
                Name = "Milk Shakes",
                Category = drinks,
                Description = "Chocolate,Banana,Strawberry",
                Price = 2.95m
            };

            // Products of Restaurant "La Vie En Rose"
            Product p50 = new Product()
            {
                Name = "Squash Ravioli",
                Category = main,
                Description = "Ambercup squash,shallot brown butter",
                Price = 10.95m
            };

            Product p51 = new Product()
            {
                Name = "Ahi Tuna",
                Category = main,
                Description = "Sesame encrusted seared tuna steak",
                Price = 15.95m
            };

            Product p52 = new Product()
            {
                Name = "Seafood Risotto",
                Category = main,
                Description = "Arborio rice,cream and fresh seasonal seafood",
                Price = 16
            };


            Product p53 = new Product()
            {
                Name = "Lobster Macaroni & Cheese",
                Category = appetizer,
                Description = "Bay shrimp and chunks of lobster",
                Price = 26
            };

            Product p54 = new Product()
            {
                Name = "Cobo Salmon",
                Category = appetizer,
                Description = "Over-roasted wild salmon",
                Price = 25
            };

            Product p55 = new Product()
            {
                Name = "Creme Brulee",
                Category = dessert,
                Description = "Creme Brulee",
                Price = 5.80m
            };

            Product p56 = new Product()
            {
                Name = "Beef Tenderloin",
                Category = appetizer,
                Description = "Roasted with spicy chili infusion",
                Price = 18.90m
            };

            Product p57 = new Product()
            {
                Name = "Prime Rib",
                Category = appetizer,
                Description = "Rib slow roasted",
                Price = 32
            };

            Product p58 = new Product()
            {
                Name = "Veal Scalloppini",
                Category = appetizer,
                Description = "Grilled veal chop",
                Price = 7.50m
            };


            Product p60 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 45
            };

            Product p61 = new Product()
            {
                Name = "Water",
                Category = drinks,
                Description = "Water Bottle",
                Price = 2.50m
            };

            Product p62 = new Product()
            {
                Name = "Coca Colla",
                Category = drinks,
                Description = "Colla Bottle",
                Price = 2.50m
            };

            Product p63 = new Product()
            {
                Name = "White Wine",
                Category = drinks,
                Description = "White Wine Bottle",
                Price = 35.40m
            };
            #endregion


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

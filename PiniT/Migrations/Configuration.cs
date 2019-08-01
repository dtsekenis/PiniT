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


            #region Role Creation
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

            if (adminRole == null)
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
            #endregion

            #region Managers
            //Add Managers
            string restManagerUserName_1 = "Manager1";
            string restManagerEmail_1 = "manager1@gmail.com";
            string restManagerPassword_1 = "iamthemanager1";

            string restManagerUserName_2 = "Manager2";
            string restManagerEmail_2 = "manager2@gmail.com";
            string restManagerPassword_2 = "iamthemanager2";

            string restManagerUserName_3 = "Manager3";
            string restManagerEmail_3 = "manager3@gmail.com";
            string restManagerPassword_3 = "iamthemanager3";

            string restManagerUserName_4 = "Manager4";
            string restManagerEmail_4 = "manager4@gmail.com";
            string restManagerPassword_4 = "iamthemanager4";

            PiniTManager restManager1 = (PiniTManager)userManager.FindByName(restManagerEmail_1);
            if (restManager1 == null)
            {
                restManager1 = new PiniTManager
                {
                    UserName = restManagerEmail_1,
                    Email = restManagerEmail_1,
                    RestaurantAuthorized = true
                };
                userManager.Create(restManager1, restManagerPassword_1);
            }

            if (!userManager.IsInRole(restManager1.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager1.Id, restManagerRole.Name);
            }
            PiniTManager restManager2 = (PiniTManager)userManager.FindByName(restManagerEmail_2);
            if (restManager2 == null)
            {
                restManager2 = new PiniTManager
                {
                    UserName = restManagerEmail_2,
                    Email = restManagerEmail_2,
                    RestaurantAuthorized = true
                };
                userManager.Create(restManager2, restManagerPassword_2);
            }

            if (!userManager.IsInRole(restManager2.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager2.Id, restManagerRole.Name);
            }

            PiniTManager restManager3 = (PiniTManager)userManager.FindByName(restManagerEmail_3);
            if (restManager3 == null)
            {
                restManager3 = new PiniTManager
                {
                    UserName = restManagerEmail_3,
                    Email = restManagerEmail_3,
                    RestaurantAuthorized = true
                };
                userManager.Create(restManager3, restManagerPassword_3);
            }

            if (!userManager.IsInRole(restManager3.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager3.Id, restManagerRole.Name);
            }
            PiniTManager restManager4 = (PiniTManager)userManager.FindByName(restManagerEmail_4);
            if (restManager4 == null)
            {
                restManager4 = new PiniTManager
                {
                    UserName = restManagerEmail_4,
                    Email = restManagerEmail_4,
                    RestaurantAuthorized = true
                };
                userManager.Create(restManager4, restManagerPassword_4);
            }

            if (!userManager.IsInRole(restManager4.Id, restManagerRole.Name))
            {
                userManager.AddToRole(restManager4.Id, restManagerRole.Name);
            }
            context.SaveChanges();

            #endregion

            // Tess Area
            string customerUsername_1 = "Customer1";
            string customerEmail_1 = "customer1@gmail.com";
            string customerPassword_1 = "iamthecustomer1";

            PiniTCustomer customer1 = (PiniTCustomer)userManager.FindByName(customerEmail_1);
            if (customer1 == null)
            {
                customer1 = new PiniTCustomer
                {
                    Email = customerEmail_1,
                    UserName = customerEmail_1
                };
                userManager.Create(customer1, customerPassword_1);
            }
            if (!userManager.IsInRole(customer1.Id,customerRole.Name))
            {
                userManager.AddToRole(customer1.Id, customerRoleName);
            }
            context.SaveChanges();

            #region Wallets
            //Manager Wallets
            AccountWallet wallet1 = new AccountWallet { Id = restManager1.Id, Credits = 2000.50m };
            AccountWallet wallet3 = new AccountWallet { Id = restManager2.Id, Credits = 1234.99m };
            AccountWallet wallet4 = new AccountWallet { Id = restManager3.Id, Credits = 320.16m };
            AccountWallet wallet5 = new AccountWallet { Id = restManager4.Id, Credits = 250.50m };

            //Customer Wallets
            AccountWallet wallet2 = new AccountWallet { Id = customer1.Id, Credits = 52.23m };

            //Add Wallets

            context.AccountWallets.AddOrUpdate(x=>x.Id,wallet1,wallet2,wallet3,wallet4,wallet5);
            context.SaveChanges();
            #endregion

            #region Admin
            //Add Admin
            string adminUserName = "Tasos";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "iamtheadmin";

            ApplicationUser admin = userManager.FindByName(adminEmail);
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
            context.SaveChanges();

            #endregion

            #region Product Categories
            //Add Product Categories
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

            context.ProductCategories.AddOrUpdate(x => x.Name, appetizer, salads, main, drinks, dessert);
            context.SaveChanges();
            #endregion

            #region Restaurant Types
            //Add Restaurant Type
            RestaurantType bar = new RestaurantType { Name = "Bar" };
            RestaurantType bistro  = new RestaurantType { Name = "Bistro" };
            RestaurantType grill = new RestaurantType { Name = "Grill House" };
            RestaurantType vegan = new RestaurantType { Name = "Vegan" };
            RestaurantType italian = new RestaurantType { Name = "Italian" };
            RestaurantType asian = new RestaurantType { Name = "Asian" };
            RestaurantType french= new RestaurantType { Name = "French" };
            RestaurantType restaurant= new RestaurantType { Name = "Restaurant" };
            RestaurantType greek= new RestaurantType { Name = "Greek" };
            RestaurantType seafood= new RestaurantType { Name = "Seafood" };
            RestaurantType street= new RestaurantType { Name = "Street Food Bar" };
            RestaurantType pizza= new RestaurantType { Name = "Pizza" };
            RestaurantType barRestaurant = new RestaurantType { Name = "Bar Restaurant" };
            RestaurantType winebar = new RestaurantType { Name = "Wine Bar" };
            RestaurantType beer= new RestaurantType { Name = "Beer House" };

            context.RestaurantTypes.AddOrUpdate(x => x.Name, bar, bistro,grill, vegan,italian,asian,french,restaurant,greek,seafood,street,pizza,barRestaurant,winebar,beer);
            context.SaveChanges();
            #endregion

            #region Products
            //Add Products
            Product p1 = new Product()
            {
                Name = "Pizza Volcano",
                Category = main,
                Description = "Mozzarela,Tomato,Jalapenos,Chilly Sause",
                Price = 11.0m
            };

            Product p2 = new Product()
            {
                Name = "Pizza Margerita",
                Category = main,
                Description = "Mozzarela,Tomato,Basillico",
                Price = 9.0m
            };

            Product p3 = new Product()
            {
                Name = "Pizza Vegeterian",
                Category = main,
                Description = "Mozzarela,Tomato,Olives,Onions,Peppers",
                Price = 10.0m
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
                Price = 10.0m
            };

            Product p6 = new Product()
            {
                Name = "Spaghetti Carbonara",
                Category = main,
                Description = "Bacon,White Sause",
                Price = 12.0m
            };

            Product p7 = new Product()
            {
                Name = "La Bella Mafia Sallad",
                Category = salads,
                Description = "Lettuce,iceberg,sause",
                Price = 10.0m
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
                Price = 5.0m
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
                Price = 30.0m
            };

            Product p14 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 30.0m
            };

            Product p15 = new Product()
            {
                Name = "Water",
                Category = drinks,
                Description = "Water Bottle",
                Price = 3.0m
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

           // Products of Restaurant "Ôhe beautiful Beijing"
            Product p19 = new Product()
            {
                Name = "Chicken Kow",
                Category = main,
                Description = "Chicken with vegetables",
                Price = 15.0m
            };

            Product p20 = new Product()
            {
                Name = "Sweet & Sour Pork",
                Category = main,
                Description = "Pork with sweet & sour pork",
                Price = 9.0m
            };

            Product p21 = new Product()
            {
                Name = "Sesame Chicken",
                Category = main,
                Description = "Chicken with Sesame",
                Price = 10.0m
            };


            Product p25 = new Product()
            {
                Name = "Egg Roll",
                Category = appetizer,
                Description = "Egg Roll",
                Price = 8.0m
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
                Description = "Crab well made",
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
                Price = 13.0m
            };

            Product p32 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 16.0m
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

           // Products of Restaurant "God Bless America"
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
                Price = 10.0m
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
                Price = 13.0m
            };

            Product p46 = new Product()
            {
                Name = "Red Wine",
                Category = drinks,
                Description = "Red Wine Bottle",
                Price = 16.0m
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

            //Products of Restaurant "La Vie En Rose"
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
                Price = 16.0m
            };


            Product p53 = new Product()
            {
                Name = "Lobster Macaroni & Cheese",
                Category = appetizer,
                Description = "Bay shrimp and chunks of lobster",
                Price = 26.0m
            };

            Product p54 = new Product()
            {
                Name = "Cobo Salmon",
                Category = appetizer,
                Description = "Over-roasted wild salmon",
                Price = 25.0m
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
                Price = 32.0m
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
                Price = 45.0m
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

            context.Products.AddOrUpdate(x => x.ProductId,
                                         p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                                         p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                                         p21, p25, p26, p27, p28, p29, p30,
                                         p31, p32, p33, p34, p35, p36, p37, p38, p39, p40,
                                         p41, p42, p43, p44, p45, p46, p47, p48, p49, p50,
                                         p51, p52, p53, p54, p55, p56, p57, p58, p60,
                                         p61, p62, p63);
            context.SaveChanges();

            #endregion

            #region Tables
            //Add Tables
            Table t1 = new Table { Size = 4, Name = "A1" };
            Table t2 = new Table { Size = 6, Name = "A2" };
            Table t3 = new Table { Size = 8, Name = "A3" };
            Table t4 = new Table { Size = 4, Name = "A4" };
            Table t5 = new Table { Size = 6, Name = "A5" };
            Table t6 = new Table { Size = 8, Name = "A6" };
            Table t7 = new Table { Size = 4, Name = "A7" };
            Table t8 = new Table { Size = 6, Name = "A8" };
            Table t9 = new Table { Size = 8, Name = "A9" };
            Table t10 = new Table { Size = 4, Name = "A10" };
            Table t11 = new Table { Size = 6, Name = "A11" };
            Table t12 = new Table { Size = 8, Name = "A12" };
            Table t13 = new Table { Size = 4, Name = "A13" };
            Table t14 = new Table { Size = 6, Name = "A14" };
            Table t15= new Table { Size = 8, Name = "A15" };
            Table t16 = new Table { Size = 4, Name = "A16" };
            Table t17 = new Table { Size = 6, Name = "A17" };
            Table t18 = new Table { Size = 8, Name = "A18" };
            Table t19= new Table { Size = 4, Name = "A19" };
            Table t20 = new Table { Size = 6, Name = "A20" };
            Table t21 = new Table { Size = 8, Name = "A21" };
            Table t22 = new Table { Size = 4, Name = "A22" };
            Table t23 = new Table { Size = 6, Name = "A23" };
            Table t24 = new Table { Size = 8, Name = "A24" };
            Table t25 = new Table { Size = 4, Name = "A25" };
            Table t26 = new Table { Size = 6, Name = "A26" };
            Table t27 = new Table { Size = 8, Name = "A27" };
            Table t28 = new Table { Size = 4, Name = "A28" };
            Table t29 = new Table { Size = 6, Name = "A29" };
            Table t30 = new Table { Size = 8, Name = "A30" };
            Table t31 = new Table { Size = 4, Name = "A31" };
            Table t32 = new Table { Size = 6, Name = "A32" };
            Table t33 = new Table { Size = 8, Name = "A33" };
            Table t34 = new Table { Size = 4, Name = "A34" };
            Table t35 = new Table { Size = 6, Name = "A35" };
            Table t36= new Table { Size = 8, Name = "A36" };
            Table t37 = new Table { Size = 4, Name = "A37" };
            Table t38= new Table { Size = 6, Name = "A38" };
            Table t39= new Table { Size = 8, Name = "A39" };
            Table t40 = new Table { Size = 4, Name = "A40" };
            Table t41 = new Table { Size = 6, Name = "A41" };
            Table t42 = new Table { Size = 8, Name = "A42" };
            Table t43 = new Table { Size = 4, Name = "A43" };
            Table t44 = new Table { Size = 6, Name = "A44" };
            Table t45 = new Table { Size = 8, Name = "A45" };
            Table t46 = new Table { Size = 4, Name = "A46" };
            Table t47 = new Table { Size = 6, Name = "A47" };
            Table t48 = new Table { Size = 8, Name = "A48" };


            context.Tables.AddOrUpdate(x => x.TableId, t1, t2, t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19,t20,t21,t22,t23,t24,t25,t26,t27,t28,t29,t30,t31,t32,t33,t34,t35,t36,t37,t38,t39,t40,t41,t42,t43,t44,t45,t46,t47,t48);

            context.SaveChanges();
            #endregion

            #region Restaurants
            //Add Restaurants
            Restaurant restaurant1 = new Restaurant
            {
                CompanyName = "La Bella Mafia",
                VAT = "12345678",
                Manager = restManager1,
                Type = { pizza, italian },
                Menu = {p1,p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p17,p18},
                Tables = {t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12}
            };

            Restaurant restaurant2 = new Restaurant
            {
                CompanyName = "The Beautiful Beijing",
                VAT = "12345679",
                Manager = restManager2,
                Type = { restaurant, asian  },
                Menu = { p19, p20, p21, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35 },
                Tables = { t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23, t24 }
            };

            Restaurant restaurant3 = new Restaurant
            {
                CompanyName = "God Bless America",
                VAT = "12345680",
                Manager = restManager3,
                Type = { restaurant, grill},
                Menu = { p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49},
                Tables = { t25, t26, t27, t28, t29, t30, t31, t32, t33, t34, t35, t36 }
            };

            Restaurant restaurant4 = new Restaurant
            {
                CompanyName = "La vie en rose",
                VAT = "12345682",
                Manager = restManager4,
                Type = { restaurant, french,bistro, bar },
                Menu = { p50, p51, p52, p53, p54, p55, p56, p57, p58, p60, p61, p62, p63 },
                Tables = { t37, t38, t39, t40, t41, t42, t43, t44, t45, t46, t47, t48 }
            };


            context.Restaurants.AddOrUpdate(x => x.RestaurantId, restaurant1,restaurant2,restaurant3,restaurant4);
            context.SaveChanges();
            #endregion

        }
    }
}

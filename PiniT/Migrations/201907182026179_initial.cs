namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryId = c.String(maxLength: 128),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServedAt = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryId)
                .ForeignKey("dbo.Restaurants", t => t.ServedAt)
                .Index(t => t.CategoryId)
                .Index(t => t.ServedAt);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.String(nullable: false, maxLength: 128),
                        CompanyName = c.String(nullable: false),
                        VAT = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantId)
                .ForeignKey("dbo.AspNetUsers", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        RestaurantId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TableId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        TableId = c.Int(nullable: false),
                        BookDate = c.DateTime(nullable: false),
                        BookingFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.RestaurantTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RestaurantTypeRestaurants",
                c => new
                    {
                        RestaurantType_Name = c.String(nullable: false, maxLength: 128),
                        Restaurant_RestaurantId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RestaurantType_Name, t.Restaurant_RestaurantId })
                .ForeignKey("dbo.RestaurantTypes", t => t.RestaurantType_Name, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantType_Name)
                .Index(t => t.Restaurant_RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "ServedAt", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantTypeRestaurants", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.RestaurantTypeRestaurants", "RestaurantType_Name", "dbo.RestaurantTypes");
            DropForeignKey("dbo.Tables", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Reservations", "TableId", "dbo.Tables");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Restaurants", "RestaurantId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories");
            DropIndex("dbo.RestaurantTypeRestaurants", new[] { "Restaurant_RestaurantId" });
            DropIndex("dbo.RestaurantTypeRestaurants", new[] { "RestaurantType_Name" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reservations", new[] { "TableId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropIndex("dbo.Tables", new[] { "RestaurantId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Restaurants", new[] { "RestaurantId" });
            DropIndex("dbo.Products", new[] { "ServedAt" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.RestaurantTypeRestaurants");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RestaurantTypes");
            DropTable("dbo.Reservations");
            DropTable("dbo.Tables");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Restaurants");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
        }
    }
}

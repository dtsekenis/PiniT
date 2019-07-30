namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images_ProfileRestImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ImagePath = c.String(nullable: false),
                        RestaurantId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Images", new[] { "RestaurantId" });
            DropTable("dbo.Images");
        }
    }
}

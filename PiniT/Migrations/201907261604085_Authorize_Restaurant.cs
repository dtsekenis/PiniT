namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authorize_Restaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RestaurantAuthorized", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RestaurantAuthorized");
        }
    }
}

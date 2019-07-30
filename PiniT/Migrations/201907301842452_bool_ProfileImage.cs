namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bool_ProfileImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "isProfileImage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "isProfileImage");
        }
    }
}

namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablenames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "Name");
        }
    }
}

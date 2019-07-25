namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation_comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "Comment");
        }
    }
}

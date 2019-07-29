namespace PiniT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Account_Wallet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountWallets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Credits = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountWallets", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.AccountWallets", new[] { "Id" });
            DropTable("dbo.AccountWallets");
        }
    }
}

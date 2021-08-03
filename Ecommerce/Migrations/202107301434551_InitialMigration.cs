namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "CategoryId", c => c.Int());
            CreateIndex("dbo.Sales", "CategoryId");
            AddForeignKey("dbo.Sales", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Sales", new[] { "CategoryId" });
            DropColumn("dbo.Sales", "CategoryId");
        }
    }
}

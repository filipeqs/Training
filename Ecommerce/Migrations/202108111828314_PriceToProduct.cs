namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
        }
    }
}

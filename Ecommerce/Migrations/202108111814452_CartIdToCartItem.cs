namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CartIdToCartItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            RenameColumn(table: "dbo.CartItems", name: "Cart_Id", newName: "CartId");
            AlterColumn("dbo.CartItems", "CartId", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "CartId");
            AddForeignKey("dbo.CartItems", "CartId", "dbo.Carts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "CartId", "dbo.Carts");
            DropIndex("dbo.CartItems", new[] { "CartId" });
            AlterColumn("dbo.CartItems", "CartId", c => c.Int());
            RenameColumn(table: "dbo.CartItems", name: "CartId", newName: "Cart_Id");
            CreateIndex("dbo.CartItems", "Cart_Id");
            AddForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts", "Id");
        }
    }
}

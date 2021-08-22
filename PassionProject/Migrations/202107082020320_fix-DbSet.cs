namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDbSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Rental_RentalId", "dbo.Rentals");
            DropForeignKey("dbo.Tools", "Rental_RentalId", "dbo.Rentals");
            DropForeignKey("dbo.Rentals", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Rentals", "Tool_ToolId", "dbo.Tools");
            DropIndex("dbo.Customers", new[] { "Rental_RentalId" });
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropIndex("dbo.Rentals", new[] { "ToolId" });
            DropIndex("dbo.Rentals", new[] { "Tool_ToolId" });
            DropIndex("dbo.Rentals", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Tools", new[] { "Rental_RentalId" });
            DropColumn("dbo.Rentals", "CustomerId");
            DropColumn("dbo.Rentals", "ToolId");
            RenameColumn(table: "dbo.Rentals", name: "Customer_CustomerId", newName: "CustomerId");
            RenameColumn(table: "dbo.Rentals", name: "Tool_ToolId", newName: "ToolId");
            AlterColumn("dbo.Rentals", "ToolId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "CustomerId");
            CreateIndex("dbo.Rentals", "ToolId");
            AddForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "ToolId", "dbo.Tools", "ToolId", cascadeDelete: true);
            DropColumn("dbo.Customers", "Rental_RentalId");
            DropColumn("dbo.Tools", "Rental_RentalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tools", "Rental_RentalId", c => c.Int());
            AddColumn("dbo.Customers", "Rental_RentalId", c => c.Int());
            DropForeignKey("dbo.Rentals", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "ToolId" });
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            AlterColumn("dbo.Rentals", "CustomerId", c => c.Int());
            AlterColumn("dbo.Rentals", "ToolId", c => c.Int());
            RenameColumn(table: "dbo.Rentals", name: "ToolId", newName: "Tool_ToolId");
            RenameColumn(table: "dbo.Rentals", name: "CustomerId", newName: "Customer_CustomerId");
            AddColumn("dbo.Rentals", "ToolId", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tools", "Rental_RentalId");
            CreateIndex("dbo.Rentals", "Customer_CustomerId");
            CreateIndex("dbo.Rentals", "Tool_ToolId");
            CreateIndex("dbo.Rentals", "ToolId");
            CreateIndex("dbo.Rentals", "CustomerId");
            CreateIndex("dbo.Customers", "Rental_RentalId");
            AddForeignKey("dbo.Rentals", "Tool_ToolId", "dbo.Tools", "ToolId");
            AddForeignKey("dbo.Rentals", "Customer_CustomerId", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Tools", "Rental_RentalId", "dbo.Rentals", "RentalId");
            AddForeignKey("dbo.Customers", "Rental_RentalId", "dbo.Rentals", "RentalId");
        }
    }
}

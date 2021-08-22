namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ToolId = c.Int(nullable: false),
                        RentalDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Tool_ToolId = c.Int(),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.RentalId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tools", t => t.Tool_ToolId)
                .ForeignKey("dbo.Tools", t => t.ToolId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.ToolId)
                .Index(t => t.Tool_ToolId)
                .Index(t => t.Customer_CustomerId);
            
            AddColumn("dbo.Customers", "Rental_RentalId", c => c.Int());
            AddColumn("dbo.Tools", "Rental_RentalId", c => c.Int());
            CreateIndex("dbo.Customers", "Rental_RentalId");
            CreateIndex("dbo.Tools", "Rental_RentalId");
            AddForeignKey("dbo.Customers", "Rental_RentalId", "dbo.Rentals", "RentalId");
            AddForeignKey("dbo.Tools", "Rental_RentalId", "dbo.Rentals", "RentalId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Tools", "Rental_RentalId", "dbo.Rentals");
            DropForeignKey("dbo.Rentals", "ToolId", "dbo.Tools");
            DropForeignKey("dbo.Rentals", "Tool_ToolId", "dbo.Tools");
            DropForeignKey("dbo.Customers", "Rental_RentalId", "dbo.Rentals");
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Tools", new[] { "Rental_RentalId" });
            DropIndex("dbo.Rentals", new[] { "Customer_CustomerId" });
            DropIndex("dbo.Rentals", new[] { "Tool_ToolId" });
            DropIndex("dbo.Rentals", new[] { "ToolId" });
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "Rental_RentalId" });
            DropColumn("dbo.Tools", "Rental_RentalId");
            DropColumn("dbo.Customers", "Rental_RentalId");
            DropTable("dbo.Rentals");
        }
    }
}

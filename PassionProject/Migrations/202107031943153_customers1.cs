namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customers1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerPhone", c => c.Int(nullable: false));
        }
    }
}

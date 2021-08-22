namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tools : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tools",
                c => new
                    {
                        ToolId = c.Int(nullable: false, identity: true),
                        ToolName = c.String(),
                    })
                .PrimaryKey(t => t.ToolId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tools");
        }
    }
}

namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        ComplaintID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        description = c.String(),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComplaintID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Complaints", new[] { "CustomerID" });
            DropTable("dbo.Complaints");
        }
    }
}

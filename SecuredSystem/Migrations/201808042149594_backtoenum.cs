namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class backtoenum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complaints", "FlagID", "dbo.Flags");
            DropIndex("dbo.Complaints", new[] { "FlagID" });
            AddColumn("dbo.Complaints", "status", c => c.Int(nullable: false));
            AddColumn("dbo.Complaints", "Flag", c => c.Int());
            DropColumn("dbo.Complaints", "FlagID");
            DropTable("dbo.Flags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Flags",
                c => new
                    {
                        FlagID = c.Int(nullable: false, identity: true),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.FlagID);
            
            AddColumn("dbo.Complaints", "FlagID", c => c.Int(nullable: false));
            DropColumn("dbo.Complaints", "Flag");
            DropColumn("dbo.Complaints", "status");
            CreateIndex("dbo.Complaints", "FlagID");
            AddForeignKey("dbo.Complaints", "FlagID", "dbo.Flags", "FlagID", cascadeDelete: true);
        }
    }
}

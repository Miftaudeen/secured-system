namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flagclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flags",
                c => new
                    {
                        FlagID = c.Int(nullable: false, identity: true),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.FlagID);
            
            AddColumn("dbo.Complaints", "Flag_FlagID", c => c.Int());
            CreateIndex("dbo.Complaints", "Flag_FlagID");
            AddForeignKey("dbo.Complaints", "Flag_FlagID", "dbo.Flags", "FlagID");
            DropColumn("dbo.Complaints", "status");
            DropColumn("dbo.Complaints", "Flag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "Flag", c => c.Int());
            AddColumn("dbo.Complaints", "status", c => c.Int(nullable: false));
            DropForeignKey("dbo.Complaints", "Flag_FlagID", "dbo.Flags");
            DropIndex("dbo.Complaints", new[] { "Flag_FlagID" });
            DropColumn("dbo.Complaints", "Flag_FlagID");
            DropTable("dbo.Flags");
        }
    }
}

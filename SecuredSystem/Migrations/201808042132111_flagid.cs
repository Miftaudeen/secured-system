namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flagid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complaints", "Flag_FlagID", "dbo.Flags");
            DropIndex("dbo.Complaints", new[] { "Flag_FlagID" });
            RenameColumn(table: "dbo.Complaints", name: "Flag_FlagID", newName: "FlagID");
            AlterColumn("dbo.Complaints", "FlagID", c => c.Int(nullable: true));
            CreateIndex("dbo.Complaints", "FlagID");
            AddForeignKey("dbo.Complaints", "FlagID", "dbo.Flags", "FlagID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "FlagID", "dbo.Flags");
            DropIndex("dbo.Complaints", new[] { "FlagID" });
            AlterColumn("dbo.Complaints", "FlagID", c => c.Int());
            RenameColumn(table: "dbo.Complaints", name: "FlagID", newName: "Flag_FlagID");
            CreateIndex("dbo.Complaints", "Flag_FlagID");
            AddForeignKey("dbo.Complaints", "Flag_FlagID", "dbo.Flags", "FlagID");
        }
    }
}

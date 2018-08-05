namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "Flag", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Complaints", "Flag");
        }
    }
}

namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "SecurityQuestion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "SecurityQuestion");
        }
    }
}

namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "PIN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "PIN", c => c.Int(nullable: false));
        }
    }
}

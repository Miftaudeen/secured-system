namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        FirstName = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Occupation = c.String(),
                        NextOfKin = c.String(),
                        AccountNumber = c.String(),
                        PIN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}

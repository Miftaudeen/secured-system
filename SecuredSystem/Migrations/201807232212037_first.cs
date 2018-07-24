namespace SecuredSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditTables",
                c => new
                    {
                        AuditId = c.Int(nullable: false, identity: true),
                        Admin = c.String(),
                        Customer = c.String(),
                        Field = c.String(),
                        InitialValue = c.String(),
                        FinalValue = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuditId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditTables");
        }
    }
}

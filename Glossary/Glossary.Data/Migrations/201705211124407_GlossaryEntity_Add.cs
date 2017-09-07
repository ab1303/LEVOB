namespace Glossary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlossaryEntity_Add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GlossaryEntity",
                c => new
                    {
                        GlossaryEntityId = c.Int(nullable: false, identity: true),
                        Term = c.String(nullable: false, maxLength: 200),
                        Definition = c.String(nullable: false, maxLength: 2000),
                        DateCreated = c.DateTime(nullable: false),
                        DateChanged = c.DateTime(),
                        CreateLogin = c.String(nullable: false, maxLength: 100),
                        UpdateLogin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.GlossaryEntityId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GlossaryEntity");
        }
    }
}

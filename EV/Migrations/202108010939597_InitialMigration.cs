namespace EV.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        cId = c.Int(nullable: false, identity: true),
                        cName = c.String(),
                    })
                .PrimaryKey(t => t.cId);
            
            CreateTable(
                "dbo.Immigrants",
                c => new
                    {
                        iId = c.Int(nullable: false, identity: true),
                        iName = c.String(nullable: false, maxLength: 50),
                        nCountryId = c.Int(nullable: false),
                        iPassNo = c.String(nullable: false),
                        iDob = c.DateTime(nullable: false),
                        iGender = c.String(nullable: false),
                        iPurpose = c.String(nullable: false),
                        iImmiType = c.String(nullable: false),
                        dCountryId = c.Int(),
                        aCountryId = c.Int(),
                        iDate = c.DateTime(nullable: false),
                        iImage = c.String(),
                    })
                .PrimaryKey(t => t.iId)
                .ForeignKey("dbo.Countries", t => t.aCountryId)
                .ForeignKey("dbo.Countries", t => t.dCountryId)
                .ForeignKey("dbo.Countries", t => t.nCountryId, cascadeDelete: true)
                .Index(t => t.nCountryId)
                .Index(t => t.dCountryId)
                .Index(t => t.aCountryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        EmailID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.EmailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Immigrants", "nCountryId", "dbo.Countries");
            DropForeignKey("dbo.Immigrants", "dCountryId", "dbo.Countries");
            DropForeignKey("dbo.Immigrants", "aCountryId", "dbo.Countries");
            DropIndex("dbo.Immigrants", new[] { "aCountryId" });
            DropIndex("dbo.Immigrants", new[] { "dCountryId" });
            DropIndex("dbo.Immigrants", new[] { "nCountryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Immigrants");
            DropTable("dbo.Countries");
        }
    }
}

namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlockLists",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        P1Chatid = c.Long(nullable: false),
                        P2Chatid = c.Long(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.ListID)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        PersonName = c.String(),
                        PersonAge = c.String(),
                        PersonCity = c.String(),
                        PersonGender = c.String(),
                        chatid = c.Long(nullable: false),
                        FilterAge = c.String(),
                        FilterCity = c.String(),
                        FilterGender = c.String(),
                        LastUser = c.Long(nullable: false),
                        CommandName = c.String(),
                        Block_Ckeck = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.FriendsLists",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        P1Chatid = c.Long(nullable: false),
                        P2Chatid = c.Long(nullable: false),
                        name = c.String(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.ListID)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.Onlines",
                c => new
                    {
                        OnlineID = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        Username = c.String(),
                        User2 = c.Long(nullable: false),
                        chatid = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.OnlineID)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.RequestValidations",
                c => new
                    {
                        ListID = c.Int(nullable: false, identity: true),
                        P1Chatid = c.Long(nullable: false),
                        P2Chatid = c.Long(nullable: false),
                        Date = c.DateTime(),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.ListID)
                .ForeignKey("dbo.People", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestValidations", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.Onlines", "PersonId", "dbo.People");
            DropForeignKey("dbo.FriendsLists", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.BlockLists", "Person_PersonId", "dbo.People");
            DropIndex("dbo.RequestValidations", new[] { "Person_PersonId" });
            DropIndex("dbo.Onlines", new[] { "PersonId" });
            DropIndex("dbo.FriendsLists", new[] { "Person_PersonId" });
            DropIndex("dbo.BlockLists", new[] { "Person_PersonId" });
            DropTable("dbo.RequestValidations");
            DropTable("dbo.Onlines");
            DropTable("dbo.FriendsLists");
            DropTable("dbo.People");
            DropTable("dbo.BlockLists");
        }
    }
}

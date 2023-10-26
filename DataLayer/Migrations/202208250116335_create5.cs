namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlockLists", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.FriendsLists", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.RequestValidations", "Person_PersonId", "dbo.People");
            DropIndex("dbo.BlockLists", new[] { "Person_PersonId" });
            DropIndex("dbo.FriendsLists", new[] { "Person_PersonId" });
            DropIndex("dbo.RequestValidations", new[] { "Person_PersonId" });
            RenameColumn(table: "dbo.BlockLists", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.FriendsLists", name: "Person_PersonId", newName: "PersonId");
            RenameColumn(table: "dbo.RequestValidations", name: "Person_PersonId", newName: "PersonId");
            AlterColumn("dbo.BlockLists", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendsLists", "PersonId", c => c.Int(nullable: false));
            AlterColumn("dbo.RequestValidations", "PersonId", c => c.Int(nullable: false));
            CreateIndex("dbo.BlockLists", "PersonId");
            CreateIndex("dbo.FriendsLists", "PersonId");
            CreateIndex("dbo.RequestValidations", "PersonId");
            AddForeignKey("dbo.BlockLists", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
            AddForeignKey("dbo.FriendsLists", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
            AddForeignKey("dbo.RequestValidations", "PersonId", "dbo.People", "PersonId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestValidations", "PersonId", "dbo.People");
            DropForeignKey("dbo.FriendsLists", "PersonId", "dbo.People");
            DropForeignKey("dbo.BlockLists", "PersonId", "dbo.People");
            DropIndex("dbo.RequestValidations", new[] { "PersonId" });
            DropIndex("dbo.FriendsLists", new[] { "PersonId" });
            DropIndex("dbo.BlockLists", new[] { "PersonId" });
            AlterColumn("dbo.RequestValidations", "PersonId", c => c.Int());
            AlterColumn("dbo.FriendsLists", "PersonId", c => c.Int());
            AlterColumn("dbo.BlockLists", "PersonId", c => c.Int());
            RenameColumn(table: "dbo.RequestValidations", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.FriendsLists", name: "PersonId", newName: "Person_PersonId");
            RenameColumn(table: "dbo.BlockLists", name: "PersonId", newName: "Person_PersonId");
            CreateIndex("dbo.RequestValidations", "Person_PersonId");
            CreateIndex("dbo.FriendsLists", "Person_PersonId");
            CreateIndex("dbo.BlockLists", "Person_PersonId");
            AddForeignKey("dbo.RequestValidations", "Person_PersonId", "dbo.People", "PersonId");
            AddForeignKey("dbo.FriendsLists", "Person_PersonId", "dbo.People", "PersonId");
            AddForeignKey("dbo.BlockLists", "Person_PersonId", "dbo.People", "PersonId");
        }
    }
}

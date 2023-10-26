namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "Block_Ckeck", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Block_Ckeck", c => c.Byte(nullable: false));
        }
    }
}

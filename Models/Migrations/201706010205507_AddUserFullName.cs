namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FullName", c => c.String(maxLength: 100));
            Sql("update dbo.Users set FullName=UserName");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FullName");
        }
    }
}

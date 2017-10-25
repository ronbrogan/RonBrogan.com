namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriesMetadataAndPublicFlag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Blog_Id = c.Guid(nullable: false),
                        CategoryName = c.String(maxLength: 255),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .Index(t => t.Blog_Id)
                .Index(t => t.CategoryName)
                .Index(t => t.CreatedBy_Id);
            
            AddColumn("dbo.Blogs", "Metadata", c => c.String(maxLength: 160));
            AddColumn("dbo.Blogs", "Public", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.Blogs", "CreatedBy_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserClaims", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Blogs", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Categories", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Categories", "Blog_Id", "dbo.Blogs");
            DropIndex("dbo.Categories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Categories", new[] { "CategoryName" });
            DropIndex("dbo.Categories", new[] { "Blog_Id" });
            DropColumn("dbo.Blogs", "Public");
            DropColumn("dbo.Blogs", "Metadata");
            DropTable("dbo.Categories");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaims", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Blogs", "CreatedBy_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}

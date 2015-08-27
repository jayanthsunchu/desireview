namespace desireview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentBody = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        CommentDate = c.DateTime(nullable: false),
                        ReviewId = c.Int(nullable: false),
                        Review_Id = c.Int(),
                        Review_ReviewTitle = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CommentBody, t.UserId })
                .ForeignKey("dbo.Reviews", t => new { t.Review_Id, t.Review_ReviewTitle })
                .Index(t => new { t.Review_Id, t.Review_ReviewTitle });
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Title = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false, identity: true),
                        Cast = c.String(),
                        Director = c.String(),
                        Producer = c.String(),
                        AverageRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReleaseDate = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        ImageName = c.String(),
                        ImageExtension = c.String(),
                    })
                .PrimaryKey(t => t.Title);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewTitle = c.String(nullable: false, maxLength: 128),
                        ReviewContent = c.String(),
                        UserId = c.Int(nullable: false),
                        ReviewedDate = c.DateTime(nullable: false),
                        ReviewRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MovieId = c.Int(nullable: false),
                        Movie_Title = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.ReviewTitle })
                .ForeignKey("dbo.Movies", t => t.Movie_Title)
                .Index(t => t.Movie_Title);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false, identity: true),
                        Password = c.String(),
                        UserGuid = c.Guid(nullable: false),
                        Email = c.String(nullable: true)
                    })
                .PrimaryKey(t => t.UserName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Movie_Title", "dbo.Movies");
            DropForeignKey("dbo.Comments", new[] { "Review_Id", "Review_ReviewTitle" }, "dbo.Reviews");
            DropIndex("dbo.Reviews", new[] { "Movie_Title" });
            DropIndex("dbo.Comments", new[] { "Review_Id", "Review_ReviewTitle" });
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Movies");
            DropTable("dbo.Comments");
        }
    }
}

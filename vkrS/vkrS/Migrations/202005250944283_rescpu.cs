namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rescpu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Guid(nullable: false),
                        Description = c.String(),
                        Link = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Guid(nullable: false),
                        ImageId = c.Guid(nullable: false),
                        TimeSeriesId = c.Guid(nullable: false),
                        Res = c.String(),
                        Time = c.Time(nullable: false, precision: 7),
                        Memory = c.String(nullable: false),
                        CPU = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.TimeSeries", t => t.TimeSeriesId, cascadeDelete: true)
                .Index(t => t.ImageId)
                .Index(t => t.TimeSeriesId);
            
            CreateTable(
                "dbo.TimeSeries",
                c => new
                    {
                        TimeSeriesId = c.Guid(nullable: false),
                        Description = c.String(),
                        Title = c.String(nullable: false),
                        AmountOfElements = c.Int(nullable: false),
                        Elements = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TimeSeriesId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "TimeSeriesId", "dbo.TimeSeries");
            DropForeignKey("dbo.Results", "ImageId", "dbo.Images");
            DropIndex("dbo.Results", new[] { "TimeSeriesId" });
            DropIndex("dbo.Results", new[] { "ImageId" });
            DropTable("dbo.Users");
            DropTable("dbo.TimeSeries");
            DropTable("dbo.Results");
            DropTable("dbo.Images");
        }
    }
}

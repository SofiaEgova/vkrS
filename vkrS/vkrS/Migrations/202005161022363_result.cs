namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class result : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultId = c.Guid(nullable: false),
                        ImageId = c.Guid(nullable: false),
                        TimeSeriesId = c.Guid(nullable: false),
                        Accuracy = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ResultId)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .ForeignKey("dbo.TimeSeries", t => t.TimeSeriesId, cascadeDelete: true)
                .Index(t => t.ImageId)
                .Index(t => t.TimeSeriesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "TimeSeriesId", "dbo.TimeSeries");
            DropForeignKey("dbo.Results", "ImageId", "dbo.Images");
            DropIndex("dbo.Results", new[] { "TimeSeriesId" });
            DropIndex("dbo.Results", new[] { "ImageId" });
            DropTable("dbo.Results");
        }
    }
}

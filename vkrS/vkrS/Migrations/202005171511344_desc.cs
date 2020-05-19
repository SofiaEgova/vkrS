namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class desc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Description", c => c.String(nullable: false));
            AddColumn("dbo.TimeSeries", "Description", c => c.String(nullable: false));
            AddColumn("dbo.TimeSeries", "Title", c => c.String(nullable: false));
            AddColumn("dbo.TimeSeries", "AmountOfElements", c => c.Int(nullable: false));
            DropColumn("dbo.TimeSeries", "IsDouble");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeSeries", "IsDouble", c => c.Boolean(nullable: false));
            DropColumn("dbo.TimeSeries", "AmountOfElements");
            DropColumn("dbo.TimeSeries", "Title");
            DropColumn("dbo.TimeSeries", "Description");
            DropColumn("dbo.Images", "Description");
        }
    }
}

namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeSeries", "Elements", c => c.Binary(nullable: false));
            DropColumn("dbo.TimeSeries", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeSeries", "Path", c => c.String(nullable: false));
            DropColumn("dbo.TimeSeries", "Elements");
        }
    }
}

namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TimeSeries", "Elements", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSeries", "Elements", c => c.Binary(nullable: false));
            DropColumn("dbo.Users", "IsActive");
        }
    }
}

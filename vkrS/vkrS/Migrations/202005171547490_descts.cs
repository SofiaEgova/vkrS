namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class descts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeSeries", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeSeries", "Description", c => c.String(nullable: false));
        }
    }
}

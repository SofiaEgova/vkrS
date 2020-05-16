namespace vkrS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class result2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Time", c => c.DateTime(nullable: false));
        }
    }
}

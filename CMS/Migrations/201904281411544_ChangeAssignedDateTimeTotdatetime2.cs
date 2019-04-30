namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAssignedDateTimeTotdatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShippingAssignments", "AssignedDateTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShippingAssignments", "AssignedDateTime", c => c.DateTime(nullable: false));
        }
    }
}

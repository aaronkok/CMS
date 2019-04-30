namespace CMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        MaxTonnage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ShipmentStatusLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShippingOrderId = c.Long(nullable: false),
                        FromStatus = c.Int(nullable: false),
                        ToStatus = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        UpdatorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShippingOrders", t => t.ShippingOrderId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatorId)
                .Index(t => t.ShippingOrderId)
                .Index(t => t.UpdatorId);
            
            CreateTable(
                "dbo.ShippingOrders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrackingId = c.String(),
                        ActualTonnage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        BillingAddressId = c.Long(nullable: false),
                        DestinationAddressId = c.Long(nullable: false),
                        ShipmentOwnerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShippingBillingAddresses", t => t.BillingAddressId, cascadeDelete: true)
                .ForeignKey("dbo.ShippingDestinationAddresses", t => t.DestinationAddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ShipmentOwnerId)
                .Index(t => t.BillingAddressId)
                .Index(t => t.DestinationAddressId)
                .Index(t => t.ShipmentOwnerId);
            
            CreateTable(
                "dbo.ShippingBillingAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingDestinationAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Category = c.String(),
                        Name = c.String(),
                        Amount = c.Int(nullable: false),
                        EstimateWeightage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingOrderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShippingOrders", t => t.ShippingOrderId, cascadeDelete: true)
                .Index(t => t.ShippingOrderId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ShippingAssignments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ShippingOrderId = c.Long(nullable: false),
                        ContainerId = c.Long(nullable: false),
                        AssignorId = c.String(maxLength: 128),
                        AssignedDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignorId)
                .ForeignKey("dbo.Containers", t => t.ContainerId, cascadeDelete: true)
                .ForeignKey("dbo.ShippingOrders", t => t.ShippingOrderId, cascadeDelete: true)
                .Index(t => t.ShippingOrderId)
                .Index(t => t.ContainerId)
                .Index(t => t.AssignorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingAssignments", "ShippingOrderId", "dbo.ShippingOrders");
            DropForeignKey("dbo.ShippingAssignments", "ContainerId", "dbo.Containers");
            DropForeignKey("dbo.ShippingAssignments", "AssignorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShipmentStatusLogs", "UpdatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShipmentStatusLogs", "ShippingOrderId", "dbo.ShippingOrders");
            DropForeignKey("dbo.ShippingOrders", "ShipmentOwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShippingItems", "ShippingOrderId", "dbo.ShippingOrders");
            DropForeignKey("dbo.ShippingOrders", "DestinationAddressId", "dbo.ShippingDestinationAddresses");
            DropForeignKey("dbo.ShippingOrders", "BillingAddressId", "dbo.ShippingBillingAddresses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.ShippingAssignments", new[] { "AssignorId" });
            DropIndex("dbo.ShippingAssignments", new[] { "ContainerId" });
            DropIndex("dbo.ShippingAssignments", new[] { "ShippingOrderId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ShippingItems", new[] { "ShippingOrderId" });
            DropIndex("dbo.ShippingOrders", new[] { "ShipmentOwnerId" });
            DropIndex("dbo.ShippingOrders", new[] { "DestinationAddressId" });
            DropIndex("dbo.ShippingOrders", new[] { "BillingAddressId" });
            DropIndex("dbo.ShipmentStatusLogs", new[] { "UpdatorId" });
            DropIndex("dbo.ShipmentStatusLogs", new[] { "ShippingOrderId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.ShippingAssignments");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ShippingItems");
            DropTable("dbo.ShippingDestinationAddresses");
            DropTable("dbo.ShippingBillingAddresses");
            DropTable("dbo.ShippingOrders");
            DropTable("dbo.ShipmentStatusLogs");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Containers");
        }
    }
}

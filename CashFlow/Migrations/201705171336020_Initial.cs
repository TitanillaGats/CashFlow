namespace CashFlow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseLogID = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PurchaseLogs", t => t.PurchaseLogID, cascadeDelete: true)
                .Index(t => t.PurchaseLogID);
            
            CreateTable(
                "dbo.PurchaseLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        PurchaseLogID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseLogs", t => t.PurchaseLogID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.PurchaseLogID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Savings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PurchaseLogID = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                        GoalBalance = c.Double(nullable: false),
                        Description = c.String(),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PurchaseLogs", t => t.PurchaseLogID, cascadeDelete: true)
                .Index(t => t.PurchaseLogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Savings", "PurchaseLogID", "dbo.PurchaseLogs");
            DropForeignKey("dbo.PurchaseItems", "PurchaseLogID", "dbo.PurchaseLogs");
            DropForeignKey("dbo.PurchaseItems", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Budgets", "PurchaseLogID", "dbo.PurchaseLogs");
            DropIndex("dbo.Savings", new[] { "PurchaseLogID" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseLogID" });
            DropIndex("dbo.PurchaseItems", new[] { "CategoryID" });
            DropIndex("dbo.Budgets", new[] { "PurchaseLogID" });
            DropTable("dbo.Savings");
            DropTable("dbo.Categories");
            DropTable("dbo.PurchaseItems");
            DropTable("dbo.PurchaseLogs");
            DropTable("dbo.Budgets");
        }
    }
}

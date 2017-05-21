namespace CashFlow.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CashFlow.Models.CashFlowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CashFlow.Models.CashFlowContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            context.PurchaseLogs.AddOrUpdate(
                new Models.PurchaseLog { ID = 1, Name = "Personal" }
                );

            context.Categories.AddOrUpdate(
                new Models.Category { ID = 1, Name = "Shopping", Description = ""},
                new Models.Category { ID = 2, Name = "Health", Description = "" },
                new Models.Category { ID = 3, Name = "Beauty", Description = "" }
                );

            context.Budgets.AddOrUpdate(
                new Models.Budget { ID = 1, PurchaseLogID = 1, Balance = 125000 }
                );

            context.Savings.AddOrUpdate(
                new Models.Saving { ID = 1, PurchaseLogID = 1, Balance = 65000, Deadline = new DateTime(2017, 12, 31), GoalBalance = 250000 }
                );

            context.PurchaseItems.AddOrUpdate(
                new Models.PurchaseItem { ID = 1, PurchaseLogID = 1, Amount = 2000, CategoryID = 1, Date = new DateTime(2017, 5, 11), Comment = "For lasagne tonight" }
                );
        }
    }
}

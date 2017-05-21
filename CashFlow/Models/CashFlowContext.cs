using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class CashFlowContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CashFlowContext() : base("name=CashFlowContext")
        {
        }

        public System.Data.Entity.DbSet<CashFlow.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<CashFlow.Models.PurchaseLog> PurchaseLogs { get; set; }

        public System.Data.Entity.DbSet<CashFlow.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<CashFlow.Models.PurchaseItem> PurchaseItems { get; set; }

        public System.Data.Entity.DbSet<CashFlow.Models.Saving> Savings { get; set; }
    }
}

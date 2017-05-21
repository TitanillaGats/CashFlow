using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class PurchaseLog
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
        public virtual ICollection<Saving> Savings { get; set; }


        public void newPurchaseItem() { }

        public void newBudget() { }

        public void newSaving() { }
    }
}
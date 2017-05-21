using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class Saving
    {
        public int ID { get; set; }
        public int PurchaseLogID { get; set; }
        public double Balance { get; set; }
        public double GoalBalance { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public virtual PurchaseLog PurchaseLog { get; set; }

        public void cashFlow(double amount)
        {
            Balance += amount;
        }
    }
}
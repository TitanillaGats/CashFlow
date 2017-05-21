using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class Budget
    {
        public int ID { get; set; }
        public int PurchaseLogID { get; set; }
        public double Balance { get; set; }

        public virtual PurchaseLog PurchaseLog { get; set; }

        public void cashFlow(double amount)
        {
            Balance += amount;
        }
    }
}
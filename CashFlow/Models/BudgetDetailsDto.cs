using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class BudgetDetailsDto
    {
        public int ID { get; set; }
        public double Balance { get; set; }
        public int PurchaseLogID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class PurchaseItemDetailsDto
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }
        public int CategoryID { get; set; }
        public int PurchaseLogID { get; set; }
    }
}
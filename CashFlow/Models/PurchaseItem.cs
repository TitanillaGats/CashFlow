using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace CashFlow.Models
{
    public class PurchaseItem
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int PurchaseLogID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Comment { get; set; }

        public virtual Category Category{ get; set; }
        public virtual PurchaseLog PurchaseLog { get; set; }


        public void modifyItem(PurchaseItem newItem)
        {
            this.Date = newItem.Date;
            this.Amount = newItem.Amount;
            this.Comment = newItem.Comment;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class PurchaseLogDetailsDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<BudgetDetailsDto> Budgets { get; set; }
        public List<PurchaseItemDetailsDto> PurchaseItems { get; set; }
        public List<SavingDetailsDto> Savings { get; set; }
    }
}
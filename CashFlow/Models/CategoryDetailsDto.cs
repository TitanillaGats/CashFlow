using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashFlow.Models
{
    public class CategoryDetailsDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<PurchaseItemDetailsDto> PurchaseItems { get; set; }
    }
}
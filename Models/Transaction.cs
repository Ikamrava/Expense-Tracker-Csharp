using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Traker_Csharp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; } = "empty";
        public double Amount { get; set; } = 0.0;
        public DateTime Date { get; set; } = DateTime.Now;

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
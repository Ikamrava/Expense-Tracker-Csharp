using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Traker_Csharp.Models
{
    public class Transaction
    {
        public int id { get; set; }
        public string description { get; set; } = "empty";
        public double amount { get; set; } = 0.0;
        public DateTime date { get; set; } = DateTime.Now;

        public int categoryid { get; set; }
        public Category category { get; set; }

    }
}
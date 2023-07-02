using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Expense_Traker_Csharp.Models

{
    public class Transaction
    {
        [Key]
        public int transactionid { get; set; }
        public string note { get; set; } = "Spent";
        public double amount { get; set; } = 0.0;
        public DateTime date { get; set; } = DateTime.Now;

        public int categoryid { get; set; } = 17;
        public Category category { get; set; }

    }
}
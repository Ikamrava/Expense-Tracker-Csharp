// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace Expense_Traker_Csharp.Models

// {
//     public class Transaction
//     {
//         [Key]
//         public int transactionid { get; set; }
//         public string note { get; set; } = "Spent";
//         // public string amount { get; set; } = "2";
//         // public string date { get; set; }

//         public int categoryid { get; set; }
//         public Category category { get; set; }

//         [NotMapped]
//         public string? cattitleWithIcon
//         {
//             get
//             {
//                 return category == null ? "" : category.icon + " " + category.title;
//             }
//         }

//         // [NotMapped]

//         // public string? formatedAmount
//         // {
//         //     get
//         //     {
//         //         return category == null || category.type == "Expense" ? "-" + amount.ToString() : "+" + amount.ToString();
//         //     }

//         // }


//     }
// }



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Traker_Csharp.Models
{
    public class Transaction
    {
        [Key]
        public int transactionid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int categoryid { get; set; }
        public Category? category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0.")]
        public int amount { get; set; }


        public string? note { get; set; }

        public DateOnly date { get; set; }

        [NotMapped]
        public string? cattitleWithIcon
        {
            get
            {
                return category == null ? "" : category.icon + " " + category.title;
            }
        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                return ((category == null || category.type == "Expense") ? "- " : "+ ") + amount.ToString("C0");
            }
        }

    }
}

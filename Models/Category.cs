using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Traker_Csharp.Models
{
    public class Category
    {

        public int categoryid { get; set; }
        public string title { get; set; } = "General";
        public string icon { get; set; } = "";
        public string type { get; set; } = "Expense";

        [NotMapped]
        public string? titleWithIcon
        {
            get
            {
                return this.icon + " " + this.title;
            }
        }
    }





}
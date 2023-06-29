using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Expense_Traker_Csharp.Models
{
    public class Category
    {

        public int id { get; set; }
        public string title { get; set; } = "General";
        public string icon { get; set; } = "https://icons8.com/icon/Uc54oOGCsjOE/house";
        public string type { get; set; } = "Out";

    }
}
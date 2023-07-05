using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Traker_Csharp.Models
{
    public class VMLogin
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";

        public bool RememberMe { get; set; } = true;
    }
}
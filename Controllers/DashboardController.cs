using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Expense_Traker_Csharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Traker_Csharp.Controllers
{
    public class DashboardController : Controller
    {



        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<ActionResult> IndexAsync()
        {
            DateTime startDate = DateTime.Today.AddDays(-6);
            DateTime endDate = DateTime.Today;
            List<Models.Transaction> SelectedTransactios = await _context.transaction.Include(x => x.category).Where(y => y.date >= startDate && y.date <= endDate).ToListAsync();

            int TotalIncome = SelectedTransactios.Where(x => x.category.type == "Income").Sum(x => x.amount);
            ViewBag.TotalIncome = TotalIncome.ToString();

            int TotalExpense = SelectedTransactios.Where(x => x.category.type == "Expense").Sum(x => x.amount);
            ViewBag.TotalExpense = TotalExpense.ToString();

            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString();



            return View();
        }
    }
}
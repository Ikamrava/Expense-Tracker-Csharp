using System;
using System.Collections.Generic;
using System.Globalization;
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
            CultureInfo culture = new CultureInfo("en-GB");
            culture.NumberFormat.CurrencySymbol = "£";
            culture.NumberFormat.CurrencyNegativePattern = 1;

            int TotalIncome = SelectedTransactios.Where(x => x.category.type == "Income").Sum(x => x.amount);
            ViewBag.TotalIncome = String.Format(culture, "{0:C}", TotalIncome);

            int TotalExpense = SelectedTransactios.Where(x => x.category.type == "Expense").Sum(x => x.amount);
            ViewBag.TotalExpense = String.Format(culture, "{0:C}", TotalExpense);

            int Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = String.Format(culture, "{0:C}", Balance);


            ViewBag.DoughnutChartData = SelectedTransactios
                .Where(i => i.category.type == "Expense")
                .GroupBy(j => j.category.categoryid)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().category.icon + " " + k.First().category.title,
                    amount = k.Sum(j => j.amount),
                    formattedAmount = k.Sum(j => j.amount).ToString("C0"),
                })
                .OrderByDescending(l => l.amount)
                .ToList();


            List<SplineChartData> IncomeSummary = SelectedTransactios
        .Where(i => i.category.type == "Income")
        .GroupBy(j => j.date)
        .Select(k => new SplineChartData()
        {
            day = k.First().date.ToString("dd-MMM"),
            income = k.Sum(l => l.amount)
        })
        .ToList();

            //Expense

            List<SplineChartData> ExpenseSummary = SelectedTransactios
                .Where(i => i.category.type == "Expense")
                .GroupBy(j => j.date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().date.ToString("dd-MMM"),
                    expense = k.Sum(l => l.amount)
                })
                .ToList();

            //Combine Income & Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };
            //Recent Transactions
            ViewBag.RecentTransactions = await _context.transaction
                .Include(i => i.category)
                .OrderByDescending(j => j.date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;

    }
}
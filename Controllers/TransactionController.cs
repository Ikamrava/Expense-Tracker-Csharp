using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Traker_Csharp.Models;

namespace Expense_Traker_Csharp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.transaction.Include(t => t.category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transaction/Details/5


        // GET: Transaction/Create
        public IActionResult AddorEdit()
        {
            populateCategory();
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddorEdit([Bind("transactionid,note,amount,date,categoryid")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryid"] = new SelectList(_context.category, "categoryid", "categoryid", transaction.categoryid);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["categoryid"] = new SelectList(_context.category, "categoryid", "categoryid", transaction.categoryid);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("transactionid,note,amount,date,categoryid")] Transaction transaction)
        {
            if (id != transaction.transactionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.transactionid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["categoryid"] = new SelectList(_context.category, "categoryid", "categoryid", transaction.categoryid);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.transaction
                .Include(t => t.category)
                .FirstOrDefaultAsync(m => m.transactionid == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.transaction == null)
            {
                return Problem("Entity set 'ApplicationDbContext.transaction'  is null.");
            }
            var transaction = await _context.transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.transaction.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return (_context.transaction?.Any(e => e.transactionid == id)).GetValueOrDefault();
        }


        [NonAction]
        public void populateCategory()
        {
            var categoryCollection = _context.category.ToList();
            Category Defaultcategory = new Category() { categoryid = 0, title = "Choose Category" };
            categoryCollection.Insert(0, Defaultcategory);


            ViewBag.category = categoryCollection;


        }

    }
}

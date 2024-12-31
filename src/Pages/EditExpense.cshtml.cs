using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTrackerSystem.Pages
{
    [BindProperties]

    public class EditExpenseModel : PageModel
    {
        private readonly ILogger<EditExpenseModel> _logger;
        public Expense? exp;

        public int ID { get; init; }

        public required string Description { get; set; }
        public required decimal Amount { get; set; } 
        public string Category { get; set; } = default!;
        public DateTime DateAdded { get; set; } = DateTime.Now;

        private InMemoryExpenseService expenseService = new InMemoryExpenseService();

        public EditExpenseModel(ILogger<EditExpenseModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGetAsync(int id=0, CancellationToken cToken = default)
        {
            var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            List<Expense> list = listTemp.ToList();
            if (list != null && list.Count > 0)
            {
                exp = list.FirstOrDefault(x => x.ID==id);

            }
        }
        public RedirectToPageResult OnGetDelete(int id = 0, bool del = false, CancellationToken cToken = default)
        {
            if (del)
            {
                _ = expenseService.DeleteExpenseAsync(id, cToken);
            }
            return RedirectToPage("./Index");
        }
        public async Task<RedirectToPageResult> OnPostAsync(CancellationToken cToken)
        {

            var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            List<Expense> list = listTemp.ToList();

            Expense edit = new Expense { Amount = Amount, Category = Category,  Description = Description, ID = ID };

            if (list != null && list.Count > 0)
            {
                Expense e = list.Where(y => y.ID == ID).First();
                if (e != null)
                {
                    edit.DateAdded = e.DateAdded;
                }
            }


            _ = expenseService.UpdateExpenseAsync(edit, cToken);

            return RedirectToPage("./Index");

        }
    }

}

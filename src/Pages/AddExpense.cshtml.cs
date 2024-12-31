using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Services;
using System;
using System.Collections.Generic;

namespace PersonalExpenseTrackerSystem.Pages
{
    [BindProperties]

    public class AddExpenseModel : PageModel
    {
        private readonly ILogger<AddExpenseModel> _logger;

        public int ID { get; init; }
        public required string Description { get; set; }
        public required decimal Amount { get; set; } = 0;
        public string Category { get; set; } = default!;
        public DateTime DateAdded { get; set; } = DateTime.Now;

        private InMemoryExpenseService expenseService = new InMemoryExpenseService();

        public AddExpenseModel(ILogger<AddExpenseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
        public async Task<RedirectToPageResult> OnPostAsync(CancellationToken cToken)
        {

            var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            List<Expense> list = listTemp.ToList();

            var maxID = 0;

            if (list!=null && list.Count>0)
                maxID = list.Select(x=>x.ID).Max();

            Expense one = new Expense { Amount = Amount, Category = Category, DateAdded = DateTime.Now, ID = maxID+1, Description = Description };

            _ = expenseService.AddExpenseAsync(one, cToken);

            return RedirectToPage("./Index");

        }
    }

}

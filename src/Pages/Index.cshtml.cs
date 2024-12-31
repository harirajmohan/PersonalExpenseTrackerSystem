using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Services;
using PersonalExpenseTrackerSystem.Services.Contract;

namespace PersonalExpenseTrackerSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private InMemoryExpenseService expenseService = new InMemoryExpenseService();
        public List<Expense>? list;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet(CancellationToken cToken)
        {

             var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            list = listTemp.ToList();
              
        }
    }
}

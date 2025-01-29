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
        public IExpenseService expenseService { get; }
        public List<Expense>? list;
        
        public IndexModel(ILogger<IndexModel> logger, IExpenseService inMemoryExpenseService)
        {
            _logger = logger;
            expenseService = inMemoryExpenseService;
        }

        public async Task OnGet(CancellationToken cToken)
        {

             var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            list = listTemp.ToList();
            _logger.LogInformation("Reached Get method");
        }
    }
}

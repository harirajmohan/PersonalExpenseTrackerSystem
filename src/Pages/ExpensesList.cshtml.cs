using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalExpenseTrackerSystem.Domain.Entities;

namespace Web.Pages;

public class ExpensesList : PageModel
{
    [BindProperty]
    public List<Expense> Expenses { get; set; }
    
    public void OnGet()
    {
        //call the service to get the expenses
        //in order to do that you need to create a bind property and fill it with the expenses
        // and use the same property in cshtml file
    }
    
    public void OnPost()
    {
        //call the service to add the expense
    }
}
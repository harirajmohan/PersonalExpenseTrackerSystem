using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Pages.Shared;
using PersonalExpenseTrackerSystem.Services;
using PersonalExpenseTrackerSystem.Services.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace PersonalExpenseTrackerSystem.Pages
{
    

    public class EditExpenseModel : PageModel
    {
        private readonly ILogger<EditExpenseModel> _logger;
        //public Expense? exp;
        
        public ExpenseModel? ExpenseModel;

        private readonly IMapper _mapper;

        public IExpenseService expenseService { get; }

        public EditExpenseModel(ILogger<EditExpenseModel> logger, IExpenseService inMemoryExpenseService)
        {
            _logger = logger;
            expenseService = inMemoryExpenseService;
        }
        public async Task OnGetAsync(int id=0, CancellationToken cToken = default)
        {
            var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            List<Expense> list1 = listTemp.ToList();
            if (list1 != null && list1.Count > 0)
            {
                //ExpenseModel = list1.Where(x => x.ID == id).Select(a=> new ExpenseModel   { Amount = a.Amount, Description = a.Description, Category = a.Category, DateAdded = a.DateAdded, ID = a.ID }).FirstOrDefault();
                ExpenseModel = _mapper.Map<ExpenseModel>(list1.Where(x => x.ID == id).Select(a => new ExpenseModel { Amount = a.Amount, Description = a.Description, Category = a.Category, DateAdded = a.DateAdded, ID = a.ID }).FirstOrDefault());

                 

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

            Expense edit = new Expense { Amount = ExpenseModel.Amount, Category = ExpenseModel.Category,  Description = ExpenseModel.Description, ID = ExpenseModel.ID };

            if (list != null && list.Count > 0)
            {
                Expense e = list.Where(y => y.ID == ExpenseModel.ID).First();
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

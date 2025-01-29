using AutoMapper;
using FluentValidation;
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

namespace PersonalExpenseTrackerSystem.Pages
{
    [BindProperties]

    public class AddExpenseModel: PageModel
    {
        public required ExpenseModel ExpenseModel;
        public required string Description { get; set; }

        private readonly ILogger<AddExpenseModel> _logger ;
        private readonly IMapper _mapper;
        //private readonly ExpenseValidator _validator;
        private IValidator<ExpenseModel> _validator;

        public IExpenseService expenseService1 = new InMemoryExpenseService();
        public AddExpenseModel(ILogger<AddExpenseModel> logger, IValidator<ExpenseModel> validator
            //, IExpenseService expenseService1
            )
        {
            _logger = logger;
            _validator = validator;
            //this.expenseService1 = expenseService1;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(CancellationToken cToken)
        {
            //Expense expense = new Expense { Amount = ExpenseModel.Amount, Category = ExpenseModel.Category, DateAdded = DateTime.Now, ID = maxID+1, Description = ExpenseModel.Description };
            ExpenseModel expenseModel = _mapper.Map<ExpenseModel>(ExpenseModel);
            _validator.Validate(expenseModel);
            var result = _validator.Validate(ExpenseModel);


            if (!result.IsValid)
            {
                // Copy the validation results into ModelState.
                // ASP.NET uses the ModelState collection to populate 
                // error messages in the View.


                // re-render the view when validation failed.
                return (IActionResult)ExpenseModel;
            }

            var expenseService = new InMemoryExpenseService();
            var listTemp = await expenseService.GetAllExpensesAsync(cToken);
            List<Expense> list = listTemp.ToList();

            var maxID = 0;

            if (list != null && list.Count > 0)
                maxID = list.Select(x => x.ID).Max();



            _ = expenseService.AddExpenseAsync(_mapper.Map<Expense>(ExpenseModel), cToken);

            return RedirectToPage("./Index");

        }
    }

}

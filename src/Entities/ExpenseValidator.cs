using FluentValidation;
using PersonalExpenseTrackerSystem.Pages.Shared;

namespace PersonalExpenseTrackerSystem.Entities
{
    public class ExpenseValidator : AbstractValidator<ExpenseModel>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Description).NotNull().MinimumLength(1).MaximumLength(200);
            RuleFor(x => x.Category).NotNull().MinimumLength(1).MaximumLength(50);

        }
    }
}

using PersonalExpenseTrackerSystem.Application.Contracts;
using PersonalExpenseTrackerSystem.Domain.Entities;

namespace PersonalExpenseTrackerSystem.Application.Services;

public class InMemoryExpenseService : IExpenseService
{
    private readonly List<Expense> _expenses = [];

    public Task<Expense> AddExpenseAsync(Expense expense)
    {
        _expenses.Add(expense);
        return Task.FromResult(expense);
    }

    public Task<bool> DeleteExpenseAsync(int id)
    {
        var expenseToDelete = _expenses.SingleOrDefault(x => x.Id == id);
        if (expenseToDelete is null)
            throw new Exception("expense not found!");
        
        _expenses.Remove(expenseToDelete);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Expense>> GetAllExpensesAsync() =>
        Task.FromResult<IEnumerable<Expense>>(_expenses.ToList());

    public Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category)
    {
        var result = _expenses.Where(x => x.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult<IEnumerable<Expense>>(result.ToList());
    }

    public Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var result = _expenses.Where(x => x.DateAdded >= startDate && x.DateAdded <= endDate);
        return Task.FromResult<IEnumerable<Expense>>(result.ToList());
    }

    public Task<decimal> GetTotalExpensesAsync() => Task.FromResult(_expenses.Sum(x => x.Amount));

    public Task<bool> UpdateExpenseAsync(Expense expense)
    {
        var expenseToUpdate = _expenses.SingleOrDefault(x => x.Id == expense.Id);
        if (expenseToUpdate is null)
            throw new Exception("Expense not found");

        expenseToUpdate.Amount = expense.Amount;
        expenseToUpdate.Category = expense.Category;
        expenseToUpdate.DateModified = DateTime.Now;
        expenseToUpdate.Description = expense.Description;


        return Task.FromResult(true);
    }
}
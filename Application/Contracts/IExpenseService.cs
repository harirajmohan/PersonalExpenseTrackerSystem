using PersonalExpenseTrackerSystem.Domain.Entities;

namespace PersonalExpenseTrackerSystem.Application.Contracts;

public interface IExpenseService
{
    Task<Expense> AddExpenseAsync(Expense expense);
    Task<IEnumerable<Expense>> GetAllExpensesAsync();
    Task<bool> UpdateExpenseAsync(Expense expense);
    Task<bool> DeleteExpenseAsync(Guid id);
    Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category);
    Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalExpensesAsync();
}

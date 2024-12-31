using PersonalExpenseTrackerSystem.Entities;

namespace PersonalExpenseTrackerSystem.Services.Contract;

public interface IExpenseService
{
    Task<Expense> AddExpenseAsync(Expense expense, CancellationToken cToken = default);
    Task<List<Expense>> GetAllExpensesAsync(CancellationToken cToken=default);
    Task<bool> UpdateExpenseAsync(Expense expense, CancellationToken cToken = default);
    Task<bool> DeleteExpenseAsync(int id, CancellationToken cToken = default);
    Task<List<Expense>> GetExpensesByCategoryAsync(string category, CancellationToken cToken = default);
    Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cToken = default);
    Task<decimal> GetTotalExpensesAsync(CancellationToken cToken = default);
}

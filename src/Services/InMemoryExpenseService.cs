using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Services.Contract;

namespace PersonalExpenseTrackerSystem.Services;

public class InMemoryExpenseService : IExpenseService
{
    private readonly List<Expense> _expenses = [];

    public Task<Expense> AddExpenseAsync(Expense expense)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteExpenseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetAllExpensesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetTotalExpensesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateExpenseAsync(Expense expense)
    {
        throw new NotImplementedException();
    }
}

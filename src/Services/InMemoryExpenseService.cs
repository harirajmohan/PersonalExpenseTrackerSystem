using AutoMapper;
using PersonalExpenseTrackerSystem.Entities;
using PersonalExpenseTrackerSystem.Services.Contract;

namespace PersonalExpenseTrackerSystem.Services;

public class InMemoryExpenseService : IExpenseService
{
    private static readonly List<Expense> _expenses = [];
    private readonly IMapper _mapper;

    public Task<Expense> AddExpenseAsync(Expense expense, CancellationToken cToken = default)
    {      
        if (!cToken.IsCancellationRequested)
        {
            if (_expenses.Where(x => x.ID == expense.ID).Count() <= 0)
                _expenses.Add(expense);
        }
        else
        {
            Console.WriteLine("Task cancelled.");
            cToken.ThrowIfCancellationRequested();
        }

        return Task.FromResult(expense);
    }

    public Task<bool> DeleteExpenseAsync(int id, CancellationToken cToken)
    {
        bool deleted = false;

        if (!cToken.IsCancellationRequested)
        {
            if (_expenses.Where(x => x.ID == id).Count() > 0)
            {
                _expenses.Remove(_expenses.First(x => x.ID == id));
                deleted = true;
            }
        }
        else
        {
            Console.WriteLine("Task cancelled.");
            cToken.ThrowIfCancellationRequested();
        }


        return Task.FromResult(deleted);
    }

    public Task<List<Expense>> GetAllExpensesAsync(CancellationToken cToken)
    {
        return Task.FromResult(_expenses);
    }

    public Task<List<Expense>> GetExpensesByCategoryAsync(string category, CancellationToken cToken)
    {
        return Task.FromResult(_expenses.Where(x => x.Category.ToUpper() == category.ToUpper()).ToList());
    }

    public Task<List<Expense>> GetExpensesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cToken = default)
    {
        return Task.FromResult(_expenses.Where(x => x.DateAdded >= startDate && x.DateAdded <= endDate).ToList());
    }

    public Task<decimal> GetTotalExpensesAsync(CancellationToken cToken)
    {
        return Task.FromResult(_expenses.Sum(x => x.Amount));
    }

    public Task<bool> UpdateExpenseAsync(Expense expense, CancellationToken cToken)
    {
        bool updated = false;
        if (_expenses.Where(x => x.ID == expense.ID).Count() > 0)
        {
            Expense temp = (Expense)_expenses.Where(x => x.ID == expense.ID).First();

            var indexOf = _expenses.IndexOf(_expenses.First(x => x.ID == expense.ID));

            if (!cToken.IsCancellationRequested)
            {
                if (indexOf >= 0)
                    _expenses[indexOf] = expense;

                updated = true;
            }
            else
            {
                Console.WriteLine("Task cancelled.");
                cToken.ThrowIfCancellationRequested();
            }

        };
        return Task.FromResult(updated);
    }

    internal async Task GetAllExpensesAsync(CancellationToken? cToken)
    {
        throw new NotImplementedException();
    }
}
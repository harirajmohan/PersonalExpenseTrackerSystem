namespace PersonalExpenseTrackerSystem.Entities;

public class Expense
{
    public int ID { get; init; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; } = 0;
    public string Category { get; set; } = default!;
    public DateTime DateAdded { get; set; } = DateTime.Now;

}

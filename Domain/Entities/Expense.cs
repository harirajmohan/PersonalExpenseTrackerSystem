﻿namespace PersonalExpenseTrackerSystem.Domain.Entities;

public class Expense
{
    public int Id { get; init; }
    public string? Description { get; set; }
    public required decimal Amount { get; set; } = 0;
    public string Category { get; set; } = default!;
    public DateTime DateAdded { get; set; } = DateTime.Now;
    
    public DateTime? DateModified { get; set; }

}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTrackerSystem.Pages.Shared
{
    [BindProperties]
    public class ExpenseModel
    {
        public int ID { get; init; }
        [Required]
        [StringLength(1)]
        [MaxLength(20)]
        public required string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public required decimal Amount { get; set; } = 1;
        [Required]
        [StringLength(1)]
        [MaxLength(10)]
        public string Category { get; set; } = default!;
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}

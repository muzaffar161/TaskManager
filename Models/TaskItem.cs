using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    public bool IsDone { get; set; }

    public Category Category { get; set; }
}

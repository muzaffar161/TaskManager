using TaskManager.Models;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync(Category? category);
    Task AddTaskAsync(TaskItem task);
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task UpdateTaskAsync(TaskItem task);
    Task DeleteTaskAsync(int id);
}

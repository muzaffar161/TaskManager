using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly TaskManagerContext _context;

    public TaskService(TaskManagerContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasksAsync(Category? category)
    {
        var query = _context.TaskItems.AsQueryable();

        if (category.HasValue)
        {
            query = query.Where(t => t.Category == category.Value);
        }

        return await query.ToListAsync();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task UpdateTaskAsync(TaskItem task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var task = await GetTaskByIdAsync(id);
        if (task != null)
        {
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}

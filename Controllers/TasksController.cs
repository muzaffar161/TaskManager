using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: Tasks
    public async Task<IActionResult> Index(Category? category)
    {
        var tasks = await _taskService.GetTasksAsync(category);
        ViewData["Filter"] = category;
        return View(tasks);
    }

    // GET: Tasks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tasks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Category")] TaskItem task)
    {
        if (ModelState.IsValid)
        {
            task.IsDone = false;
            await _taskService.AddTaskAsync(task);
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }

    // POST: Tasks/Toggle/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Toggle(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task != null)
        {
            task.IsDone = !task.IsDone;
            await _taskService.UpdateTaskAsync(task);
        }
        return RedirectToAction(nameof(Index));
    }
    
    // POST: Tasks/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteTaskAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

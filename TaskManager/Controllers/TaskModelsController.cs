using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Data.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TaskModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //get User signed in
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: TaskModels
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.tasks.ToListAsync();
            var tasksModel = MapToTasksModelList(tasks);
            return View(tasksModel.ToAsyncEnumerable());
        }

        private IList<TaskModel> MapToTasksModelList(IList<EmpTask> tasks)
        {
            IList<TaskModel> taskModels = new List<TaskModel>();
            foreach (EmpTask task in tasks)
            {
                TaskModel taskModel = MapToTaskModel(task);
                taskModels.Add(taskModel);
            }
            return taskModels;
        }

        private TaskModel MapToTaskModel(EmpTask task)
        {
            TaskModel taskModel = new TaskModel
            {
                Id = task.Id,
                Name = task.Name,
                IdentityUserId = task.IdentityUserId,
                StatusId = task.StatusId
            };

            return taskModel;
        }

        // GET: TaskModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                if (task.StatusId == StatusEnum.Assigned && task.IdentityUserId.Equals(user.Id))
                {
                    task.StatusId = StatusEnum.InProgress;
                    try
                    {
                        _context.Update(task);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TaskModelExists(task.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            return View(MapToTaskModel(task));
        }

        // GET: TaskModels/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,IdentityUserId,StatusId")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.StatusId = StatusEnum.Assigned;
                var user = await _context.Users.Where(u => u.Email == taskModel.Email).FirstOrDefaultAsync();
                taskModel.IdentityUserId = user.Id;
                _context.Add(MapToEntityTask(taskModel));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskModel);
        }

        private EmpTask MapToEntityTask(TaskModel taskModel)
        {
            EmpTask task = new EmpTask
            {
                Name = taskModel.Name,
                IdentityUserId = taskModel.IdentityUserId,
                StatusId = taskModel.StatusId
            };
            if (taskModel.Id != null)
                task.Id = taskModel.Id;
            return task;

        }

        // GET: TaskModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                ViewData["LoggedInUser"] = user.Id;
                ViewData["TaskStatus"] = task.StatusId;
            }
            return View(MapToTaskModel(task));
        }

        // POST: TaskModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            [Bind("Id,CheckFinish,CheckComplete,Name,Email,IdentityUserId,StatusId")] TaskModel taskModel)
        {
            if (id != taskModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (taskModel.CheckFinish)
                        taskModel.StatusId = StatusEnum.Pending;
                    if (taskModel.CheckComplete)
                        taskModel.StatusId = StatusEnum.Complete;

                    _context.Update(MapToEntityTask(taskModel));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskModelExists(taskModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskModel);
        }

        // GET: TaskModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(MapToTaskModel(task));
        }

        // POST: TaskModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var task = await _context.tasks.FindAsync(id);
            _context.tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskModelExists(string id)
        {
            return _context.tasks.Any(e => e.Id == id);
        }
    }
}

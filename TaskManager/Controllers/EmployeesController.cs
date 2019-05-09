using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Data;
using TaskManager.Helpers;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var userList = await _context.Users.ToListAsync();
            var employeeModelList = MapToEmloyeeModelsList(userList);
            return View(employeeModelList.ToAsyncEnumerable());
        }

        private IList<EmployeeModel> MapToEmloyeeModelsList(List<IdentityUser> users)
        {
            IList<EmployeeModel> employeeModels = new List<EmployeeModel>();
            foreach(IdentityUser user in users)
            {
                EmployeeModel employeeModel = MapToEmployeeModel(user);
                employeeModels.Add(employeeModel);
            }
            return employeeModels;
        }

        private EmployeeModel MapToEmployeeModel(IdentityUser user)
        {
            EmployeeModel model = new EmployeeModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return model;
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}


using System.ComponentModel.DataAnnotations;
using TaskManager.Data.Entities;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        // Assigned employee
        public string IdentityUserId { get; set; }

        // Task's status
        public StatusEnum StatusId { get; set; }
    }
}

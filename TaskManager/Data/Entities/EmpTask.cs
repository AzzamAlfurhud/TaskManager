
namespace TaskManager.Data.Entities
{
    public class EmpTask : BaseEntity
    {
        public string Name { get; set; }

        // Assigned employee
        public string IdentityUserId { get; set; }
        public Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

        // Task's status
        public StatusEnum StatusId { get; set; }
        public Status Status { get; set; }
    }
}

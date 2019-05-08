
namespace TaskManager.Data.Entities
{
    public class EmpTask : BaseEntity
    {
        public string Name { get; set; }

        // Assigned employee
        public string IdentityUserId { get; set; }
        public Microsoft.AspNetCore.Identity.IdentityUser user { get; set; }
    }
}

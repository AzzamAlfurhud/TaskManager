
namespace TaskManager.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        // Replies
        public string CommentId { get; set; }
        public Comment Reply { get; set; }

        // Commentator
        public string IdentityUserId { get; set; }
        public Microsoft.AspNetCore.Identity.IdentityUser User { get; set; }

        // Task
        public string EmpTaskId { get; set; }
        public EmpTask EmpTask { get; set; }

    }
}

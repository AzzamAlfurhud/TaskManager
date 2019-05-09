using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        public string Text { get; set; }

        // Replies
        public string CommentId { get; set; }

        // Commentator
        public string IdentityUserId { get; set; }

        // Task
        public string EmpTaskId { get; set; }
    }
}

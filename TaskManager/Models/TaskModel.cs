﻿
using System.ComponentModel;
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
        public string Email { get; set; }
        public string IdentityUserId { get; set; }

        // Task's status
        [DisplayName("Status")]
        public StatusEnum StatusId { get; set; }
        public bool CheckFinish { get; set; }
        public bool CheckComplete { get; set; }

    }
}

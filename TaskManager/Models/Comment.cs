﻿
namespace TaskManager.Models
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public string CommentId { get; set; }
        public Comment Reply { get; set; }
    }
}
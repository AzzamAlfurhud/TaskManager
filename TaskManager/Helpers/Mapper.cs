using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using TaskManager.Data.Entities;
using TaskManager.Models;

namespace TaskManager.Helpers
{
    public class Mapper
    {
        public static readonly string EMP_TASK = "EmpTask"; 
        public static readonly string COMMENT = "comment"; 
        public static readonly string USER = "AspUsers"; 

        public static Object MapToEntity(Object model, string entityName)
        {
            if (entityName.Equals(EMP_TASK))
                return MapToTaskEntity((TaskModel) model);
            else if (entityName.Equals(COMMENT))
                return MapToCommentEntity((CommentModel) model);
            else if (entityName.Equals(USER))
                return MapToUser((EmployeeModel)model);
            else
                return null;
        }

        private static EmpTask MapToTaskEntity(TaskModel model)
        {
            EmpTask task = new EmpTask
            {
                Name = model.Name,
                IdentityUserId = model.IdentityUserId,
                StatusId = model.StatusId
            };

            return task;

        }

        private static Comment MapToCommentEntity(CommentModel model)
        {
            Comment comment = new Comment()
            {
                Text = model.Text,
                CommentId = model.CommentId,
                IdentityUserId = model.IdentityUserId,
                EmpTaskId = model.EmpTaskId
            };

            return comment;
        }
        private static IdentityUser MapToUser(EmployeeModel model)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PasswordHash = model.Password;
            return user;
            
        }
    }
}

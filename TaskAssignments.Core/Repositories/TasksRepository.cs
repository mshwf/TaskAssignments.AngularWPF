using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskAssignments.Core.ViewModels;
using System;
using Omu.ValueInjecter;
using TaskAssignments.Core.Entities;

namespace TaskAssignments.Core.Repositories
{
    public class TasksRepository
    {
        public static List<TaskViewModel> SelectAll(ApplicationDbContext context)
        {
            var tasks = context.Tasks.Include(x => x.Users).ToList().Select(x => new TaskViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                DueDate = x.DueDate,
                _Users = x.Users.Select(z => new UserViewModel { Email = z.Email, Id = z.Id, UserName = z.UserName }).ToList()
            }).ToList();
            return tasks;

        }

        public static bool Update(ApplicationDbContext context, TaskViewModel taskVM)
        {
            try
            {
                var task = context.Tasks.Find(taskVM.Id);
                context.Entry(task).State = EntityState.Modified;
                task.InjectFrom(taskVM);
                task.Users.Clear();
                task.Users = new List<ApplicationUser>();
                foreach (var user in taskVM._Users)
                {
                    var userDB = context.Users.Find(user.Id);
                    context.Entry(userDB).State = EntityState.Unchanged;
                    task.Users.Add(userDB);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static dynamic SelectMyTasks(ApplicationDbContext context, string userId)
        {
            var myTasks = new List<TaskViewModel>();
            myTasks = context.Users.Include(t => t.Tasks).SingleOrDefault(u => u.Id == userId)?.Tasks.Select(x => new TaskViewModel
            { Id = x.Id, Title = x.Title, Description = x.Description, DueDate = x.DueDate, Status = x.Status }).ToList() ?? myTasks;
            return myTasks;
        }

        public static bool Delete(ApplicationDbContext context, int id)
        {
            try
            {
                var taskToDelete = context.Tasks.Find(id);
                context.Tasks.Remove(taskToDelete);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;

                throw;
            }


        }
    }
}

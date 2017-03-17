using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignments.Core.Entities;

namespace TaskAssignments.Core.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<UserViewModel> _Users { get; set; } = new List<UserViewModel>();
        public TaskViewModel()
        {

        }
        public TaskViewModel(Entities.Task task)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            Status = task.Status;
            DueDate = task.DueDate;
            foreach (var user in task.Users)
            {
                var userViewModel = new UserViewModel();
                userViewModel.InjectFrom(user);
                _Users.Add(userViewModel);
            }
        }
    }
}

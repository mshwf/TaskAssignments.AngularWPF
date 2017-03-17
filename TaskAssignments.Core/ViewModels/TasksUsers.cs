using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAssignments.Core.Entities;

namespace TaskAssignments.Core.ViewModels
{
    public class TasksUsers
    {
        public IEnumerable<Entities.Task> Tasks { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}

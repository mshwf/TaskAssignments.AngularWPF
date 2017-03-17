using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersAndRolesMVC.ViewModels
{
    public class TasksUsers
    {
        public IEnumerable<UserTask> Tasks { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
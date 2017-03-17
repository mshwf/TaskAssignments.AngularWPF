using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using TaskAssignments.Core.Entities;
using TaskAssignments.Core.ViewModels;

namespace TaskAssignments.Core.Repositories
{
    public class UsersRepository
    {
        public static dynamic GetAll(ApplicationDbContext context)
        {
            //getting all users not in admin role, so that the app administrator cannot assign tasks to himself
            var users = new List<UserViewModel>();
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            foreach (var user in context.Users)
            {
                if (!usermanager.IsInRole(user.Id, "Admin"))
                {
                    users.Add(new UserViewModel { Id = user.Id, UserName = user.UserName, Email = user.Email });
                }
            }
            return users;
        }
    }
}

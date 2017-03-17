using System.Linq;
using TaskAssignments.Core.Entities;
using TaskAssignments.Core.ViewModels;

namespace TaskAssignments.Core.Repositories
{
    public class UsersRepository
    {
        public static dynamic GetAll(ApplicationDbContext context)
        {
            var users = (from u in context.Users select new UserViewModel { Id = u.Id, Email=u.Email, UserName=u.UserName }).ToList();
            return users;
        }
    }
}

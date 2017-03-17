using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskAssignments.Core.Entities;
using UsersAndRolesMVC.Models;

namespace UsersAndRolesMVC.Helpers
{
    public static class HtmlHelpers
    {
        public static List<ApplicationUser> UserInRole(this HtmlHelper Html, string roleId)
        {
            var context = new ApplicationDbContext();
            var users = context.Users.Where(x => (x.Roles.Select(y => y.RoleId)).Contains(roleId)).ToList();
            return users;
        }

    }
}
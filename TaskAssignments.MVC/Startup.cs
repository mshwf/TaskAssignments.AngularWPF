using System;
using Microsoft.Owin;
using Owin;
using UsersAndRolesMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TaskAssignments.Core.Entities;

[assembly: OwinStartupAttribute(typeof(UsersAndRolesMVC.Startup))]
namespace UsersAndRolesMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUsersAndRoles();
        }

        private void CreateUsersAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string pwd = "123@Asd";

            if (!roleManager.RoleExists("Admin"))
            {
                var adminRole = new IdentityRole { Name = "Admin" };
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole { Name = "Manager" };
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole { Name = "Employee" };
                roleManager.Create(role);
            }

            if (userManager.FindByName("Mohamed") == null)
            {
                var user = new ApplicationUser { Email = "admin@app.net", UserName = "Mohamed" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Admin");
            }


            if (userManager.FindByName("Abdallah") == null)
            {
                var user = new ApplicationUser { Email = "abd@app.net", UserName = "Abdallah" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Manager");
            }
            if (userManager.FindByName("Asmaa") == null)
            {
                var user = new ApplicationUser { Email = "asm@app.net", UserName = "Asmaa" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Manager");
            }

            if (userManager.FindByName("Ahmed") == null)
            {
                var user = new ApplicationUser { Email = "ahm@app.net", UserName = "Ahmed" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Employee");
            }
            if (userManager.FindByName("Kamal") == null)
            {
                var user = new ApplicationUser { Email = "kam@app.net", UserName = "Kamal" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Employee");
            }
            if (userManager.FindByName("Alyaa") == null)
            {
                var user = new ApplicationUser { Email = "aly@app.net", UserName = "Alyaa" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Employee");
            }

            if (userManager.FindByName("Reda") == null)
            {
                var user = new ApplicationUser { Email = "red@app.net", UserName = "Reda" };
                var result = userManager.Create(user, pwd);
                userManager.AddToRole(user.Id, "Employee");
            }

        }
    }
}

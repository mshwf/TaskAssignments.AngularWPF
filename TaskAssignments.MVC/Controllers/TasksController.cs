using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using TaskAssignments.Core.Entities;
using TaskAssignments.Core.ViewModels;

namespace UsersAndRolesMVC.Controllers
{
    public class TasksController : Controller
    {
        ApplicationDbContext context;
        public TasksController()
        {
            context = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? taskId)
        {
            var viewModel = new TasksUsers();
            viewModel.Tasks = context.Tasks.Include(x => x.Users);
            if (taskId != null)
            {
                viewModel.Users = viewModel.Tasks.Single(t => t.Id == taskId).Users;
                ViewBag.Row = taskId;
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult MyTasks()
        {
            string id = User.Identity.GetUserId();
            var userTasks = context.Users.Include(t => t.Tasks).Single(u => u.Id == id).Tasks;
            return View(userTasks);
        }
        public ActionResult Delete(int? taskId)
        {
            var taskToDelete = context.Tasks.Single(t => t.Id == taskId);
            context.Tasks.Remove(taskToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? taskId)
        {
            if (taskId != null)
            {
                var taskToUpdate = context.Tasks.Include("Users").SingleOrDefault(t => t.Id == taskId);
                ViewBag.AllUsers = context.Users.ToList();
                if (taskToUpdate != null)
                {
                    return View(taskToUpdate);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] asndUsers)//assigned users
        {
            var task = context.Tasks.Include(t => t.Users).SingleOrDefault(s => s.Id == id);
            task.Users = new List<ApplicationUser>();
            if (asndUsers != null)
            {
                foreach (var userId in asndUsers)
                {
                    var user = context.Users.Single(u => u.Id == userId);
                    task.Users.Add(user);
                }
            }
            if (TryUpdateModel(task, "", new string[] { "Title", "Description", "DueDate", "Status" }))
            {
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllUsers = context.Users.ToList();
            return View(task);
        }
    }
}
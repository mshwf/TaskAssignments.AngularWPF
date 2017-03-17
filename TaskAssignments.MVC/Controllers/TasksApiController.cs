using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskAssignments.Core.Repositories;
using TaskAssignments.Core.ViewModels;

namespace UsersAndRolesMVC.Controllers
{
    [RoutePrefix("api/Tasks")]
    public class TasksApiController : BaseApiController
    {
        [HttpGet, Route("")]
        public List<TaskViewModel> GetAll()
        {
            return TasksRepository.SelectAll(Context);
        }
        [HttpPost, Route("")]
        public bool Post(TaskViewModel task)
        {
            return TasksRepository.Update(Context, task);
        }
        [HttpDelete, Route("{id}")]
        public bool Delete(int id)
        {
            return TasksRepository.Delete(Context, id);
        }
        [HttpGet, Route("MyTasks")]
        public dynamic GetMyTasks()
        {
            string userId = User.Identity.GetUserId();
            return TasksRepository.SelectMyTasks(Context, userId);
        }
    }
}

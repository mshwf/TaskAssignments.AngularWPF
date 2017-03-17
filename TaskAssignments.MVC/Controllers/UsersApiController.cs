using System.Web.Http;
using TaskAssignments.Core.Repositories;

namespace UsersAndRolesMVC.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersApiController : BaseApiController
    {
        [HttpGet, Route(""), Authorize(Roles = "Admin")]
        public dynamic GetAll()
        {
            return UsersRepository.GetAll(Context);
        }
    }
}

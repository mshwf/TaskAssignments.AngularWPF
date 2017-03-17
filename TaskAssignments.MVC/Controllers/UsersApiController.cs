using System.Web.Http;
using TaskAssignments.Core.Repositories;

namespace UsersAndRolesMVC.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersApiController : BaseApiController
    {
        [HttpGet, Route("")]
        public dynamic GetAll()
        {
            return UsersRepository.GetAll(Context);
        }
    }
}

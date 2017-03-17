using System.Web.Http;
using TaskAssignments.Core.Entities;

namespace UsersAndRolesMVC.Controllers
{
    public class BaseApiController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public BaseApiController()
        {

        }
        public ApplicationDbContext Context
        {
            get
            {
                if (_context == null)
                    _context = new ApplicationDbContext();
                return _context;
            }
        }

    }
}
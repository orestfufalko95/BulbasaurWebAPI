using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulbasaurWebAPI.web.Controllers
{

    /*
     * This controller will be used later to test the authorization requirement, as well as visualize the claims identity through the eyes of the API.
     */

    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Get()
        {
            var r = new JsonResult(from c in User.Claims select new {c.Type, c.Value});
            Console.WriteLine(r);
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}

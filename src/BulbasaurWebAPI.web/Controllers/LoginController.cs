using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.Service;
using BulbasaurWebAPI.bl.Interface;
using System.Net;
using Microsoft.AspNetCore.Cors;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulbasaurWebAPI.web.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly ILoginService _loginService ;

        public LoginController()
        {
            _loginService = new LoginService();
        }


        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        /// POST api/login  
        [HttpPost]
        public UserProfileDTO Login([FromBody]LoginInputDTO loginInputDTO)
        {
            var result = _loginService.Login(loginInputDTO);

            if(result?.tokenResponse != null)
            {
                this.HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                return result;
            }
            else
            {
                this.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return new UserProfileDTO();
            }
        }

        [HttpPost]
        [Route("signup")]
        public WriteUserResponseDTO SignUp([FromBody]UserDataDTO loginInputDTO)
        {
            var result = _loginService.SignUp(loginInputDTO);
            Console.WriteLine(result.IsSuccessful);

            this.HttpContext.Response.StatusCode = (int)(result.IsSuccessful ? HttpStatusCode.OK : HttpStatusCode.ExpectationFailed);

            return result;
        }

    }
}

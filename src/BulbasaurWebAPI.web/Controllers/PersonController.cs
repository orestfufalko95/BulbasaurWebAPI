using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.Service;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.exception.exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BulbasaurWebAPI.web.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController()
        {
            _personService = new PersonService();
        }


//        // GET: api/values
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public UserProfileDTO Get([FromHeader]string Authorization, int id)
        {
            /*
             * Я заходжу на сторінку юзера.
мені приходять вся інфа про нього. + список френдів з іменами і фотками. (більше нетреба)
бо коли я заходжу на френда, все повторяється, тобто "мені приходять вся інфа про нього. + список френдів з іменами і фотками" 
             */
             var currentUserId = TokenParcer.GetUserIdByToken(Authorization);
            var result = _personService.GetLoggedUserProfileById(id, currentUserId);

            if (result?.userDTO != null)
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

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IEnumerable<UserInfoForSearchDTO> Get()
        {
            IEnumerable<UserInfoForSearchDTO> result;
            try
            {
                result = _personService.getAllUsers();
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                Console.WriteLine(e);
                result = null;
            }
            return result;
        }



        // GET api/values/5
        [HttpGet]
        [Authorize]
        public UserProfileDTO Get([FromHeader] string Authorization)
        {
            /*
             * Я заходжу на сторінку юзера.
мені приходять вся інфа про нього. + список френдів з іменами і фотками. (більше нетреба)
бо коли я заходжу на френда, все повторяється, тобто "мені приходять вся інфа про нього. + список френдів з іменами і фотками" 
             */
            //Authorization: Bearer  eyJhbGciOiJSUzI1NiIsImtpZCI6IjE1MT...
            int startIndex = Authorization.IndexOf(' ');
            var token = Authorization.Substring(startIndex, Authorization.Length - startIndex);
            var result = _personService.GetLoggedUserProfileByToken(token);

            if (result?.userDTO != null)
            {
                this.HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                return result;
            }
            else
            {
                this.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return new UserProfileDTO();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("info")]
        public async Task SetInfo([FromHeader] string Authorization, UserInfoDTO userInfo)
        {
            try
            {
                await _personService.AddUserInfo(TokenParcer.GetUserIdByToken(Authorization), userInfo);

            }
            catch (InvalidImageException)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.ExpectationFailed;
            }
            catch (FileLoadException)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NoContent;
            }

        }

        [HttpPost]
        [Authorize]
        [Route("edit")]
        public async Task<WriteUserResponseDTO> EditProfile([FromHeader] string Authorization, EditProfileDTO data)
        {
            try
            {

                var response = await _personService.EditProfile(TokenParcer.GetUserIdByToken(Authorization), data);
                if (!response.IsSuccessful)
                {
                    HttpContext.Response.StatusCode = (int) HttpStatusCode.Conflict;
                }
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("\n");
                Console.WriteLine(e.StackTrace);
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NoContent;
                return null;
            }

        }

        [HttpGet]
        [Authorize]
        [Route("friends/{id}")]
        public IEnumerable<UserInfoForSearchDTO> GetFriendsById(long id)
        {
            return GetFriends(id);
        }

        private IEnumerable<UserInfoForSearchDTO> GetFriends(long id)
        {
            IEnumerable<UserInfoForSearchDTO> friends = null;
            try
            {
                friends = _personService.getFriendsById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            return friends;
        }

        [HttpGet]
        [Authorize]
        [Route("friends")]
        public IEnumerable<UserInfoForSearchDTO> GetFriendsByToken([FromHeader] string Authorization)
        {
            try
            {
                var id = TokenParcer.GetUserIdByToken(Authorization);
                return GetFriends(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }
    }
}

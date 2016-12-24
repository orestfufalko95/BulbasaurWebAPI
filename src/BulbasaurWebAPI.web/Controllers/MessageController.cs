using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.exceptions;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.Service;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulbasaurWebAPI.web.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController()
        {
            _messageService = new MessageService();
        }

        [HttpPost]
        [Authorize]
        public async Task Message([FromHeader] string Authorization, MessageInputDTO messageInput)
        {
            //Authorization: Bearer  eyJhbGciOiJSUzI1NiIsImtpZCI6IjE1MT...
            try
            {
                var senderId = TokenParcer.GetUserIdByToken(Authorization);
                await _messageService.AddMessage(senderId, messageInput);
            }
            catch (MessageAndMediaEmptyException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            }

        }

        [HttpPost]
        [Authorize]
        [Route("first_message")]
        public async Task FirstMessage([FromHeader] string Authorization, MessageInputDTO messageInput)
        {
            try
            {
                var senderId = TokenParcer.GetUserIdByToken(Authorization);
                await _messageService.AddFirstMessage(senderId, messageInput);
            }
            catch (MessageAndMediaEmptyException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception exception)
            {
                Console.WriteLine("======================================");
                Console.WriteLine(exception.Message);
                Console.WriteLine("======================================");
                Console.WriteLine(exception.StackTrace);
                HttpContext.Response.StatusCode = (int) HttpStatusCode.ExpectationFailed;
                
            }

        }

        [HttpGet]
        [Authorize]
        [Route("last")]
        public List<LastMessageDTO> LastMessage([FromHeader] string Authorization)
        {
            try
            {
                //Authorization: Bearer  eyJhbGciOiJSUzI1NiIsImtpZCI6IjE1MT...
                var senderId = TokenParcer.GetUserIdByToken(Authorization);
                return _messageService.GetLastMessages(senderId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"{e.StackTrace}");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                return null;
            }
           
        }

        [HttpGet("all/{id}")]
        [Authorize]
         public AllMessageResponseDTO GetAllMessages(int id, [FromHeader]string Authorization)
        {
            var senderId = TokenParcer.GetUserIdByToken(Authorization);

            return _messageService.GetMessages(senderId, id);
        }
    }
}

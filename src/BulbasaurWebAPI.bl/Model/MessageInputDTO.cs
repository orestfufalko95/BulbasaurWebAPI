using System;
using Microsoft.AspNetCore.Http;

namespace BulbasaurWebAPI.bl.Model
{
    public class MessageInputDTO
    {
        public int ReceiverId { set; get; }
        public string Message { set; get; }
        public DateTime Time { set; get; }
        public IFormFile Media { get; set; }
    }
} 
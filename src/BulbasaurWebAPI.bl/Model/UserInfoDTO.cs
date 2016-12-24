using System;
using Microsoft.AspNetCore.Http;

namespace BulbasaurWebAPI.bl.Model
{
   
    public class UserInfoDTO
    {
        public IFormFile Photo { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public byte Sex { get; set; }
    }
}
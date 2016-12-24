using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BulbasaurWebAPI.bl.Interface
{
    interface IMediaService
    {
        Task<string> SavePersonImage(IFormFile photo, int userId, string currentUrl);
        Task<string> SaveMedia(IFormFile mediaFile, int messageId);
    }
}

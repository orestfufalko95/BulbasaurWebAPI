using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Model
{
    public class AllMessageResponseDTO
    {
        public string FriendName { get; set; }
        public string CurrentUserName { get; set; }
        public string FriendSurame { get; set; }
        public string FriendImageUrl { get; set; }
        public string CurrentUserImageUrl { get; set; }

        public List<Message> Messages { get; set; }
    }
}

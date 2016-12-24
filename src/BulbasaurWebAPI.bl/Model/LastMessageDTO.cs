using System;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Model
{
    public class LastMessageDTO
    {
        public int FriendId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime Time { get; set; }
        public string PhotoUrl { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public MessageSenderShortInfo SenderShortInfo{ get; set; }  

        public class MessageSenderShortInfo
        {
            public MessageSenderShortInfo(){}

            public MessageSenderShortInfo(User user)
            {
                SenderName = user.Name;
                SenderPhotoUrl = user.Info.PhotoUrl;
                SenderSurame = user.SurName;
            }

            public string SenderName { get; set; }
            public string SenderSurame { get; set; }
            public string SenderPhotoUrl { get; set; }

        }
    }
}

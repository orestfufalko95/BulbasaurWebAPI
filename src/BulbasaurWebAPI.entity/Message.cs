using System;
using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.Entity
{
    public class Message 
    {
        public int MessageId { set; get; }
        [Required]
        public int SenderId { set; get; }
        [Required]
        public int ReceiverId { set; get; }
        public string Text { set; get; }
        public DateTime DateTime { set; get; }
        public bool IsRead { set; get; }
		public string AttachmentUrl { set; get; }

        public string MediaName { get; set; }
        //get
        //    {
        //        if (AttachmentUrl == null) return null;
        //        var indexSlash = AttachmentUrl.LastIndexOf('/');
        //        return AttachmentUrl.Substring(indexSlash);
        //    }
}
}
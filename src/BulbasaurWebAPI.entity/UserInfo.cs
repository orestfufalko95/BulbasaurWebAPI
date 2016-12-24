using System;
using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.Entity
{
    public class UserInfo
    {
        public int UserId { get; set; }
        [MaxLength(256)]
        public String PhotoUrl { get; set; }
        public byte SexId { get; set; }
		public String Address { get; set; }
		public String About { get; set; }
		
        public Sex Sex
        {
            get { return (Sex) SexId; }

            set { SexId = (byte) value; }
        }

        public User User { get; set; }

    }
}


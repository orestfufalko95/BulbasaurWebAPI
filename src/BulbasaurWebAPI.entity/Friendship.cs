using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.entity
{
    public class Friendship
    {
        [Required]
        public int SubscriberId { get; set; }
        [Required]
        public int ResponderId { get; set; }
    }
}

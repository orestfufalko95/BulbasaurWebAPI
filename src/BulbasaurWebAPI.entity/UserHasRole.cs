using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.Entity
{
    public class UserHasRole
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public RoleEnum RoleEnum
        {
            get { return (RoleEnum)RoleId; }

            set { RoleId = (int)value; }
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulbasaurWebAPI.Entity
{
    public class Role
    {
        public int _roleId { get; set; }

        public RoleEnum RoleEnum
        {
            get { return (RoleEnum)_roleId; }

            set { _roleId = (int)value; }
        }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<UserHasRole> UserRoles { get; set; }
    }
}

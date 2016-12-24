using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BulbasaurWebAPI.entity;

namespace BulbasaurWebAPI.Entity
{
    public  class User
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string SurName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        [Required]
        public UserInfo Info { get; set; }

        public ICollection<UserHasRole> UserRoles { get; set; }
        public ICollection<UserHasGame> UserGames { get; set; }
    }
}

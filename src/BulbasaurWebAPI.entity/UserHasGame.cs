using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.entity
{
    public class UserHasGame
    {
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}

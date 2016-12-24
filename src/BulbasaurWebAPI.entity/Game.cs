using System;
using System.Collections.Generic;

namespace BulbasaurWebAPI.entity
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<UserHasGame> UserGames { get; set; }
    }
}
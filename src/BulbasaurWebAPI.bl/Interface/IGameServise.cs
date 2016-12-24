using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Model;

namespace BulbasaurWebAPI.bl.Interface
{
    public interface IGameServise
    {
        void AddGame(int userId, int gameId);
        void DeleteGame(int userId, int gameId);
        List<GameDTO> GetGames(int userId);
        IEnumerable<UserInfoForSearchDTO> GetGamePlayers(long id);
    }
}

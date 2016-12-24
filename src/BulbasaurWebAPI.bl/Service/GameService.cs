using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.dal;
using BulbasaurWebAPI.entity;

namespace BulbasaurWebAPI.bl.Service
{
    public class GameService : IGameServise
    {
        private readonly UnitOfWork _unitOfWork;

        public GameService()
        {
            _unitOfWork = new UnitOfWork(new DatabaseContext());
        }

        public void AddGame(int userId, int gameId)
        {
            var user = _unitOfWork.Users.Get(userId);
            user.UserGames.Add(new UserHasGame
            {
                GameId = gameId, UserId = userId
            });
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }

        public void DeleteGame(int userId, int gameId)
        {
           var itemToremove = _unitOfWork.Users.Get(userId).UserGames.ToList()
                .Single(g => (g.UserId == userId) && (g.GameId == gameId));
            _unitOfWork.Users.Get(userId).UserGames.Remove(itemToremove);
            _unitOfWork.Complete();
        }

        public List<GameDTO> GetGames(int userId)
        {
            return _unitOfWork.Games.GetAll().Select(g => new GameDTO()
            {
                Id = g.GameId, Name = g.Name, Picture = g.ImageUrl,
                IsChosenByUser = _unitOfWork.UsersGames.GetAll().Any(h=> h.UserId == userId && h.GameId == g.GameId )
            }).ToList();
        }

        public IEnumerable<UserInfoForSearchDTO> GetGamePlayers(long id)
        {
            var players = new List<UserInfoForSearchDTO>();

            foreach (var userHasGame in _unitOfWork.UsersGames.GetAll().Where(h=> h.GameId == id))
            {
                players.AddRange( _unitOfWork.Users.GetAll().Where(u=> u.UserId == userHasGame.UserId).Select(m=>new UserInfoForSearchDTO()
                {
                    Id = m.UserId,
                    Name = m.Name,
                    SurName = m.SurName,
                    PhotoUrl = m.Info.PhotoUrl,
                }));
            }

            return players;
        }

    }
}

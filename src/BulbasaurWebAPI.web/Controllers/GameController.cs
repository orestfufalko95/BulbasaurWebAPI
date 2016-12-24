using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.Service;
using BulbasaurWebAPI.bl.utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.CodeGenerators;

namespace BulbasaurWebAPI.web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IGameServise _gameServise;

        public GameController()
        {
            this._gameServise = new GameService();
        }

        [HttpPost]
        [Route("{gameId}")]
        [Authorize]
        public void AddGameToUser([FromHeader]string Authorization, int gameId)
        {
            try
            {
                var userId =  TokenParcer.GetUserIdByToken(Authorization);
                _gameServise.AddGame(userId, gameId);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.WriteLine(e.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("{gameId}")]
        public void DeleteGameFromUser([FromHeader]string Authorization,int gameId)
        {
            try
            {
                var userId = TokenParcer.GetUserIdByToken(Authorization);
                _gameServise.DeleteGame(userId, gameId);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Console.WriteLine(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("all")]
        public IEnumerable<GameDTO> Get([FromHeader]string Authorization)
        {
            List<GameDTO> result;

            try
            {
                var userId = TokenParcer.GetUserIdByToken(Authorization);
                result = _gameServise.GetGames(userId);
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                Console.WriteLine(e);
                result = null;
            }
            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}/users")]
        public IEnumerable<UserInfoForSearchDTO> GetUsers(long id)
        {
            IEnumerable<UserInfoForSearchDTO> players = null;

            try
            {
                players = _gameServise.GetGamePlayers(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return players;
            }

            return players;
        }
    }
}

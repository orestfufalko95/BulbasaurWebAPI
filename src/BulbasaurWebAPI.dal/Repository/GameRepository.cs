using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DbContext context) : base(context)
        {
        }
    }
}
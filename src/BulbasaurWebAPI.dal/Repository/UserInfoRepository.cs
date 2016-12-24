using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(DbContext context) : base(context)
        {
        }
    }
}
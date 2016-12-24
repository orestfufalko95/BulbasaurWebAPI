using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.dal.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User LoginUser(String userEmail, string passHash);
        List<User> GetFriendsOf(int userId);
    }
}

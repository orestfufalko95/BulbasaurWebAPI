using System;

namespace BulbasaurWebAPI.dal.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserInfoRepository UsersInfo { get; }
        IMessageRepositoty Messages { get; }
        IRoleRepository Roles { get; }
        IUserHasRoleRepositoty UsersHasRoles { get; }
        IFriendshipRepository Friendships { get; }
        IGameRepository Games { get; set; }
        IUserHasGameRepository UsersGames { get; set; }
        
        int Complete();
    }
}
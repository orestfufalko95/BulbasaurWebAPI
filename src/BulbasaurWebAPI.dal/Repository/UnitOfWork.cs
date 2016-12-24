using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.dal.Repository;

namespace BulbasaurWebAPI.dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            
            Users = new UserRepository(_context);
            UsersInfo = new UserInfoRepository(_context);
            Roles = new RoleRepository(_context);
            UsersHasRoles = new UserHasRoleRepository(_context);
            Messages = new MessageRepository(_context);
            Friendships = new FriendshipRepository(_context);
            Games = new GameRepository(_context);
            UsersGames = new UserHasGameRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IUserRepository Users { get; }
        public IUserInfoRepository UsersInfo { get; }
        public IMessageRepositoty Messages { get; }
        public IRoleRepository Roles { get; }
        public IUserHasRoleRepositoty UsersHasRoles { get; }
        public IFriendshipRepository Friendships { get; }
        public IGameRepository Games { get; set; }
        public IUserHasGameRepository UsersGames { get; set; }

        public int Complete()
        {
           return _context.SaveChanges();
        }
    }
}
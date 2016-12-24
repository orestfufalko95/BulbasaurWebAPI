using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.entity;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
            
        }

        public override User Get(params object[] id)
        {
            var user = base.Get(id);
            IncludeReferenceEntitis(user);
            return user;
        }

        public override IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            var users = base.Find(predicate);
            IncludeReferenceEntitis(users);
            return users;
        }

        public override IEnumerable<User> GetAll()
        {
            var users = base.GetAll();
            IncludeReferenceEntitis(users);
            return users;
        }

        public override User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            var user = base.SingleOrDefault(predicate);
            IncludeReferenceEntitis(user);
            return user;
        }

        public User LoginUser(string userEmail, string passHash)
        {
           return Context.Set<User>().First(u => u.Email.Equals(userEmail) && u.PasswordHash.Equals(passHash));
        }

        public List<User> GetFriendsOf(int userId)
        {
            var responders = Context.Set<Friendship>().Where(p => p.SubscriberId == userId).Select(p => p.ResponderId) ;
            var subsribers = Context.Set<Friendship>().Where(p => p.ResponderId == userId).Select(p => p.SubscriberId);
            var friendsId = responders.Concat(subsribers);
            return Context.Set<User>().Where(u => friendsId.Contains(u.UserId)).Include(user => user.Info).ToList();
        }

        private void IncludeReferenceEntitis(IEnumerable<User> users)
        {
            foreach (var user in users)
                IncludeReferenceEntitis(user);
        }

        private void IncludeReferenceEntitis(User user)
        {
            Context.Entry(user).Collection(p => p.UserRoles).Load();
            Context.Entry(user).Collection(p => p.UserGames).Load();
            Context.Entry(user).Reference(p => p.Info).Load();
        }
    }
}
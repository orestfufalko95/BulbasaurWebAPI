using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class UserHasGameRepository: Repository<UserHasGame>, IUserHasGameRepository
    {
        public UserHasGameRepository(DbContext context) : base(context)
        {
        }

        public override UserHasGame Get(params object[] id)
        {
            var userGame =  base.Get(id);
            IncludeReferenceEntitis(userGame);
            return userGame;
        }

        public override IEnumerable<UserHasGame> GetAll()
        {
            var  userGames = base.GetAll();
            IncludeReferenceEntitis(userGames);
            return userGames;
        }

        public override IEnumerable<UserHasGame> Find(Expression<Func<UserHasGame, bool>> predicate)
        {
            var userGames = base.Find(predicate);
            IncludeReferenceEntitis(userGames);
            return userGames;
        }

        public override UserHasGame SingleOrDefault(Expression<Func<UserHasGame, bool>> predicate)
        {
            var userGame = base.SingleOrDefault(predicate);
            IncludeReferenceEntitis(userGame);
            return userGame;
        }

        private void IncludeReferenceEntitis(IEnumerable<UserHasGame> usersGames)
        {
            foreach (var userGame in usersGames)
                IncludeReferenceEntitis(userGame);
        }

        private void IncludeReferenceEntitis(UserHasGame user)
        {
            Context.Entry(user).Reference(p => p.Game).Load();
        }

    }
}
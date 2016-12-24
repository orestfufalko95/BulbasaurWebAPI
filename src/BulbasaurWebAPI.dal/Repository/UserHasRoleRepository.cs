using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class UserHasRoleRepository : Repository<UserHasRole>, IUserHasRoleRepositoty
    {
        public UserHasRoleRepository(DbContext context) : base(context)
        {
        }

        public override UserHasRole Get(params object[] id)
        {
            var userRole = base.Get(id);
            IncludeReferenceEntitis(userRole);
            return userRole;
        }


        public override IEnumerable<UserHasRole> Find(Expression<Func<UserHasRole, bool>> predicate)
        {
            var userRoles = base.Find(predicate);
            IncludeReferenceEntitis(userRoles);
            return userRoles;
        }

        public override IEnumerable<UserHasRole> GetAll()
        {
            var userRoles = base.GetAll();
            IncludeReferenceEntitis(userRoles);
            return userRoles;
        }

        public override UserHasRole SingleOrDefault(Expression<Func<UserHasRole, bool>> predicate)
        {
            var userRole = base.SingleOrDefault(predicate);
            IncludeReferenceEntitis(userRole);
            return userRole;
        }

        private void IncludeReferenceEntitis(IEnumerable<UserHasRole> userRoles)
        {
            foreach (var userRole in userRoles)
                IncludeReferenceEntitis(userRole);
        }

        private void IncludeReferenceEntitis(UserHasRole userRole)
        {
            Context.Entry(userRole).Reference(p => p.User).Load();
            //Context.Entry(userRole).Reference(p=> p.Role).Load();
        }


    }
}
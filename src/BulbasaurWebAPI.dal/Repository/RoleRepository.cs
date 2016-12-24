using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }


        public override Role Get(params object[] id)
        {
            var role = base.Get(id);
            IncludeReferenceEntitis(role);
            return role;
        }


        public override IEnumerable<Role> Find(Expression<Func<Role, bool>> predicate)
        {
            var roles = base.Find(predicate);
            IncludeReferenceEntitis(roles);
            return roles;
        }

        public override IEnumerable<Role> GetAll()
        {
            var roles = base.GetAll();
            IncludeReferenceEntitis(roles);
            return roles;
        }

        public override Role SingleOrDefault(Expression<Func<Role, bool>> predicate)
        {
            var role = base.SingleOrDefault(predicate);
            IncludeReferenceEntitis(role);
            return role;
        }

        private void IncludeReferenceEntitis(IEnumerable<Role> roles)
        {
            foreach (var role in roles)
                IncludeReferenceEntitis(role);
        }

        private void IncludeReferenceEntitis(Role userRole)
        {
            Context.Entry(userRole).Collection(p => p.UserRoles).Load();
        }
        
    }
}
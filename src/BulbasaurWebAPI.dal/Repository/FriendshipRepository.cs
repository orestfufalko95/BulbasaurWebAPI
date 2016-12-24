using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class FriendshipRepository: Repository<Friendship> , IFriendshipRepository
    {
        public FriendshipRepository(DbContext context) : base(context)
        {
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.dal.Interface
{
    public interface IMessageRepositoty : IRepository<Message>
    {
        List<Message> GetMessageById(int senderId, int receiverId);
        List<Message> GetLastMessages(List<int> friendsIds,int userId);
        void MarkReadMessage(IEnumerable<Message> messages);
    }
}

using System.Collections.Generic;
using System.Linq;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BulbasaurWebAPI.dal.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepositoty
    {
        public MessageRepository(DbContext context) : base(context)
        {
            
        }

        public List<Message> GetMessageById(int senderId, int receiverId)
        {
            var userMessages = new List<Message>();
            userMessages.AddRange(
                Context.Set<Message>().Where(m => ((m.SenderId == senderId) && (m.ReceiverId == receiverId)) ||
                                                  ((m.ReceiverId == senderId) && (m.SenderId == receiverId)))
                                                  .OrderBy(d => d.DateTime));
            return userMessages;
        }

        public List<Message> GetLastMessages(List<int> friendsIds, int userId)
        {
            var maxDate = new List<Message>();
         
            foreach (var friendId in friendsIds)
            {
               var messages = GetMessageById(userId, friendId);
                maxDate.Add(messages.Last());
                
               //maxDate.AddRange(new[] { messages.ElementAt(messages.Count - 1) });
               //maxDate.AddRange(messages.Take(messages.Count - 1));
            }
            return maxDate;
        }

        public void MarkReadMessage(IEnumerable<Message> messages)
        {
            foreach (var message in messages)
            {
                message.IsRead = true;
            }
        }
    }
}
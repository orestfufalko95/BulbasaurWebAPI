using System.Collections.Generic;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Interface
{
    public interface IMessageService
    {
        Task AddFirstMessage(int senderId, MessageInputDTO messageInputDto);
        Task AddMessage(int senderId, MessageInputDTO messageInputDto);
        AllMessageResponseDTO GetMessages(int userId, int friendId);
        List<LastMessageDTO> GetLastMessages(int userId);
    }
}
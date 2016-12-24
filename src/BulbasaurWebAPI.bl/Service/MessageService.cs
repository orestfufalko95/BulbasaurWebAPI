using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.exceptions;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.dal;
using BulbasaurWebAPI.dal.Interface;
using BulbasaurWebAPI.entity;
using BulbasaurWebAPI.Entity;
using Microsoft.AspNetCore.Http;

namespace BulbasaurWebAPI.bl.Service
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService()
        {
            _unitOfWork = new UnitOfWork(new DatabaseContext());
        }

        public async Task AddFirstMessage(int senderId, MessageInputDTO messageInputDto)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine($"sender --- {senderId}");
            Console.WriteLine($"receiver --- {messageInputDto.ReceiverId}");
            _unitOfWork.Friendships.Add(new Friendship
            {
                SubscriberId = senderId,
                ResponderId = messageInputDto.ReceiverId
            });
            await AddMessage(senderId, messageInputDto);
     
        }
        public async Task AddMessage(int senderId, MessageInputDTO messageInput)
        {

            if (string.IsNullOrWhiteSpace(messageInput.Message) && messageInput.Media == null)
            {
                throw new MessageAndMediaEmptyException("epty message data");
            }
            _unitOfWork.Messages.Add(new Message
            {
                SenderId = senderId,
                ReceiverId = messageInput.ReceiverId,
                Text = messageInput.Message,
                DateTime = System.DateTime.Now,
                IsRead = false,

            });
            _unitOfWork.Complete();
            if (messageInput.Media != null)
            {
                var message = _unitOfWork.Messages.Find(m => m.SenderId == senderId && m.ReceiverId == messageInput.ReceiverId)
                     .Last();
                Console.WriteLine("===================================================");
                Console.WriteLine(messageInput.Media.FileName);
                message.MediaName = MediaPath.GetFileName(messageInput.Media.FileName);
                message.AttachmentUrl = await AddMedia(messageInput.Media, message.MessageId);
               

                _unitOfWork.Messages.Update(message);
                _unitOfWork.Complete();
            }
        }

        public AllMessageResponseDTO GetMessages(int userId, int friendId)
        {
            var messages =  _unitOfWork.Messages.GetMessageById(userId, friendId);
            _unitOfWork.Messages.MarkReadMessage(messages.Where(m => m.SenderId == friendId));
            _unitOfWork.Complete();

            var friend = _unitOfWork.Users.Get(friendId);
            var user = _unitOfWork.Users.Get(userId);

            var result = new AllMessageResponseDTO()
            {
                Messages = messages,
                CurrentUserName = user.Name,
                CurrentUserImageUrl = user.Info.PhotoUrl,
                FriendImageUrl = friend.Info.PhotoUrl,
                FriendName = friend.Name,
                FriendSurame = friend.SurName
            };

            return result;
        }

        public List<LastMessageDTO> GetLastMessages(int userId)
        {
            List<User> friendsList = _unitOfWork.Users.GetFriendsOf(userId);
       
            List<int> friendsIds = new List<int>();
            friendsIds.AddRange(friendsList.Select(f => f.UserId));
            List<Message> messages = new List<Message>();
            messages.AddRange(_unitOfWork.Messages.GetLastMessages(friendsIds, userId));

            List<LastMessageDTO> lastMessages = friendsList.Select((t, i) => new LastMessageDTO
            {
                FriendId = t.UserId,
                Message = messages[i].Text,
                Time = messages[i].DateTime,
                IsRead = messages[i].IsRead,
                Name = t.Name,
                SurName = t.SurName,
                PhotoUrl = t.Info.PhotoUrl,
                SenderShortInfo = new LastMessageDTO.MessageSenderShortInfo(_unitOfWork.Users.Get(messages[i].SenderId))

            }).ToList();
            

            return lastMessages.OrderByDescending(m => m.Time).ToList();
        }

        private async Task<string> AddMedia(IFormFile media, int messageId)
        {
            if (media == null){
                return null;
            }
            IMediaService mediaService = new MediaService();
            return await mediaService.SaveMedia(media, messageId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.bl.Validator;
using BulbasaurWebAPI.dal;
using BulbasaurWebAPI.entity;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Service
{
    public class PersonService : IPersonService
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonService()
        {
            _unitOfWork = new UnitOfWork(new DatabaseContext());
        }

        public UserProfileDTO GetLoggedUserProfileByToken(string token)
        {
            var id = TokenParcer.GetUserIdFromToken(token).GetAwaiter().GetResult();

            return GetLoggedUserProfileById(id, id);
        }

        public UserProfileDTO GetLoggedUserProfileById(int id, int currentUserId)
        {
            User user;
            try
            {
                user = _unitOfWork.Users.Find(u => u.UserId.Equals(id)).First();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return new UserProfileDTO();
            }

            var loggedUserProfileDto = new UserProfileDTO
            {
                userDTO = GetUserProfileDTOFromUser(user),
                games = GetUserGames(user.UserId, currentUserId),
                friends = GetUserFriendsUserProfileDTO(user.UserId)
            };

            return loggedUserProfileDto;
        }

        public async Task AddUserInfo(int userId, UserInfoDTO userInfo)
        {

            IMediaService media = new MediaService();

            var currentUrl = MediaPath.GetFolderPath(_unitOfWork.Users.Get(userId).Info.PhotoUrl);
            if (userInfo.Photo != null)
            {
                var url = await media.SavePersonImage(userInfo.Photo, userId, currentUrl);
                _unitOfWork.Users.Get(userId).Info.PhotoUrl = url;

            }
            else
            {
                //if (currentUrl != null)
                //{
                //    File.Delete(currentUrl);
                //}
            }
            //_unitOfWork.Users.Get(userId).Info.PhotoUrl = url;
            _unitOfWork.Users.Get(userId).Info.Address = userInfo.Address;
            _unitOfWork.Users.Get(userId).Info.About = userInfo.About;
            _unitOfWork.Users.Get(userId).Info.SexId = userInfo.Sex;
            _unitOfWork.Complete();
        }

        public async Task<WriteUserResponseDTO> EditProfile(int userId, EditProfileDTO data)
        {
            IUserDataValidator validator = new UserDataValidator();
            var userData = new UserDataDTO
            {
                Email = data.Email,
                Name = data.Name,
                Surname = data.Surname
            };
           
            var editResponce = new WriteUserResponseDTO
            {
                IsSuccessful = true,
                PropertyInfos = new List<WriteUserResponseDTO.PropertyInfo>()
            };
            var user = _unitOfWork.Users.Find(u => u.Email.Equals(data.Email) && u.UserId != userId).FirstOrDefault();
            validator.EditProfileValidation(user, userData, editResponce);
            if (editResponce.IsSuccessful)
            {
                EditUserData(userId, userData);
                await AddUserInfo(userId, data);
            }
            return editResponce;
        }

        private void EditUserData(int userId, UserDataDTO userData)
        {
            var user = _unitOfWork.Users.Get(userId);
            user.Name = userData.Name;
            user.SurName = userData.Surname;
            user.Email = userData.Email;
            _unitOfWork.Users.Update(user);
        }

        private List<UserDTO> GetUserFriendsUserProfileDTO(int userId)
        {
            List<User> list;
            try
            {
                list = _unitOfWork.Users.GetFriendsOf(userId);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetUserFriendsUserProfileDTO===============================");
                Console.WriteLine(e);
                return new List<UserDTO>();
            }

            return list.Select(GetUserProfileDTOFromUser).ToList();
        }

        private List<GameDTO> GetUserGames(int userId, int currentUserId)
        {
            var gameDtos = new List<GameDTO>();

            try
            {
                var t = _unitOfWork.Users.Get(userId).UserGames;
                gameDtos.AddRange(t.Select(game => _unitOfWork.Games.Get(game.GameId)).Select(g => new GameDTO()
                {
                    Id = g.GameId, Name = g.Name, Picture = g.ImageUrl,
                    IsChosenByUser = _unitOfWork.UsersGames.GetAll().Any(h => h.UserId == currentUserId && h.GameId == g.GameId)
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<GameDTO>();
            } 
            return  gameDtos;
        }

        private UserDTO GetUserProfileDTOFromUser(User user)
        {
            return new UserDTO()
            {
                Id = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Surname = user.SurName,
                Picture = user.Info.PhotoUrl,
                Address = user.Info.Address,
                About = user.Info.About,
                Sex = user.Info.SexId.ToString(),
                Online = true
            };
        }

        public IEnumerable<UserInfoForSearchDTO> getAllUsers()
        {
            var allUsers = _unitOfWork.Users.GetAll().Select(u => new UserInfoForSearchDTO()
            {
                Id = u.UserId,
                Name = u.Name,
                SurName = u.SurName,
                PhotoUrl = u.Info.PhotoUrl
            }
            );

            return allUsers;
        }
        
        public IEnumerable<UserInfoForSearchDTO> getFriendsById(long id)
        {
            return _unitOfWork.Users.GetFriendsOf((int) id).Select(u => new UserInfoForSearchDTO()
            {
                Id = u.UserId,
                Name = u.Name,
                SurName = u.SurName,
                PhotoUrl = u.Info.PhotoUrl
            });
        }
    }
}

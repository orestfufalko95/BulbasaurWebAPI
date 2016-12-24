using System.Collections.Generic;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Model;

namespace BulbasaurWebAPI.bl.Interface
{
    public interface IPersonService
    {
        UserProfileDTO GetLoggedUserProfileByToken(string token);
        UserProfileDTO GetLoggedUserProfileById(int id, int currentUserId);
        Task AddUserInfo(int userId, UserInfoDTO userInfo);
        Task<WriteUserResponseDTO> EditProfile(int userId, EditProfileDTO data);
        IEnumerable<UserInfoForSearchDTO> getAllUsers();

        IEnumerable<UserInfoForSearchDTO> getFriendsById(long id);
    }
}
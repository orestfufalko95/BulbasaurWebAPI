using BulbasaurWebAPI.bl.Model;

namespace BulbasaurWebAPI.bl.Interface
{
    public interface ILoginService
    {
        UserProfileDTO Login(LoginInputDTO loginInputDTO);
        WriteUserResponseDTO SignUp(UserDataDTO userProfile);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Interface
{
    interface IUserDataValidator
    {
        void ValidateUserData(User sameUser, UserDataDTO userProfile, WriteUserResponseDTO signUpResponse);
        void EditProfileValidation(User sameUser, UserDataDTO userProfile, WriteUserResponseDTO signUpResponse);
    }
}

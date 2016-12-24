using System;
using System.Text.RegularExpressions;
using BulbasaurWebAPI.bl.Interface;
using BulbasaurWebAPI.bl.Model;
using BulbasaurWebAPI.Entity;

namespace BulbasaurWebAPI.bl.Validator
{
    public class UserDataValidator : IUserDataValidator
    {
        private UserDataDTO _userProfile;
        private WriteUserResponseDTO _signUpResponse;

        public void ValidateUserData(User sameUser,UserDataDTO userProfile, WriteUserResponseDTO signUpResponse)
        {
            if (isEmailUnique(sameUser, signUpResponse))
            {
                _userProfile = userProfile;
                _signUpResponse = signUpResponse;
                ValidateEmail();
                ValidateName();
                ValidateSurName();
                ValidatePassword();
            }
        }

        public void EditProfileValidation(User sameUser, UserDataDTO userProfile, WriteUserResponseDTO signUpResponse)
        {
            if (isEmailUnique(sameUser, signUpResponse))
            {
                _userProfile = userProfile;
                _signUpResponse = signUpResponse;
                ValidateEmail();
                ValidateName();
                ValidateSurName();
            }
           
        }


        private void ValidateEmail()
        {
            var isEmail = false;
            try
            {
                isEmail = Regex.IsMatch(_userProfile.Email,
                    @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                    RegexOptions.IgnoreCase);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (isEmail) return;
            _signUpResponse.IsSuccessful = false;
            _signUpResponse.PropertyInfos.Add(
                new WriteUserResponseDTO.PropertyInfo
                {
                    PropertyName = "Email",
                    Message = "Email is NOT valid"
                });
        }

        private bool isEmailUnique(User sameUser, WriteUserResponseDTO signUpResponse)
        {
            if (sameUser == null) return true;
            signUpResponse.IsSuccessful = false;
            signUpResponse.PropertyInfos.Add(new WriteUserResponseDTO.PropertyInfo()
            {
                PropertyName = "Email",
                Message = "This email already exist"
            });
            return false;
        }

        private void ValidateName()
        {
            var isName = false;
            try
            {
                isName = Regex.IsMatch(_userProfile.Name, @"^(?=.*[a-zA-Z])(?=.+[a-zA-Z0-9]).{1,50}$", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (isName) return;
            _signUpResponse.IsSuccessful = false;
            _signUpResponse.PropertyInfos.Add(
                new WriteUserResponseDTO.PropertyInfo
                {
                    PropertyName = "Name",
                    Message = "Name should start from letter and contain more than 1 letter"
                });
        }

        private void ValidateSurName()
        {
            var isSurName = false;
            try
            {
                isSurName = Regex.IsMatch(_userProfile.Surname, @"^(?=.*[a-zA-Z])(?=.+[a-zA-Z0-9]).{1,50}$", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (isSurName) return;
            _signUpResponse.IsSuccessful = false;
            _signUpResponse.PropertyInfos.Add(
                new WriteUserResponseDTO.PropertyInfo
                {
                    PropertyName = "SurName",
                    Message = "SurName should start from letter and contain more than 1 letter"
                });
        }

        private void ValidatePassword()
        {
            var isPassword = false;
            try
            {
                isPassword = Regex.IsMatch(_userProfile.Password, @"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).\S{6,20}$", RegexOptions.IgnoreCase);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            if (isPassword) return;
            _signUpResponse.IsSuccessful = false;
            _signUpResponse.PropertyInfos.Add(
                new WriteUserResponseDTO.PropertyInfo
                {
                    PropertyName = "Password",
                    Message = "Password should contain more than 6 symbols and at least one letter, one number and one special symbol"
                });
        }
    }
}
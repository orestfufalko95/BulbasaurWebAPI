using BulbasaurWebAPI.bl.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.Model;
using IdentityModel.Client;
using System.Net;
using BulbasaurWebAPI.bl.utils;
using BulbasaurWebAPI.bl.Validator;
using BulbasaurWebAPI.dal;
using BulbasaurWebAPI.Entity;
using Microsoft.CodeAnalysis.Text;

namespace BulbasaurWebAPI.bl.Service
{
    public class LoginService : ILoginService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IUserDataValidator _validator;
        private readonly PersonService _personService;

        public LoginService()
        {
            _personService = new PersonService();
            _validator = new UserDataValidator();
            _unitOfWork = new UnitOfWork(new DatabaseContext());
        }


        public UserProfileDTO Login(LoginInputDTO loginInputDTO)
        {
            if (loginInputDTO == null)
            {
                Console.WriteLine("loginInputDTO == null!!!!!!!!!!!!!!!!!!!");
                return null;
            }

            var loggedUserProfileDto = new UserProfileDTO();
            var tokenResponse = MainAsync(loginInputDTO).GetAwaiter().GetResult();

            if (tokenResponse.HttpStatusCode != HttpStatusCode.OK) return loggedUserProfileDto;

            loggedUserProfileDto = _personService.GetLoggedUserProfileByToken(tokenResponse.AccessToken);

            loggedUserProfileDto.tokenResponse = tokenResponse.AccessToken;

            return loggedUserProfileDto;
        }



        private async Task<TokenResponse> MainAsync(LoginInputDTO model)
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, model.ClientId, model.ClientSecret);
            var tokenResponse =
                await tokenClient.RequestResourceOwnerPasswordAsync(model.Email, model.Password, model.Scope);

            if (tokenResponse.IsError)
            {
                return new TokenResponse(HttpStatusCode.Unauthorized, "Unautorized");
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            return tokenResponse;
        }

        public WriteUserResponseDTO SignUp(UserDataDTO userProfile)
        {

            var signUpResponse = new WriteUserResponseDTO
            {
                IsSuccessful = true,
                PropertyInfos = new List<WriteUserResponseDTO.PropertyInfo>()
            };

            User user = null;
            try
            {
                user = _unitOfWork.Users.Find(u => u.Email.Equals(userProfile.Email)).First();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

                _validator.ValidateUserData(user, userProfile, signUpResponse);
        

            if (signUpResponse.IsSuccessful)
            {
                SaveUser(userProfile);
            }

            Console.WriteLine(signUpResponse.IsSuccessful);

            return signUpResponse;

        }

        private void SaveUser(UserDataDTO userProfile)
        {
            _unitOfWork.Users.Add(new User
            {
                Email = userProfile.Email,
                PasswordHash = HashcodeUtil.HashPassword( userProfile.Password),
                Name = userProfile.Name,
                SurName = userProfile.Surname,
                Info = new UserInfo()
            });
            _unitOfWork.Complete();
        }
    }
}

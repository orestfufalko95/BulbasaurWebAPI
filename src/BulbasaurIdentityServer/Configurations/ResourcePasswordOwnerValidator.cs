using BulbasaurWebAPI.dal;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulbasaurWebAPI.bl.utils;
using IdentityModel;
using IdentityModel.Client;
using TokenResponse = IdentityServer4.Models.TokenResponse;
using BulbasaurWebAPI.Entity;

namespace BulbasaurIdentityServer.Configurations
{
    public class ResourcePasswordOwnerValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            DatabaseContext contextDB = new DatabaseContext();

            UnitOfWork unitOfWork = new UnitOfWork(contextDB);

            Console.WriteLine($"name: {context.UserName} pass: {context.Password}==============================================");

            User user = null;

            bool isValidCredential = false;

            try { 
                //user = unitOfWork.Users.LoginUser(context.UserName, HashcodeUtil.GetHashCodeString( context.Password ) );
                //unitOfWork.Complete();

                user = unitOfWork.Users.GetAll().First(u => u.Email.Equals(context.UserName));
                isValidCredential = HashcodeUtil.VerifyHashedPassword(user.PasswordHash, context.Password);

            } catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
            }
            if(!isValidCredential)
            {
                Console.WriteLine("ResourcePasswordOwnerValidator:  User is null !!!!!!!!!!!!!");
                context.Result = null;
            }
            else
            {
                Console.WriteLine("ResourcePasswordOwnerValidator:  User is NOT NOT NOT null !!!!!!!!!!!!!");

                context.Result = new GrantValidationResult(user.UserId.ToString(), "password");
            }

            return Task.FromResult(0);
        }
    }
}

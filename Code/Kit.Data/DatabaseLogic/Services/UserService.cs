using Kit.Data.Identity;
using Kit.Data.Tools;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kit.Data.DatabaseLogic.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext context;
        private readonly IAppSettings appSettings;
        private readonly IHashProvider hashProvider;

        public UserService(DatabaseContext context, IAppSettings appSettings, IHashProvider hashProvider)
        {
            this.context = context;
            this.appSettings = appSettings;
            this.hashProvider = hashProvider;
        }

        public User Authenticate(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentNullException(nameof(userName));
                }
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }               

                var user = context.Users.FirstOrDefault(t => string.Equals(t.UserName, userName));
                if (user == null)
                {
                    throw new UserNotExistException();
                }

                if (hashProvider.CheckHash(password, user.Password))
                {
                    return user;
                }

                throw new BadPasswordException();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public User GetById(int id)
        {
            try
            {               
                return context.Users.FirstOrDefault(t => t.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public User Register(User user)
        {
            try
            {
                var s = hashProvider.CheckHash(user.Password, "cbpD4lpgff3QFm7C//+HMZCErbwvhHtrcC6ml55akVAvPBY8");

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }
    }
}

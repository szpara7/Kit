using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.DatabaseLogic.Services
{
    public interface IUserService
    {
        User GetById(int id);

        User Authenticate(string userName, string password);
        User Register(User user);
    }
}

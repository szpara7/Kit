using Kit.Data.DatabaseLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.Identity
{
    public interface IHashProvider
    {
        bool CheckHash(string password, string hash);
        string CreateHash(string password);
    }
}

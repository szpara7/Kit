using Kit.Data.DatabaseLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.Identity
{
    public interface IJwtProvider
    {
        string WriteToken(User user, bool rememberMe);
    }
}

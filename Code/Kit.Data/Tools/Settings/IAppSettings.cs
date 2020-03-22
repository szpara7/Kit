using System;
using System.Collections.Generic;
using System.Text;

namespace Kit.Data.Tools
{
    public interface IAppSettings
    {
        string JwtKey { get; set; }
        string JwtIssuer { get; set; }

    }
}

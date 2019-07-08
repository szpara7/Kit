using Kit.Data.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kit.Web.Model
{
    public class AppSettings : IAppSettings
    {       
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string HashSalt { get; set; }
    }
}

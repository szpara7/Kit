using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Kit.Data.DatabaseLogic;
using Kit.Data.Tools;

namespace Kit.Data.Identity
{
    public class IdentityHelper : IHashProvider, IJwtProvider
    {
        private readonly IAppSettings appSettings;

        public IdentityHelper(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public bool CheckHash(string password, string hash)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }
                if (string.IsNullOrEmpty(hash))
                {
                    throw new ArgumentNullException(nameof(hash));
                }

                byte[] passwordBytes = Convert.FromBase64String(hash);

                byte[] salt = new byte[16];
                Array.Copy(passwordBytes, 0, salt, 0, 16);

                var rfc = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] newHash = rfc.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (passwordBytes[i + 16] != newHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public string CreateHash(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentNullException(nameof(password));
                }               

                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var rfc = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = rfc.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                return Convert.ToBase64String(hashBytes);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        public string WriteToken(User user, bool rememberMe)
        {
            try
            {
                //    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettings.JwtKey));
                //    var signingCreditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                //    DateTime? expire = DateTime.UtcNow.AddMinutes(30);
                //    if (rememberMe == true)
                //    {
                //        expire = null;
                //    }

                //    var tokenHandler = new JwtSecurityTokenHandler();
                //    var securityTokenDescriptor = new SecurityTokenDescriptor()
                //    {
                //        Expires = expire,
                //        Audience = appSettings.JwtIssuer,
                //        Issuer = appSettings.JwtIssuer,
                //        SigningCredentials = signingCreditials//,
                //        //Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                //        //{
                //        //    new Claim(ClaimTypes.Name, user.UserName),
                //        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //        //    new Claim(ClaimTypes.Email, user.Email)
                //        //})
                //    };

                //    var token = tokenHandler.CreateToken(securityTokenDescriptor);
                //    return tokenHandler.WriteToken(token);
                return "";
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}

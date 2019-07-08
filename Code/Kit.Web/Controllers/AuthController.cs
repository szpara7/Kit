using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kit.Data.DatabaseLogic;
using Kit.Data.DatabaseLogic.Services;
using Kit.Data.Identity;
using Kit.Data.Tools;
using Kit.Web.Controllers.Model;
using Kit.Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kit.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(IUserService userService, IAppSettings appSettings, IJwtProvider jwtProvider)
        {
            this._userService = userService;
            this._jwtProvider = jwtProvider;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }

            try
            {
                User user = _userService.Authenticate(request.UserName, request.Password);
                var token = _jwtProvider.WriteToken(user, request.RememberMe);

                return Ok(token);
            }
            catch (UserNotExistException)
            {
                ModelState.AddModelError(nameof(request.UserName), $"User: {request.UserName} doesn't exist in Database");
                return BadRequest(ModelState);
            }
            catch(BadPasswordException)
            {
                ModelState.AddModelError(nameof(request.Password), $"Wrong password");
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
        
        [Route("register")]
        [HttpGet]
        public async Task<IActionResult> Register(string password)
        {
            var s = _userService.Register(new Data.DatabaseLogic.User { Password = password });


            return Ok();
        }
    }
}
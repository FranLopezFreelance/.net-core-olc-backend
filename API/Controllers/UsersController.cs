using Application.Security;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //https://localhost:44364/api/users
    [AllowAnonymous]
    public class UsersController: ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserPublicData>> Login(Login.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserPublicData>> Register(Register.Execute parameters)
        {
            return await Mediator.Send(parameters);
        }

        [HttpGet]
        public async Task<ActionResult<UserPublicData>> GetUserSession()
        {
            return await Mediator.Send(new CurrentUser.Execute());
        }
    }
}

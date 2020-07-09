using Application.Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class CurrentUser
    {
        public class Execute: IRequest<UserPublicData> { }

        public class Handler : IRequestHandler<Execute, UserPublicData>
        {
            private readonly UserManager<User> _userManager;
            private readonly IJWTGenerator _jwtGenerator;
            private readonly IUserSession _userSession;
            public Handler(UserManager<User> userManager, IJWTGenerator jWTGenerator, IUserSession userSession)
            {
                _userManager = userManager;
                _jwtGenerator = jWTGenerator;
                _userSession = userSession;
            }
            public async Task<UserPublicData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSession.getUserName());
                return new UserPublicData
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    ProfileImage = null
                };
            }
        }
    }
}

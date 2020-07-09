using Application.Contracts;
using Application.ErrorsHandler;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class Login
    {
        public class Execute: IRequest<UserPublicData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Validation: AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(u => u.Email).NotEmpty();
                RuleFor(u => u.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, UserPublicData>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IJWTGenerator _jwtGenerator;
            public Handler(UserManager<User> userManager, SignInManager<User> signInManager, IJWTGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<UserPublicData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user == null)
                {
                    throw new HandlerExceptions(HttpStatusCode.Unauthorized);
                }
                var status = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (status.Succeeded)
                {
                    return new UserPublicData { 
                        FullName = user.FullName,
                        Token = _jwtGenerator.TokenGenerate(user),
                        UserName = user.UserName,
                        Email = user.Email,
                        ProfileImage = null
                    };
                }
                    throw new HandlerExceptions(HttpStatusCode.Unauthorized);
            }
        }
    }
}

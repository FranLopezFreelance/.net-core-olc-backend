using Application.Contracts;
using Application.ErrorsHandler;
using DataAccess;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Security
{
    public class Register
    {
        public class Execute: IRequest<UserPublicData>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class Validation : AbstractValidator<Execute>
        {
            public Validation()
            {
                RuleFor(u => u.UserName).NotEmpty();
                RuleFor(u => u.FirstName).NotEmpty();
                RuleFor(u => u.LastName).NotEmpty();
                RuleFor(u => u.Email).NotEmpty();
                RuleFor(u => u.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, UserPublicData>
        {
            private readonly DataContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJWTGenerator _jwtGenerator;
            public Handler(DataContext context, UserManager<User> userManager, IJWTGenerator JwtGenerator)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = JwtGenerator;
            }
            public async Task<UserPublicData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var existsEmail = await _context.Users.Where(u => u.Email == request.Email).AnyAsync();
                if (existsEmail)
                {
                    throw new HandlerExceptions(HttpStatusCode.BadRequest, new { message = "El Email ingresado ya está registrado" });
                }

                var existsUserName = await _context.Users.Where(u => u.UserName == request.UserName).AnyAsync();
                if (existsUserName)
                {
                    throw new HandlerExceptions(HttpStatusCode.BadRequest, new { message = "El Nombre usuario ingresado ya está registrado" });
                }
                var user = new User
                {
                    FullName = request.FirstName + " " + request.LastName,
                    Email = request.Email,
                    UserName = request.UserName
                };
                var status = await _userManager.CreateAsync(user, request.Password);
                if (status.Succeeded)
                {
                    return new UserPublicData 
                    {
                        FullName = user.FullName,
                        Token = _jwtGenerator.TokenGenerate(user),
                        UserName = user.UserName,
                        Email = user.Email
                    };
                }

                throw new Exception("No se pudo crear el Usuario");
            }
        }
    }
}

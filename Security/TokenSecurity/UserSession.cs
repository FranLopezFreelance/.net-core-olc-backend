using Application.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Security.TokenSecurity
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _accesor;
        public UserSession(IHttpContextAccessor accesor)
        {
            _accesor = accesor;
        }
        public string getUserName()
        {
            var userName = _accesor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}

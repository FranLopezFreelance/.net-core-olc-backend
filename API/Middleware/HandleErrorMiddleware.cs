using Application.ErrorsHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class HandleErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HandleErrorMiddleware> _logger;
        public HandleErrorMiddleware(RequestDelegate next, ILogger<HandleErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleErrorAsync(context, e, _logger);
            }
            
        }

        private async Task HandleErrorAsync(HttpContext context, Exception e, ILogger<HandleErrorMiddleware> logger)
        {
            object errors = null;
            switch (e)
            {
                case HandlerExceptions he:
                    logger.LogError(e, "Handle Error");
                    errors = he.Errors;
                    context.Response.StatusCode = (int) he.Code;
                    break;
                case Exception exc:
                    logger.LogError(e, "Server Error");
                    errors = string.IsNullOrWhiteSpace(exc.Message) ? "Error" : exc.Message;
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors!=null)
            {
                var response = JsonConvert.SerializeObject(new { errors });
                await context.Response.WriteAsync(response);            
            }
        }
    }
}

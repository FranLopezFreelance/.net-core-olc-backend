using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.ErrorsHandler
{
    public class HandlerExceptions: Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public HandlerExceptions(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}

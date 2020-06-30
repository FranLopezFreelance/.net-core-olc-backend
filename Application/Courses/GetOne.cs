using Application.ErrorsHandler;
using DataAccess;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class GetOne
    {
        public class Execute : IRequest<Course>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, Course>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Course> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.Id);
                if (course == null)
                {
                    //throw new Exception("No existe el curso que desea eliminar");
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }
                return course;
            }
        }
    }
}

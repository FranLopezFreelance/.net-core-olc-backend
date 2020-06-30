using Application.ErrorsHandler;
using DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Delete
    {
        public class Execute: IRequest
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Execute>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = await this._context.Course.FindAsync(request.Id);
                if(course == null)
                {
                    //throw new Exception("No existe el curso que desea eliminar");
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }
                this._context.Remove(course);
                var status = await this._context.SaveChangesAsync();
                if (status > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Ha ocurrido un error inesperado");
            }
        }
    }
}

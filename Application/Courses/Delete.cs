using Application.ErrorsHandler;
using DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Execute>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                //Elimino los instructores 
                var instructorsDB = _context.CourseInstructor.Where(ci => ci.CourseId == request.Id);
                foreach(var instructorDB in instructorsDB)
                {
                    _context.CourseInstructor.Remove(instructorDB);
                }
                
                //Elimino los comentarios
                var commentsDB = _context.Comment.Where(c => c.CourseId == request.Id);
                foreach(var commentDB in commentsDB)
                {
                    _context.Comment.Remove(commentDB);
                }

                //Elimino el precio
                var priceDB = _context.Price.Where(p => p.CourseId == request.Id).FirstOrDefault();
                if (priceDB != null)
                {
                    _context.Price.Remove(priceDB);
                }
                
                //Elimino el curso
                var course = await _context.Course.FindAsync(request.Id);
                if(course == null)
                {
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }
                _context.Remove(course);
                var status = await _context.SaveChangesAsync();
                if (status > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Ha ocurrido un error inesperado");
            }
        }
    }
}

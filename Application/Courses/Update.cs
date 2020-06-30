using Application.ErrorsHandler;
using DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Update
    {
        public class Execute : IRequest
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>
        {
            public ExecuteValidation()
            {
                RuleFor(c => c.Title).NotEmpty();
                RuleFor(c => c.Description).NotEmpty();
                RuleFor(c => c.PublicationDate).NotEmpty();
            }
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
                var course = await this._context.Course.FindAsync(request.CourseId);
                if (course == null)
                {
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;
                var status = await _context.SaveChangesAsync();
                if(status > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Ha ocurrido un error al querer actualizar los datos");
            }
        }
    }
}

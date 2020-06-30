using DataAccess;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Create
    {
        public class Execute: IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime PublicationDate { get; set; }
        }

        public class ExecuteValidation: AbstractValidator<Execute>
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
                var course = new Course
                {
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate
                };
                this._context.Course.Add(course);
                var status = await _context.SaveChangesAsync();
                if (status > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Ocurrió un error al intentar guardar los datos");
            }
        }
    }
}

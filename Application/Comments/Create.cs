using DataAccess;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments
{
    public class Create
    {
        public class Execute: IRequest
        {
            public Guid CommentId { get; set; }
            public string Student { get; set; }
            public int Rate { get; set; }
            public string Text { get; set; }
            public Guid CourseId { get; set; }
        }

        public class Validator : AbstractValidator<Execute>
        {
            public Validator()
            {
                RuleFor(c => c.Student).NotEmpty();
                RuleFor(c => c.Rate).NotEmpty();
                RuleFor(c => c.Text).NotEmpty();
                RuleFor(c => c.CourseId).NotEmpty();
            }
        }

        public class Handler: IRequestHandler<Execute>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var comment = new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Student = request.Student,
                    Rate = request.Rate,
                    Text = request.Text,
                    CourseId = request.CourseId,
                    CreationDate = DateTime.UtcNow
                };

                _context.Comment.Add(comment);

                var status = await _context.SaveChangesAsync();

                if (status > 0) {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el comentario");   
            }
        }
    }
}

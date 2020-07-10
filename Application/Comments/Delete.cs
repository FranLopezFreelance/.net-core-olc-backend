using DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Comments
{
    public class Delete
    {
        public class Execute: IRequest
        {
            public Guid CommentId { get; set; }
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
                var comment = await _context.Comment.FindAsync(request.CommentId);
                if (comment==null)
                {
                    throw new Exception("No se encontró el comentario");
                }

                _context.Remove(comment);

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

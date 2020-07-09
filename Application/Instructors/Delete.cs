using DataAccess.DapperConnection.Instructor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructors
{
    public class Delete
    {
        public class Execute: IRequest
        {
            public Guid InstructorId { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly IInstructor _repository;
            public Handler(IInstructor repository)
            {
                _repository = repository;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                await _repository.Delete(request.InstructorId);
                return Unit.Value;
            }
        }
    }
}

using Application.ErrorsHandler;
using DataAccess.DapperConnection.Instructor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructors
{
    public class Get
    {
        public class Execute: IRequest<InstructorModel>
        {
            public Guid InstructorId { get; set; }
        }

        public class Handle : IRequestHandler<Execute, InstructorModel>
        {
            private readonly IInstructor _repository;
            public Handle(IInstructor repository)
            {
                _repository = repository;
            }

            async Task<InstructorModel> IRequestHandler<Execute, InstructorModel>.Handle(Execute request, CancellationToken cancellationToken)
            {
                var instructor = await _repository.Get(request.InstructorId);
                return instructor;
            }
        }
    }
}

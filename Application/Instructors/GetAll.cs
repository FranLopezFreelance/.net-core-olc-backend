using DataAccess.DapperConnection.Instructor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructors
{
    public class GetAll
    {
        public class Execute: IRequest<IEnumerable<InstructorModel>> { }

        public class Handler : IRequestHandler<Execute, IEnumerable<InstructorModel>>
        {
            private readonly IInstructor _repository;

            public Handler(IInstructor instructorRepository)
            {
                _repository = instructorRepository;
            }

            public async Task<IEnumerable<InstructorModel>> Handle(Execute request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll();
            }
        }
    }
}

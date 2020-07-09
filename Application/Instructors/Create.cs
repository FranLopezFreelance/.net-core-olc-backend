using DataAccess.DapperConnection.Instructor;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructors
{
    public class Create
    {
        public class Execute: IRequest
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }

        public class Validator: AbstractValidator<Execute>
        {
            public Validator()
            {
                RuleFor(i => i.Name).NotEmpty();
                RuleFor(i => i.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly IInstructor _repository;

            public Handler(IInstructor instructorRepository)
            {
                _repository = instructorRepository;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                await _repository.Create(request.Name, request.LastName);
                return Unit.Value;
            }
        }
    }
}

using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class GetAll
    {
        public class Execute : IRequest<List<Course>> { };
        public class Handler : IRequestHandler<Execute, List<Course>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                this._context = context;
            }
            public async Task<List<Course>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var courses = await _context.Course.ToListAsync();
                return courses;
            }

        }
    }
}

using AutoMapper;
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
        public class Execute : IRequest<List<CourseDTO>> { };
        public class Handler : IRequestHandler<Execute, List<CourseDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CourseDTO>> Handle(Execute request, CancellationToken cancellationToken)
            {
                //Obtengo los cursos con sus relaciones
                var courses = await _context.Course
                        .Include(c => c.CommentList)
                        .Include(c => c.PromoPrice)
                        .Include(c => c.InstructorsLink)
                        .ThenInclude(l => l.Instructor).ToListAsync();
                var coursesDTO = _mapper.Map<List<Course>, List<CourseDTO>>(courses);
                return coursesDTO;
            }

        }
    }
}

using Application.ErrorsHandler;
using AutoMapper;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class GetOne
    {
        public class Execute : IRequest<CourseDTO>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Execute, CourseDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CourseDTO> Handle(Execute request, CancellationToken cancellationToken)
            {
                //Obtengo el curso con su relacion
                var course = await _context.Course
                        .Include(c => c.CommentList)
                        .Include(c => c.PromoPrice)
                        .Include(c => c.InstructorsLink)
                        .ThenInclude(il => il.Instructor)
                        .FirstOrDefaultAsync(i => i.CourseId == request.Id);

                if (course == null)
                {
                    //throw new Exception("No existe el curso que desea eliminar");
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }

                var courseDTO = _mapper.Map<Course, CourseDTO>(course);
                return courseDTO;
            }
        }
    }
}

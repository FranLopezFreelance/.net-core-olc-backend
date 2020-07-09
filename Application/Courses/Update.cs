using Application.ErrorsHandler;
using DataAccess;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Update
    {
        public class Execute : IRequest
        {
            public Guid CourseId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? PublicationDate { get; set; }
            public List<Guid> InstructorsList { get; set; }
            public decimal? NormalPrice { get; set; }
            public decimal? PromoPrice { get; set; }
        }

        public class Validator : AbstractValidator<Execute>
        {
            public Validator()
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
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.CourseId);
                if (course == null)
                {
                    throw new HandlerExceptions(HttpStatusCode.NotFound, new { message = "No se encontró el curso" });
                }
                //Seteo la data que se quiere modificar
                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublicationDate = request.PublicationDate ?? course.PublicationDate;

                //Edicion del precio
                var price = _context.Price.Where(p => p.CourseId == course.CourseId).FirstOrDefault();
                if (price != null)
                {
                    //Si existe el precio en la base, lo edito
                    price.NormalPrice = request.NormalPrice ?? price.NormalPrice;
                    price.PromoPrice = request.PromoPrice ?? price.PromoPrice;
                }
                else
                {
                    // Si no existía un precio lo agrego a la base
                    price = new Price
                    {
                        PriceId = Guid.NewGuid(),
                        NormalPrice = request.NormalPrice ?? 0,
                        PromoPrice = request.PromoPrice ?? 0,
                        CourseId = course.CourseId
                    };
                    await _context.Price.AddAsync(price);
                }
                //Logica para editar instructores
                if(request.InstructorsList != null)
                {
                    if(request.InstructorsList.Count > 0)
                    {
                        //Elimino los instructores anteriores
                        var instructorsDB = _context.CourseInstructor.Where(ci => ci.CourseId == request.CourseId).ToList();
                        foreach(var instructorDB in instructorsDB)
                        {
                            _context.CourseInstructor.Remove(instructorDB);
                        }
                        //Inserto los nuevos instructores
                        foreach(var id in request.InstructorsList)
                        {
                            var newInstructor = new CourseInstructor { 
                                CourseId = request.CourseId,
                                InstructorId = id
                            };
                            _context.CourseInstructor.Add(newInstructor);
                        }
                    }
                }

                //Obtengo el resultado de la consulta
                var status = await _context.SaveChangesAsync();
                
                if(status > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Ha ocurrido un error al querer actualizar los datos");
            }
        }
    }
}

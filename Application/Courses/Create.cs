using DataAccess;
using Domain;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class Create
    {
        public class Execute: IRequest
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime PublicationDate { get; set; }
            public List<Guid> InstructorsList { get; set; }
            public decimal NormalPrice { get; set; }
            public decimal PromoPrice { get; set; }
        }

        public class Validator: AbstractValidator<Execute>
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
                this._context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                Guid courseGuid = Guid.NewGuid();
                var course = new Course
                {
                    CourseId = courseGuid,
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate
                };

                _context.Course.Add(course);

                if (request.InstructorsList!=null)
                {

                    foreach(var id in request.InstructorsList)
                    {
                        var courseInstructor = new CourseInstructor
                        {
                            CourseId = courseGuid,
                            InstructorId = id
                        };
                        _context.CourseInstructor.Add(courseInstructor);
                    }
                }

                var price = new Price
                {
                    PriceId = Guid.NewGuid(),
                    CourseId = courseGuid,
                    NormalPrice = request.NormalPrice,
                    PromoPrice = request.PromoPrice
                };

                _context.Price.Add(price);
                
                var status = await _context.SaveChangesAsync();
                //Devueve la cantidad de operaciones exitosas que se realizaron en la DB
                if (status > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("Ocurrió un error al intentar guardar los datos");
            }
        }
    }
}

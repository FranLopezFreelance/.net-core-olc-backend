using Application.Courses;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {   
            //Mapeo los cursos
            CreateMap<Course, CourseDTO>()
                //Mapeo los instructores del curso
                .ForMember(c => c.Instructors, i => i.MapFrom(ci => ci.InstructorsLink.Select(ci => ci.Instructor).ToList()))
                //Mapeo los comentarios del curso
                .ForMember(c => c.Comments, c => c.MapFrom(c => c.CommentList))
                //Mapeo el precio del curso
                .ForMember(c => c.Price, p => p.MapFrom(p => p.PromoPrice));
            //Mapeo la tabla Curso/instructor
            CreateMap<CourseInstructor, CourseInstructorDTO>();
            //Mapeo los intructores
            CreateMap<Instructor, InstructorDTO>();
            //Mapeo los precios
            CreateMap<Price, PriceDTO>();
            //Mapeo los comentarios
            CreateMap<Comment, CommentDTO>();
        }
    }
}

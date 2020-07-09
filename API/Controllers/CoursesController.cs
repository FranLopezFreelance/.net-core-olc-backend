using Application.Courses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //https://localhost:44364/api/courses
    public class CoursesController : ApiBaseController
    {

        [HttpGet]
        public async Task<ActionResult<List<CourseDTO>>> GetAll()
        {
            return await Mediator.Send(new GetAll.Execute());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> Detail(Guid id)
        {
            return await Mediator.Send(new GetOne.Execute { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid Id, Update.Execute data)
        {
            data.CourseId = Id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Execute { Id = id });
        }
    }
}

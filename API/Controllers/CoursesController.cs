using Application.Courses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        //https://localhost:44364/api/courses
        public async Task<ActionResult<List<Course>>> GetAll()
        {
            return await _mediator.Send(new GetAll.Execute());
        }

        [HttpGet("{id}")]
        //https://localhost:44364/api/courses/3
        public async Task<ActionResult<Course>> Detail(int id)
        {
            return await _mediator.Send(new GetOne.Execute { Id = id });
        }

        [HttpPost]
        //https://localhost:44364/api/courses/
        public async Task<ActionResult<Unit>> Create(Create.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int Id, Update.Execute data)
        {
            data.CourseId = Id;
            return await _mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id)
        {
            return await _mediator.Send(new Delete.Execute { Id = id });
        }
    }
}

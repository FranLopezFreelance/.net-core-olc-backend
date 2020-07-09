using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Instructors;
using DataAccess.DapperConnection.Instructor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //https://localhost:44364/api/instructors
    public class InstructorsController: ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<InstructorModel>> GetAll()
        {
            var instructors = await Mediator.Send(new GetAll.Execute());
            return instructors.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Update.Execute data)
        {
            data.InstructorId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Execute { InstructorId = id });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Instructors;
using DataAccess.DapperConnection.Instructor;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class InstructorController: ApiBaseController
    {
        [HttpGet]
        public async Task<IEnumerable<InstructorModel>> GetAll()
        {
            var instructors = await Mediator.Send(new GetAll.Execute());
            return instructors.ToList();
        }
    }
}

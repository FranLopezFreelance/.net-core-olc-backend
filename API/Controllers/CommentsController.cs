using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CommentsController: ApiBaseController
    {
        //https://localhost:44364/api/comments
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Execute data)
        {
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Execute { CommentId = id });
        }
    }
}

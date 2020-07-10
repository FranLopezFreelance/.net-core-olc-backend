using DataAccess.DapperConnection.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Courses
{
    public class CoursesPagination
    {
        public class Execute: IRequest<PaginationModel>
        {
            public string Title { get; set; }
            public int Page { get; set; }
            public int Quantity { get; set; }
        }

        public class Handler : IRequestHandler<Execute, PaginationModel>
        {
            private readonly IPagination _pagination;

            public Handler(IPagination pagination)
            {
                _pagination = pagination;
            }

            public async Task<PaginationModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var sp = "usp_Get_Course_Pagination";
                var order = "Title";
                var param = new Dictionary<string, object>();
                var page = request.Page;
                var quantity = request.Quantity;
                param.Add("Title", request.Title);
                return await _pagination.GetPagination(sp, page, quantity, param, order);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DapperConnection.Pagination
{
    public class PaginationModel
    {
        public List<IDictionary<string, object>> Items { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}

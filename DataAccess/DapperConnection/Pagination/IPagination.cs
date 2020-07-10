using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperConnection.Pagination
{
    public interface IPagination
    {
        Task<PaginationModel> GetPagination(string sp, int page, 
            int quantity, IDictionary<string, object> param, string order); 
    }
}

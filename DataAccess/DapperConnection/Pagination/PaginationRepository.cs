using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.DapperConnection.Pagination
{
    public class PaginationRepository : IPagination
    {
        private readonly IFactoryConnection _connection;
        public PaginationRepository(IFactoryConnection connection)
        {
            _connection = connection;
        }

        public async Task<PaginationModel> GetPagination(string sp, int page, int quantity, IDictionary<string, object> parameters, string order)
        {

            PaginationModel pagination = new PaginationModel();
            int totalItems = 0;
            int totalPages = 0;

            try
            {
                var connection = _connection.GetConnection();

                DynamicParameters dynParameters = new DynamicParameters();

                foreach(var param in parameters)
                {
                    dynParameters.Add("@" + param.Key, param.Value);
                }
                
                dynParameters.Add("@Page", page);
                dynParameters.Add("@Quantity", quantity);
                dynParameters.Add("@Order", order);
                dynParameters.Add("@TotalItems", totalItems, DbType.Int32, 
                    ParameterDirection.Output);
                dynParameters.Add("@TotalPages", totalPages, DbType.Int32,
                    ParameterDirection.Output);

                var result = await connection.QueryAsync(sp, dynParameters , commandType: CommandType.StoredProcedure);
                pagination.Items = result.Select(i => 
                    (IDictionary<string, object>) i).ToList();
                pagination.TotalPages = dynParameters.Get<int>("@TotalPages");
                pagination.TotalItems = dynParameters.Get<int>("@TotalItems");
            }catch (Exception e)
            {
                throw new Exception("No se pudo generar la paginación", e);
            }finally
            {
                _connection.CloseConnection();
            }

            return pagination;
        }
    }
}

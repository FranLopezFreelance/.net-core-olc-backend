using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperConnection.Instructor
{
    public class InstructorRepository : IInstructor
    {
        private readonly IFactoryConnection _connection;

        public InstructorRepository(IFactoryConnection connection)
        {
            _connection = connection;
        }

        public Task<int> Create(InstructorModel data)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<InstructorModel> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> GetAll()
        {
            IEnumerable<InstructorModel> instructorsList = null;
            var storeProcedure = "usp_GetAll_Instructors";
            try
            {
                var connection = _connection.GetConnection();
                instructorsList = await connection.QueryAsync<InstructorModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch(Exception e)
            {
                throw new Exception("Error en la consulta de datos. ", e);
            }
            finally
            {
                _connection.CloseConnection();
            }

            return instructorsList;
        }

        public Task<int> Update(InstructorModel data)
        {
            throw new NotImplementedException();
        }
    }
}

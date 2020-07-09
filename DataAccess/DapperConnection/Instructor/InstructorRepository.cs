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

        public async Task<int> Create(string name, string lastName)
        {
            var storedPrecedure = "usp_New_Instructor";
            try
            {
                var connection = _connection.GetConnection();
                var status = await connection.ExecuteAsync(storedPrecedure, new { 
                    InstructorId = Guid.NewGuid(),
                    Name = name,
                    LastName = lastName
                }, commandType: CommandType.StoredProcedure);

                _connection.CloseConnection();

                return status;
            }
            catch(Exception e)
            {
                throw (new Exception("No se pudo crear el instructor", e));
            }
        }

        public async Task<int> Delete(Guid Id)
        {
            var storedPrecedure = "usp_Delete_Instructors";

            try
            {
                var connection = _connection.GetConnection();
                var status = await connection.ExecuteAsync(storedPrecedure, new
                {
                    InstructorId = Id
                }, commandType: CommandType.StoredProcedure);
                _connection.CloseConnection();
                return status;
            }
            catch(Exception e)
            {
                throw (new Exception("No se pudo eliminar el Instructor", e));
            }
        }

        public Task<InstructorModel> Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> GetAll()
        {
            IEnumerable<InstructorModel> instructorsList = null;
            var storedPrecedure = "usp_GetAll_Instructors";
            try
            {
                var connection = _connection.GetConnection();
                instructorsList = await connection.QueryAsync<InstructorModel>(storedPrecedure, null, commandType: CommandType.StoredProcedure);
            }catch(Exception e)
            {
                throw new Exception("Error en la consulta de datos. ", e);
            }finally
            {
                _connection.CloseConnection();
            }

            return instructorsList;
        }

        public async Task<int> Update(Guid instructorId, string name, string lastName)
        {
            var storedProcedure = "usp_Update_Instructor";

            try
            {
                var connection = _connection.GetConnection();
                var status = await connection.ExecuteAsync(storedProcedure, new
                {
                    InstructorId = instructorId,
                    Name = name,
                    LastName = lastName
                }, commandType: CommandType.StoredProcedure);
                _connection.CloseConnection();
                return status;
            }
            catch(Exception e)
            {
                throw(new Exception("No se pudo editar el Instructor", e));
            }
        }
    }
}

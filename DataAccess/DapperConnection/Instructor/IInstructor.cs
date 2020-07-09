using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperConnection.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> GetAll();
        Task<InstructorModel> Get(Guid Id);
        Task<int> Create(InstructorModel data);
        Task<int> Update(InstructorModel data);
        Task<int> Delete(Guid Id);
    }
}

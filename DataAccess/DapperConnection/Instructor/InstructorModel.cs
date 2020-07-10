using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DapperConnection.Instructor
{
    public class InstructorModel
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Courses
{
    public class InstructorDTO
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] ProfileImg { get; set; }
    }
}

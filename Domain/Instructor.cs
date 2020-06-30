using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Instructor
    {
        public Guid InstructorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte[] ProfileImg { get; set; }
        public ICollection<CourseInstructor> CourseLink { get; set; }
    }
}

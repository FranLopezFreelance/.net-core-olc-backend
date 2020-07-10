using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Courses
{
    public class CourseDTO
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public byte[] Image { get; set; }
        public ICollection<InstructorDTO> Instructors { get; set; }
        public PriceDTO Price { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
    }
}

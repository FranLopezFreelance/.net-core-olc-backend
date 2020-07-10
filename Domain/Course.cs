using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] Image { get; set; }
        public Price PromoPrice { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<Comment> CommentList { get; set; }
        public ICollection<CourseInstructor> InstructorsLink { get; set; }
    }
}

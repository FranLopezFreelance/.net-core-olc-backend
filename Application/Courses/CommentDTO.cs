using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Courses
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}

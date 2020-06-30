using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string STudent { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}

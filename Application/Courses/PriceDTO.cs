using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Courses
{
    public class PriceDTO
    {
        public Guid PriceId { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal PromoPrice { get; set; }
        public Guid CourseId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Price
    {
        public Guid PriceId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal NormalPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PromoPrice { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}

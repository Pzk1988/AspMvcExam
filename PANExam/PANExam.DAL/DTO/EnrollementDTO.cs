using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.DTO
{
    class EnrollementDTO
    {
        public Guid Id  { get; set; }

        public StudentDTO Student { get; set; }

        public CourseDTO Course { get; set; }

        public int Grade { get; set; }
    }
}

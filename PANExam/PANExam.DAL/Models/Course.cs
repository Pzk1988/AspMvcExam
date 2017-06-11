using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.Models
{
    public class Course : EntityBase
    {
        public Course()
        {
        }

        public Course(Guid id) : base(id)
        {
        }

        public Course(string id) : base(id)
        {
        }

        public string Title { get; set; }

        public int Ects { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

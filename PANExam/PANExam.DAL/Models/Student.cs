using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.Models
{
    public class Student : EntityBase
    {
        public Student()
        {
        }

        public Student(Guid id) : base(id)
        {
        }

        public Student(string id) : base(id)
        {
        }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}

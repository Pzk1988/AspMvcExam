using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.Models
{
    public class Department : EntityBase
    {
        public Department()
        {
        }

        public Department(Guid id) : base(id)
        {
        }

        public Department(string id) : base(id)
        {
        }
       
        public string Name { get; set; }
        
        [Column(TypeName = "image")]
        public byte[] Emblem { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}

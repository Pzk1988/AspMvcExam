using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.Models
{
    public class Enrollment : EntityBase
    {
        public Enrollment()
        {
        }

        public Enrollment(Guid id) : base(id)
        {
        }

        public Enrollment(string id) : base(id)
        {
        }

        public int Grade { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PANExam.DAL.Models
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public EntityBase(Guid id)
        {
            Id = id;
        }

        public EntityBase(string id)
        {
            Id = Guid.Parse(id);
        }

        public Guid Id { get; set; }
    }
}

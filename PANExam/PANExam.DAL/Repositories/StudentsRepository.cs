using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    internal class StudentsRepository : RepositoryBase<Student>
    {
        public StudentsRepository(UniversityDbContext context) : base(context)
        {
        }

        public IEnumerable<Student> GetStudentsByDepartmentId(Guid id)
        {
            return Context.Students.Where(s => s.Department.Id == id);
            
        }

        public IEnumerable<Student> Get(Guid[] ids)
        {
            return Context.Students.Where(s => ids.Contains(s.Id));
        }
    }
}
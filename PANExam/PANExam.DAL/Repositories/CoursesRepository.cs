using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    internal class CoursesRepository : RepositoryBase<Course>
    {
        public CoursesRepository(UniversityDbContext context) : base(context)
        {
        }

        public IEnumerable<Course> GetCoursesByDepartmentId(Guid id)
        {
            return Context.Set<Course>().Where(c => c.Department.Id == id);
        }
    }
}

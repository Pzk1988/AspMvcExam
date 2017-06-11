using System;
using System.Collections.Generic;
using System.Linq;
using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    public class EnrollementsRepository : RepositoryBase<Enrollment>
    {
        public EnrollementsRepository(UniversityDbContext context) : base(context)
        {
        }

        public IEnumerable<Enrollment> GetCourseEnrollments(Guid id)
        {
            return Context.Enrollments.Where(e => e.Course.Id == id);
        }

        public void DeleteAllCourseEnrollment(Guid id)
        {
            var enrollments = Context.Enrollments.Where(e => e.Course.Id == id);
            Context.Enrollments.RemoveRange(enrollments);
        }

        public void DeleteAllStudentEnrollment(Guid id)
        {
            var enrollments = Context.Enrollments.Where(e => e.Student.Id == id);
            Context.Enrollments.RemoveRange(enrollments);
        }
    }
}
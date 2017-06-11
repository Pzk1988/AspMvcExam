using PANExam.DAL.Models;

namespace PANExam.DAL.Repositories
{
    internal class DepartmentsRepository : RepositoryBase<Department>
    {
        public DepartmentsRepository(UniversityDbContext context) : base(context)
        {
        }
    }
}
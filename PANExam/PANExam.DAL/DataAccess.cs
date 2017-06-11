using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PANExam.DAL.DTO;
using PANExam.DAL.Models;
using PANExam.DAL.Repositories;

namespace PANExam.DAL
{
    public class DataAccess : IDisposable
    {
        private readonly CoursesRepository _coursesRepository;
        private readonly DepartmentsRepository _departmentsRepository;
        private readonly StudentsRepository _studentsRepository;
        private readonly EnrollementsRepository _enrollemntsRepository;
        private readonly UniversityDbContext _context;
        private static readonly IMapper _mapper;
        static DataAccess()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<Course, CourseDTO>().ReverseMap();
                cfg.CreateMap<Student, StudentDTO>().ReverseMap();
                cfg.CreateMap<Enrollment, EnrollementDTO>().ReverseMap();
            }));

        }

        public DataAccess()
        {
            _context = new UniversityDbContext();
            _coursesRepository = new CoursesRepository(_context);
            _departmentsRepository = new DepartmentsRepository(_context);
            _studentsRepository = new StudentsRepository(_context);
            _enrollemntsRepository = new EnrollementsRepository(_context);
        }

        #region Courses
        public List<CourseDTO> GetAllCourses()
        {
            var dbCourses = _coursesRepository.GetAll();
            List<CourseDTO> returnValues = _mapper.Map<List<CourseDTO>>(dbCourses);

            return returnValues;
        }

        public CourseDTO GetCourse(Guid id)
        {
            var dbCourse = _coursesRepository.Get(id);
            return _mapper.Map<CourseDTO>(dbCourse);
        }

        public int GetNumberOfCoursesInDepartment(Guid id)
        {
            var dbCourses = _coursesRepository.GetCoursesByDepartmentId(id);
            return dbCourses.Count();
        }

        public List<CourseDTO> GetCoursesByDepartment(Guid id)
        {
            var dbCourses = _coursesRepository.GetCoursesByDepartmentId(id);
            return _mapper.Map<List<CourseDTO>>(dbCourses);
        }

        public int GetNumberOfStudentsInCourse(Guid id)
        {
            var dbCourse = _coursesRepository.Get(id);
            return dbCourse.Enrollments.Count;
        }

        public void AddNewCourse(CourseDTO courseDto)
        {
            Course dbCourse = _mapper.Map<Course>(courseDto);

            if (dbCourse.Id == Guid.Empty)
            {
                dbCourse.Id = Guid.NewGuid();
            }

            _coursesRepository.Save(dbCourse);
            _context.SaveChanges();
        }

        public void UpdateDepartment(DepartmentDTO departmentDto)
        {
            Department dbDepartment = _mapper.Map<Department>(departmentDto);
            _departmentsRepository.Save(dbDepartment);
            _context.SaveChanges();
        }

        public void DeleteCourse(Guid id)
        {
            _enrollemntsRepository.DeleteAllCourseEnrollment(id);
            _departmentsRepository.Delete(id);
            _context.SaveChanges();
        }

        #endregion Courses

        #region Departments
        public List<DepartmentDTO> GetAllDepartments()
        {
            var dbDepartments = _departmentsRepository.GetAll();
            List<DepartmentDTO> returnValues = _mapper.Map<List<DepartmentDTO>>(dbDepartments);

            return returnValues;
        }

        public DepartmentDTO GetDepartment(Guid id)
        {
            var dbDepartment = _departmentsRepository.Get(id);
            return _mapper.Map<DepartmentDTO>(dbDepartment);
        }

        public int GetNumberOfStudentsInDepartment(Guid id)
        {
            var dbStudents = _studentsRepository.GetStudentsByDepartmentId(id);
            return dbStudents.Count();
        }

        public void AddNewDeprtment(DepartmentDTO departmentDto)
        {
            Department dbDepartment = _mapper.Map<Department>(departmentDto);

            if (dbDepartment.Id == Guid.Empty)
            {
                dbDepartment.Id = Guid.NewGuid();
            }

            _departmentsRepository.Save(dbDepartment);
            _context.SaveChanges();
        }

        public void UpdateCourse(DepartmentDTO departmentDto)
        {
            Department dbDepartment = _mapper.Map<Department>(departmentDto);
            _departmentsRepository.Save(dbDepartment);
            _context.SaveChanges();
        }

        public void DeleteDepartment(Guid id)
        {
            var courses = _coursesRepository.GetCoursesByDepartmentId(id);

            foreach (Course course in courses)
            {
                _enrollemntsRepository.DeleteAllCourseEnrollment(id);
                _coursesRepository.Delete(course);
            }
            
            _departmentsRepository.Delete(id);
            _context.SaveChanges();
        }

        #endregion Departments

        #region Students

        public List<StudentDTO> GetAllStudents()
        {
            var dbStudents = _studentsRepository.GetAll();
            List<StudentDTO> returnValues = _mapper.Map<List<StudentDTO>>(dbStudents);

            return returnValues;
        }

        public StudentDTO GetStudent(Guid id)
        {
            var dbStudent = _studentsRepository.Get(id);
            return _mapper.Map<StudentDTO>(dbStudent);
        }

        public List<StudentDTO> GetStudentsByDepartment(Guid id)
        {
            var dbStudents = _studentsRepository.GetStudentsByDepartmentId(id);
            return _mapper.Map<List<StudentDTO>>(dbStudents);
        }

        public List<StudentDTO> GetStudentsByCourse(Guid id)
        {
            var dbEnrollments = _enrollemntsRepository.GetCourseEnrollments(id);
            Guid[] ids = dbEnrollments.Select(e => e.Course.Id).ToArray();

            var dbStudents = _studentsRepository.Get(ids);
            return _mapper.Map<List<StudentDTO>>(dbStudents);
        }

        public void AddNewStudent(StudentDTO studentDto)
        {
            Student dbStudent = _mapper.Map<Student>(studentDto);
            dbStudent.Department = _departmentsRepository.GetAll().First();

            if (dbStudent.Id == Guid.Empty)
            {
                dbStudent.Id = Guid.NewGuid();
            }

            _studentsRepository.Save(dbStudent);
            _context.SaveChanges();
        }

        public void UpdateStudent(StudentDTO studentDto)
        {
            Student dbStudent = _mapper.Map<Student>(studentDto);
            _studentsRepository.Save(dbStudent);
            _context.SaveChanges();
        }

        public void DeleteStudent(Guid id)
        {
            _enrollemntsRepository.DeleteAllStudentEnrollment(id);
            _studentsRepository.Delete(id);
            _context.SaveChanges();
        }

        public int GetStudentEnrollmentsAmount(Guid id)
        {
            //int i = 0;
            //var student = _studentsRepository.Get(id);
            //var enrolement = _enrollemntsRepository.GetAll();

            //foreach (var item in enrolement)
            //{
            //    if(item.Student.Id == student.Id)
            //    {
            //        i++;
            //    }
            //}
            return 2;// _enrollemntsRepository.GetAll().Where(o => o.Student.Id == id).Count();
        }

        #endregion Students

        #region Enrollments
        #endregion Enrollments
        public void Dispose()
        {
            _context?.Dispose();
        }


    }
}

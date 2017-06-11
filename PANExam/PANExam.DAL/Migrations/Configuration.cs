using System.Collections.Generic;
using PANExam.DAL.Models;

namespace PANExam.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PANExam.DAL.Models.UniversityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PANExam.DAL.Models.UniversityDbContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander" },
                new Student { FirstName = "Meredith", LastName = "Alonso"},
                new Student { FirstName = "Arturo",   LastName = "Anand"},
                new Student { FirstName = "Gytis",    LastName = "Barzdukas"},
                new Student { FirstName = "Yan",      LastName = "Li"},
                new Student { FirstName = "Peggy",    LastName = "Justice"},
                new Student { FirstName = "Laura",    LastName = "Norman"},
                new Student { FirstName = "Nino",     LastName = "Olivetto"}
            };


            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();



            var departments = new List<Department>
            {
                new Department { Name = "English", Students = new List<Student>(){students[0], students[1], students[2]}},
                new Department { Name = "Mathematics", Students = new List<Student>(){students[3]}},
                new Department { Name = "Engineering", Students = new List<Student>(){students[4], students[5]} },
                new Department { Name = "Economics" , Students = new List<Student>(){students[6], students[7]}}
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {Title = "Chemistry",      Ects = 3, Department = departments[0]},
                new Course {Title = "Microeconomics", Ects = 3, Department = departments[1]},
                new Course {Title = "Macroeconomics", Ects = 3, Department = departments[2]},
                new Course {Title = "Calculus",       Ects = 4, Department = departments[3]},
                new Course {Title = "Trigonometry",   Ects = 4, Department = departments[0]},
                new Course {Title = "Composition",    Ects = 3, Department = departments[1]},
                new Course {Title = "Literature",     Ects = 4, Department = departments[2]},
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            
            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alexander"),
                    Course = courses.Single(c => c.Title == "Chemistry" ),
                    Grade = 5
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alexander"),
                    Course = courses.Single(c => c.Title == "Microeconomics" ),
                    Grade = 3
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alexander"),
                    Course = courses.Single(c => c.Title == "Macroeconomics" ),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alonso"),
                    Course = courses.Single(c => c.Title == "Calculus" ),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alonso"),
                    Course = courses.Single(c => c.Title == "Trigonometry" ),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Alonso"),
                    Course = courses.Single(c => c.Title == "Composition" ),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Anand"),
                    Course = courses.Single(c => c.Title == "Chemistry" )
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Anand"),
                    Course = courses.Single(c => c.Title == "Microeconomics"),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Barzdukas"),
                    Course = courses.Single(c => c.Title == "Chemistry"),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Li"),
                    Course = courses.Single(c => c.Title == "Composition"),
                    Grade = 4
                },
                new Enrollment {
                    Student = students.Single(s => s.LastName == "Justice"),
                    Course = courses.Single(c => c.Title == "Literature"),
                    Grade = 4
                }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.SingleOrDefault(s => s.Student.Id == e.Student.Id &&
                        s.Course.Id == e.Course.Id);
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}

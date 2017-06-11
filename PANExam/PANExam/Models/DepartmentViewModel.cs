using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PANExam.Models
{
    public class DepartmentViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[] Emblem { get; set; }
        public int NumberOfCourses { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEvidence.Shared
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime AdmissionDate { get; set; } = DateTime.Now;
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}

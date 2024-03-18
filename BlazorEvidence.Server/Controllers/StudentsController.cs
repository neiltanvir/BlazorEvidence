using BlazorEvidence.Server.Data;
using BlazorEvidence.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorEvidence.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StudentsController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return await _db.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _db.Students.Include(x => x.Courses).FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) { return NotFound(); }
            return student;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(Student student)
        {
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return student.Id;
        }
        [HttpPut]
        public async Task<ActionResult> Put(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
            foreach (var course in student.Courses)
            {
                if (course.Id != 0)
                {
                    _db.Entry(course).State = EntityState.Modified;
                }
                else
                {
                    _db.Entry(course).State = EntityState.Added;
                }
            }
            var idsOfCourses = student.Courses.Select(x => x.Id).ToList();
            var courseToDelete = await _db.Courses.Where(x => !idsOfCourses.Contains(x.Id) && x.StudentId == student.Id).ToListAsync();
            _db.RemoveRange(courseToDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var stuudent = await _db.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.Id == id);
            if (stuudent == null) { return NotFound(); }
            _db.Courses.RemoveRange(stuudent.Courses);
            _db.Students.Remove(stuudent);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiStudents.Data;
using ApiStudents.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStudents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create Endpoints
        [HttpGet]
        public IActionResult ListAllStudents()
        {
            var students = _context.Students.ToList<Student>();

            return new JsonResult(students);
        }

        [HttpGet]
        [Route("bylastnameoremail")]

        public IActionResult ListStudents(string LastName, string Email)
        {
            var students = _context.Students.Where
            (
                s=> s.LastName == LastName || s.Email == Email 
                
            )
            .ToList<Student>();

            return new JsonResult(students);
        }

        [HttpGet]
        [Route("byDoB")]

        public IActionResult ListStudents(string Dob)
        {
            var students = _context.Students.Where
            (
                s => s.DoB == Dob

            )
            .ToList<Student>();

            return new JsonResult(students);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Student(Student id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudents(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Student), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, Student Student)
        {
            if (id != Student.Id)
            {
                return BadRequest();
            }

            _context.Entry(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Student(int id)
        {
            var todoItem = await _context.Students.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Students.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw4.Data;
using cw4.Models;

namespace cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsControllerGenerated : ControllerBase
    {
        private readonly cw4Context _context;

        public StudentsControllerGenerated(cw4Context context)
        {
            _context = context;
        }

        // GET: api/StudentsControllerGenerated
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }

        // GET: api/StudentsControllerGenerated/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/StudentsControllerGenerated/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string IndexNumber, Student student)
        {
            if (IndexNumber != student.IndexNumber)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(IndexNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentsControllerGenerated
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { IndexNumber = student.IndexNumber }, student);
        }

        // DELETE: api/StudentsControllerGenerated/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(string IndexNumber)
        {
            var student = await _context.Student.FindAsync(IndexNumber);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(string IndexNumber)
        {
            return _context.Student.Any(e => e.IndexNumber == IndexNumber);
        }
    }
}

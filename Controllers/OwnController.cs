using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw4.Data;
using cw4.Models;
using cw4.DTOs;

namespace cw4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnController : ControllerBase
    {
        private readonly cw4Context _context;

        public OwnController(cw4Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }


        [HttpPut("{IndexNumber}")]
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


        [HttpDelete("{IndexNumber}")]
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


        [HttpPut("enrollStudent")]
        public async Task<IActionResult> EnrollStudent(EnrollmentRequest enrollmentRequest)
        {
            if (enrollmentRequest is null)
            {
                return BadRequest();
            }
            var studies = _context.Studies.Where(i => i.Name == enrollmentRequest.Studies).First();
            var enrollment = _context.Enrollment.Where(i => i.IdStudy == studies.IdStudy && i.Semester == 1).DefaultIfEmpty().First();
            if(enrollment is null)
            {
                enrollment = new Enrollment();
                enrollment.IdStudy = studies.IdStudy;
                enrollment.Semester = 1;
                enrollment.StartDate = DateTime.Now;
                enrollment.IdEnrollment = _context.Enrollment.Max(i => i.IdEnrollment) +1;
                _context.Enrollment.Add(enrollment);
            }
            var student = new Student(enrollmentRequest.FirstName, enrollmentRequest.LastName);
            student.BirthDate = Convert.ToDateTime(enrollmentRequest.BirthDate);
            student.idEnrollment = enrollment.IdEnrollment;
            student.IndexNumber = enrollmentRequest.IndexNumber;
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }

        [HttpGet("promoteStudent/{IndexNumber}")]
        public async Task<IActionResult> promoteStudent(string IndexNumber)
        {
            if (!StudentExists(IndexNumber))
                return BadRequest();

            var student = _context.Student.Where( i => i.IndexNumber == IndexNumber).First();
            var enrollment = _context.Enrollment.Where(i => i.IdEnrollment == student.idEnrollment).First();
            var newEnrollment = _context.Enrollment.Where(i => i.IdStudy == enrollment.IdStudy && i.Semester == enrollment.Semester + 1).DefaultIfEmpty().First();
            if(newEnrollment is null)
            {
                newEnrollment = new Enrollment();
                newEnrollment.IdStudy = enrollment.IdStudy;
                newEnrollment.Semester = enrollment.Semester + 1;
                newEnrollment.StartDate = DateTime.Now;
                newEnrollment.IdEnrollment = _context.Enrollment.Max(i => i.IdEnrollment) + 1;
                _context.Enrollment.Add(newEnrollment);
                student.idEnrollment = newEnrollment.IdEnrollment;
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}

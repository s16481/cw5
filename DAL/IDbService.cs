using System.Collections;
using System.Collections.Generic;
using cw2.Models;

namespace cw2.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetEnrollment(string indexNumber);
    }
}
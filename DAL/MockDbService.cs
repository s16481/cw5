using System.Collections;
using System.Collections.Generic;
using cw2.Models;

namespace cw2.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student(1, "Jan", "Kowalski"),
                new Student(2, "Anna", "Malewski"),
                new Student(3, "Andrzej", "Andrzejewicz")
            };
        }
        
        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
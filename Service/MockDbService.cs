using System.Collections;
using System.Collections.Generic;
using cw4.DTOs;
using cw4.Models;

namespace cw4.Service
{
    public class MockDbService : IStudentsDbService
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

        public IEnumerable<Enrollment> GetEnrollmentByIndexNumber(string indexNumber)
        {
            throw new System.NotImplementedException();
        }

        public Studies GetStudies(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool AddStudent(Student student, Enrollment enrollment)
        {
            throw new System.NotImplementedException();
        }

        public Enrollment EnrollStudent(EnrollmentRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
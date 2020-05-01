using System.Collections;
using System.Collections.Generic;
using cw4.DTOs;
using cw4.Models;

namespace cw4.Service
{
    public interface IStudentsDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetEnrollmentByIndexNumber(string indexNumber);
        public Studies GetStudies(string name);
        public bool AddStudent(Student student, Enrollment enrollment);
        public Enrollment EnrollStudent(EnrollmentRequest request);
    }
}
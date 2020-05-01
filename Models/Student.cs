using System;

namespace cw4.Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        
        public DateTime BirthDate { get; set; }

        public Student(int idStudent, string firstName, string lastName)
        {
            this.IdStudent = idStudent;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Student(){
        }
    }
}
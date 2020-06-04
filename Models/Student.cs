using System;
using System.ComponentModel.DataAnnotations;

namespace cw4.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string IndexNumber { get; set; }
        
        public DateTime BirthDate { get; set; }

        public int idEnrollment { get; set; }

        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Student(){
        }
    }
}
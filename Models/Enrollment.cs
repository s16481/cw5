using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw4.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public Enrollment(int idEnrollment, int semester, int idStudy, DateTime startDate)
        {
            IdEnrollment = idEnrollment;
            Semester = semester;
            IdStudy = idStudy;
            StartDate = startDate;
        }

        public Enrollment()
        {
            
        }
    }
}
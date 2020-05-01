using System;

namespace cw4.Models
{
    public class Enrollment
    {
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
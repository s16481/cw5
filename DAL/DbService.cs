using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cw2.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace cw2.DAL
{
    public class DbService : IDbService
    {
        public IEnumerable<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            String db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select * from Student;";
                connection.Open();
                var dataRow = command.ExecuteReader();
                while (dataRow.Read())
                {
                    var student = new Student();
                    student.FirstName = dataRow["FirstName"].ToString();
                    student.LastName = dataRow["LastName"].ToString();
                    student.IndexNumber = dataRow["IndexNumber"].ToString();
                    students.Add(student);
                }
            }

            return students;
        }
        
        public IEnumerable<Enrollment> GetEnrollment(string indexNumber)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            String db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                //command.CommandText = String.Format("select E.IdEnrollment, Semester, IdStudy, StartDate from Student join Enrollment E on Student.IdEnrollment = E.IdEnrollment where IndexNumber='{0}'", indexNumber);
                command.CommandText = "select E.IdEnrollment, Semester, IdStudy, StartDate from Student join Enrollment E on Student.IdEnrollment = E.IdEnrollment where IndexNumber=@indexNumber";
                command.Parameters.AddWithValue("indexNumber", indexNumber);
                connection.Open();
                var dataRow = command.ExecuteReader();
                while (dataRow.Read())
                {
                    var enrollment = new Enrollment();
                    enrollment.IdEnrollment = IntegerType.FromString(dataRow["IdEnrollment"].ToString());
                    enrollment.Semester = IntegerType.FromString(dataRow["Semester"].ToString());
                    enrollment.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                    enrollment.StartDate = Convert.ToDateTime(dataRow["StartDate"].ToString());
                    enrollments.Add(enrollment);
                }
            }

            return enrollments;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cw4.DTOs;
using cw4.Models;
using Microsoft.VisualBasic.CompilerServices;

namespace cw4.Service
{
    public class DbService : IStudentsDbService
    {
        String db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True; MultipleActiveResultSets=True";

        public DbService()
        {
        }
        
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
        
        public Studies GetStudies(string name)
        {
            String db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select * from Studies WHERE Name like @name";
                command.Parameters.AddWithValue("name", name);
                connection.Open();
                var dataRow = command.ExecuteReader();
                if (dataRow.Read()) {
                    Studies studies = new Studies();
                    studies.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                    studies.Name = dataRow["Name"].ToString();
                    return studies;
                }
                return null;
            }
        }
        
        private Studies GetStudies(string name, SqlCommand command)
        {
            command.CommandText = "select * from Studies WHERE Name like @name";
            command.Parameters.AddWithValue("name", name);
            var dataRow = command.ExecuteReader();
            if (dataRow.Read()) {
                Studies studies = new Studies();
                studies.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                studies.Name = dataRow["Name"].ToString();
                dataRow.Close();
                return studies;
            }
            dataRow.Close();
            throw new NotFoundException();
        }
        
        public IEnumerable<Enrollment> GetEnrollmentByIndexNumber(string indexNumber)
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
        
        /*public Enrollment GetEnrollmentByIdStudy(int idStudy)
        {
            String db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select E.IdEnrollment, Semester, E.IdStudy, StartDate from Studies join Enrollment E on Studies.IdStudy = E.IdStudy where Semester=1";
                connection.Open();
                var dataRow = command.ExecuteReader();
                if(dataRow.Read())
                {
                    var enrollment = new Enrollment();
                    enrollment.IdEnrollment = IntegerType.FromString(dataRow["IdEnrollment"].ToString());
                    enrollment.Semester = IntegerType.FromString(dataRow["Semester"].ToString());
                    enrollment.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                    enrollment.StartDate = Convert.ToDateTime(dataRow["StartDate"].ToString());
                    return enrollment;
                }
                command.CommandText = "insert into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES ((SELECT MAX(IdEnrollment) FROM Enrollment) + 1, 1, @idStudy, CURRENT_TIMESTAMP); ";
                command.Parameters.AddWithValue("idStudy", idStudy);
                command.ExecuteNonQuery();
                return GetEnrollmentByIdStudy(idStudy);
            }
        }
        
        public Enrollment GetEnrollmentByIdStudy(int idStudy, SqlCommand command)
        {
                command.CommandText = "select E.IdEnrollment, Semester, E.IdStudy, StartDate from Studies join Enrollment E on Studies.IdStudy = E.IdStudy where Semester=1";
                var dataRow = command.ExecuteReader();
                if(dataRow.Read())
                {
                    var enrollment = new Enrollment();
                    enrollment.IdEnrollment = IntegerType.FromString(dataRow["IdEnrollment"].ToString());
                    enrollment.Semester = IntegerType.FromString(dataRow["Semester"].ToString());
                    enrollment.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                    enrollment.StartDate = Convert.ToDateTime(dataRow["StartDate"].ToString());
                    return enrollment;
                }
                command.CommandText = "insert into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES ((SELECT MAX(IdEnrollment) FROM Enrollment) + 1, 1, @idStudy, CURRENT_TIMESTAMP); ";
                command.Parameters.AddWithValue("idStudy", idStudy);
                command.ExecuteNonQuery();
                return GetEnrollmentByIdStudy(idStudy);
        }*/
        
        public bool AddStudent(Student student, Enrollment enrollment)
        {
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                command.CommandText = "insert into Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@indexNumber, @firstName, @lastName, @birthDate, @idEnrollment); ";
                command.Parameters.AddWithValue("indexNumber", student.IndexNumber);
                command.Parameters.AddWithValue("firstName", student.FirstName);
                command.Parameters.AddWithValue("lastName", student.LastName);
                command.Parameters.AddWithValue("birthDate", student.BirthDate);
                command.Parameters.AddWithValue("idEnrollment", enrollment.IdEnrollment);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        /*public bool EnrollStudent(Student student, Enrollment enrollment)
        {
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    command.Transaction = connection.BeginTransaction();
                    Enrollment enroll = GetOrAddEnrollmentToTransaction(command, student, enrollment);
                    AddStudentToTransaction(command, student, enroll);
                    command.Transaction.Commit();
                    return true;
                }
                catch (SqlException e)
                {
                    command.Transaction.Rollback();
                    return false;
                }
            }
        }*/
        
        public Enrollment EnrollStudent(EnrollmentRequest request)
        {
            using (SqlConnection connection = new SqlConnection(db))
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                command.Transaction = connection.BeginTransaction();
                try
                {
                    var studies = GetStudies(request.Studies, command);
                    Enrollment enroll = GetOrAddEnrollmentToTransaction(command, studies);
                    AddStudent(command, request, enroll);
                    command.Transaction.Commit();
                    return enroll;
                }
                catch (SqlException e)
                {
                    command.Transaction.Rollback();
                    throw e;
                }
            }
        }

        private void AddStudent(SqlCommand command, EnrollmentRequest request, Enrollment enrollment)
        {
            command.CommandText = "insert into Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES (@indexNumber, @firstName, @lastName, @birthDate, @idEnrollment); ";
            command.Parameters.AddWithValue("indexNumber", request.IndexNumber);
            command.Parameters.AddWithValue("firstName", request.FirstName);
            command.Parameters.AddWithValue("lastName", request.LastName);
            command.Parameters.AddWithValue("birthDate", request.BirthDate);
            command.Parameters.AddWithValue("idEnrollment", enrollment.IdEnrollment);
            command.ExecuteNonQuery();
        }

        /*private Enrollment GetOrAddEnrollmentToTransaction(SqlCommand command, Student student, Enrollment enrollment)
        {
            command.CommandText = "select E.IdEnrollment, Semester, E.IdStudy, StartDate from Studies join Enrollment E on Studies.IdStudy = E.IdStudy where Semester=1";
            var dataRow = command.ExecuteReader();
            if(dataRow.Read())
            {
                enrollment.IdEnrollment = IntegerType.FromString(dataRow["IdEnrollment"].ToString());
                enrollment.Semester = IntegerType.FromString(dataRow["Semester"].ToString());
                enrollment.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                enrollment.StartDate = Convert.ToDateTime(dataRow["StartDate"].ToString());
                return enrollment;
            }
            command.CommandText = "insert into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES ((SELECT MAX(IdEnrollment) FROM Enrollment) + 1, 1, @idStudy, CURRENT_TIMESTAMP); ";
            command.Parameters.AddWithValue("idStudy", enrollment.IdStudy);
            command.ExecuteNonQuery();
            return GetEnrollmentByIdStudy(enrollment.IdStudy);
        }*/
        
        private Enrollment GetOrAddEnrollmentToTransaction(SqlCommand command, Studies studies)
        {
            command.Parameters.Clear();
            command.CommandText = "select IdEnrollment, Semester, IdStudy, StartDate from Enrollment where Semester=1 AND IdStudy = @IdStudy";
            command.Parameters.Add("@IdStudy", SqlDbType.Int);
            command.Parameters["@IdStudy"].Value = studies.IdStudy;
            using (var dataRow = command.ExecuteReader())
            {
                if(dataRow.Read())
                    {
                        var enrollment = new Enrollment();
                        enrollment.IdEnrollment = IntegerType.FromString(dataRow["IdEnrollment"].ToString());
                        enrollment.Semester = IntegerType.FromString(dataRow["Semester"].ToString());
                        enrollment.IdStudy = IntegerType.FromString(dataRow["IdStudy"].ToString());
                        enrollment.StartDate = Convert.ToDateTime(dataRow["StartDate"].ToString());
                        return enrollment;
                    }
            }
            command.CommandText = "insert into Enrollment (IdEnrollment, Semester, IdStudy, StartDate) VALUES ((SELECT MAX(IdEnrollment) FROM Enrollment) + 1, 1, @idStudy, CURRENT_TIMESTAMP); ";
            command.ExecuteNonQuery();
            return GetOrAddEnrollmentToTransaction(command, studies);
        }
    }

    internal class NotFoundException : Exception
    {
    }
}
using System;
using System.Data.SqlClient;
using cw2.DAL;
using cw2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        private string db = "Data Source=db-mssql;Initial Catalog=s16481;Integrated Security=True";
        
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        
        /*[HttpGet]
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie = {orderBy}";
        }*/
        
        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}"), HttpDelete("{id}"), HttpPut("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (Request.Method == HttpMethods.Get)
            {
                if (id == 1)
                         {
                             return Ok("Kowalski");
                         }
                         else if (id == 2)
                         {
                             return Ok("Malewski");
                         }
            }

            if (Request.Method == HttpMethods.Put)
            {
                return Ok("Aktualizacja dokończona");
            } else if (Request.Method == HttpMethods.Delete)
            {
                return Ok("Usuwanie ukończone");
            }

            return NotFound("Nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
    }
}
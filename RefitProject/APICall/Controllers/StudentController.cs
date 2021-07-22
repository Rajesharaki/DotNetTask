using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefitProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        List<Student> liststu = new List<Student>()
        {
            new Student()
            {
                            id = 1, Name = "abc", Branch = "CS"

            },
             new Student()
            {
                            id = 2, Name = "def", Branch = "ME"

            },
              new Student()
            {
                            id = 3, Name = "ghi", Branch = "EC"

            },
        };

        [HttpGet("GetAllStudents")]
        public async Task <IActionResult> Get()
        {
            var result=await Task.FromResult(liststu);
            return Ok(result);
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task <IActionResult> Get(int id)
        {
            var student=await Task.FromResult(liststu.Find(x => x.id == id));
            return Ok(student);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Post(Student student)
        {
            liststu.Add(student);
            var result = await Task.FromResult(liststu);
            return Ok(result);
        }
    }
}

using DapperProhect.Interface;
using DapperProhect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepositary _repositary;
        public StudentController(IStudentRepositary repositary)
        {
            _repositary = repositary;
        }

        [HttpGet,Route("GetAllStudents")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Student> students = await _repositary.GetAllStudents();
                List<Student> s = await _repositary.GetAllStudentswithCompression();
                var s1 = await _repositary.GetAllStudentswithoutCompression();
                return Ok(new { students,s,s1 });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet,Route("GetStudentById")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _repositary.GetStudentById(id);
            return Ok(student);
        }
    }
}

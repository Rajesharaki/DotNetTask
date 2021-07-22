using APICall.Interface;
using DapperProhect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefitProject.Interface;
using RefitProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IStudentRepositary _repositary;

        public ValuesController(IStudentRepositary repositary)
        {
            this._repositary = repositary;
        }
        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> Get()
        {
            var result = await _repositary.GetAllStudents();
            return Ok(result);
        }
        [HttpGet("GetstudentById/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            return Ok(await _repositary.GetStudentById(id));
        }
        [HttpPost("InsertStudent")]
        public async Task<IActionResult> Insert(Student student)
        {
            return Ok(await _repositary.Insert(student));
        }

        
    }
}

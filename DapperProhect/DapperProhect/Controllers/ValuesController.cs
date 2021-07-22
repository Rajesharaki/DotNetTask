using DapperProhect.Interface;
using DapperProhect.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValue value;

        public ValuesController(IValue value)
        {
            this.value = value;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var result=value.GetAllStudents();

            return Ok(result);
        }
    }
}

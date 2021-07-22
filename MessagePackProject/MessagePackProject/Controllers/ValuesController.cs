using MessagePack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagePackProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet,Route("Get")]
        public ActionResult<Student> Get()
        {
            Student result = new Student()
            {
                id = 1,
                Name = "abc"
            };

            //var result=MessagePackSerializer.Serialize(s);
            return Ok(result);
        }
    }
}

using FluentValidation;
using FluentValidationProject.Models;
using FluentValidationProject.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpPost,Route("InsertCustomer")]
        public IActionResult Post([FromBody]NewCustomer newCustomer)
        {
            //var valid=new CustomerValidator().Validate(newCustomer);
            //if (!valid.IsValid)
            //{
            //    List<String> errormsg = new List<string>();
            //    foreach(var err in valid.Errors)
            //    {
            //        errormsg.Add("Error Message: "+err.ErrorMessage);
            //    }
            //    return BadRequest(errormsg);
            //}
            return Ok();
        }
    }
}

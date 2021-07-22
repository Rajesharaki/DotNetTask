using Microsoft.AspNetCore.Mvc;
using Refit;
using RefitProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICall.Interface
{
    public interface IStudentRepositary
    {
        [Get("/api/Student/GetAllStudents")]
        Task<List<Student>> GetAllStudents();

        [Get("/api/Student/GetStudentById/{id}")]
        Task<Student> GetStudentById(int id);

        [Post("/api/Student/Insert")]
        Task<List<Student>> Insert(Student student);
    }
}

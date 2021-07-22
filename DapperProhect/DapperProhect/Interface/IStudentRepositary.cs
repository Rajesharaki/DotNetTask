using DapperProhect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Interface
{
    public interface IStudentRepositary
    {
        Task<List<Student>> GetAllStudents();
        Task<List<Student>> GetStudentById(int id);
        Task<List<Student>> GetAllStudentswithCompression();
        Task<List<Student>> GetAllStudentswithoutCompression();
    }
}

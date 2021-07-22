using DapperProhect.Interface;
using DapperProhect.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Manager
{
    public class IValueimpl : IValue
    {
        private readonly IConfiguration configuration;

        public IValueimpl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Student GetAllStudents()
        {
            var cs = configuration["ConnectionStrings:DBCS"];

            var conn = new SqlConnection(cs);
            conn.Open();

            var cmd = new SqlCommand("Sp_GetAllStu", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            var reader=cmd.ExecuteReader();
            Student stu = null;
            while (reader.Read())
            {
                stu = new Student();
                stu.StudentId = (int)reader[0];
                stu.Name = (string)reader[1];
            }
            conn.Close();
            return stu;
        }
    }
}

using Dapper;
using DapperProhect.Interface;
using DapperProhect.Models;
using MessagePack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperProhect.Manager
{
    public class StudentRepositary : IStudentRepositary
    {
        public static readonly MessagePackSerializerOptions LZ4option =
            MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

        private IConfiguration _configuration;

        private static int objectCreated = 0;

        public StudentRepositary(IDistributedCache distributedCache,IConfiguration configuration)
        {
            objectCreated ++;

            this._distributedCache = distributedCache;
            _configuration = configuration;
        }


        private string cs = "Server=(localdb)\\MSSQLLocalDB;Database=Rajesh;Trusted_Connection=True";

        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration configuration;

        public async  Task<List<Student>> GetAllStudents()
        {
            string word=_configuration.GetConnectionString("DBCS");

            //Get Section
            var section = _configuration.GetSection("Student:Name");

            var key=section.Key;
            var value = section.Value;

            string abc = _configuration.GetValue<string>("Student:Name");

            using (IDbConnection con = new SqlConnection(cs))
            {
                var result = await con.QueryAsync<Student>("sp_GetAllStudent", commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }





        //public async Task<List<Student>> GetAllStudents()
        //{
        //    var cacheKey = "GetAllStudents";
        //    //var cacheStudentList = await _distributedCache.GetAsync(cacheKey);
        //    if (true/*cacheStudentList == null*/)
        //    {
        //        using (IDbConnection con = new SqlConnection(cs))
        //        {
        //            //dapper
        //            var gridReader = await con.QueryMultipleAsync("sp_GetAllStudent", commandType: CommandType.StoredProcedure);

        //            var result=await gridReader.ReadAsync<Student>();
        //            var result1= await gridReader.ReadAsync<Student>();

        //            var serializedObject = JsonConvert.SerializeObject(result);
        //            var byteobject = Encoding.UTF8.GetBytes(serializedObject);
        //            //var options = new DistributedCacheEntryOptions()
        //            //             .SetAbsoluteExpiration(DateTime.Now.AddSeconds(120));
        //            await _distributedCache.SetAsync(cacheKey, byteobject);
        //            return result.ToList();
        //        }
        //    }
        //    var serializedCustomer = "a";//Encoding.UTF8.GetString(cacheStudentList);
        //    return JsonConvert.DeserializeObject<List<Student>>(serializedCustomer);
        //}

        //With LZ4 compression
        public async Task<List<Student>> GetAllStudentswithCompression()
        {
            var cacheKey = "LZ4option";
            var cacheStudentList = await _distributedCache.GetAsync(cacheKey);
            if (cacheStudentList == null)
            {
                using (IDbConnection con = new SqlConnection(cs))
                {
                    //dapper
                    var result = await con.QueryAsync<Student>("sp_GetAllStudent", commandType: CommandType.StoredProcedure);

                    byte[] serializedObject = MessagePackSerializer.Serialize(result.ToList(), LZ4option);
                    //var options = new DistributedCacheEntryOptions()
                    //             .SetAbsoluteExpiration(DateTime.Now.AddSeconds(120));
                    await _distributedCache.SetAsync(cacheKey, serializedObject);
                    return result.ToList();
                }
            }
            return MessagePackSerializer.Deserialize<List<Student>>(cacheStudentList, LZ4option);
        }

        //without Lz4
        public async Task<List<Student>> GetAllStudentswithoutCompression()
        {
            var cacheKey = "withoutLZ4";
            var cacheStudentList = await _distributedCache.GetAsync(cacheKey);
            if (cacheStudentList == null)
            {
                using (IDbConnection con = new SqlConnection(cs))
                {
                    //dapper
                    var result = await con.QueryAsync<Student>("sp_GetAllStudent", commandType: CommandType.StoredProcedure);

                    byte[] serializedObject = MessagePackSerializer.Serialize(result.ToList());
                    //var options = new DistributedCacheEntryOptions()
                    //             .SetAbsoluteExpiration(DateTime.Now.AddSeconds(120));
                    await _distributedCache.SetAsync(cacheKey, serializedObject);
                    return result.ToList();
                }
            }
            return MessagePackSerializer.Deserialize<List<Student>>(cacheStudentList);
        }
        public async Task<List<Student>> GetStudentById(int id)
        {
            //using (IDbConnection con = new SqlConnection(cs))
            //{
            //    var student = await con.QueryAsync<Student>("Sp_GetStudentById", new { studentid = id }, commandType: CommandType.StoredProcedure);
            //    return student.ToList();
            //}
          return  new List<Student>();
        }

    }
}

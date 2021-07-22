using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program
    {
        private string cs = "Server=(localdb)\\MSSQLLocalDB;Database=dapper;Trusted_Connection=True";
        static void Main(string[] args)
        {
            string cs = "Server=(localdb)\\MSSQLLocalDB;Database=Rajesh;Trusted_Connection=True";

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("salary", typeof(float));
            dataTable.Columns.Add("Branch", typeof(string));


            dataTable.Rows.Add("Rajesha", 1225.20,"Mech");

            var conn = new SqlConnection(cs);
            var bulkCopy = new SqlBulkCopy(conn)
            {
                DestinationTableName = "tbl_rajesh",
            };
            bulkCopy.ColumnMappings.Add("Name", "Name");
            bulkCopy.ColumnMappings.Add("salary", "salary");
            bulkCopy.ColumnMappings.Add("Branch", "Branch");

            conn.Open();
            bulkCopy.WriteToServer(dataTable);
            conn.Close();
            Console.WriteLine("return ok................responsse!");
            Console.ReadKey();
        }
    }
}


//        //private static void SetStudentInfo(Need need,Student student)
//        //{
//        //    need.id = student.id;
//        //    need.name = student.name;
//        //}
//        //private static void SetEmpInfo(Need need,Employee emp)
//        //{
//        //    need.slary = emp.slary;
//        ////}
////        List<Student> students = new List<Student>()
////            {
////                new Student(){id=1,name="rajesha"},
////                new Student(){id=3,name="Madan"},
////            }.Distinct().ToList();

////        var studentDict = students.ToDictionary(x => new { x.id, x.name }, x => x);

////        var emp = new List<Employee>()
////        {
////            //new Employee(){id=1,name="rajesha",slary=2.55},
////        }.Distinct().ToList();

////        var empDict = emp.ToDictionary(x => new { x.id, x.name }, x => x);

////        List<ScripId> scripIds = new List<ScripId>();

////            foreach(var stu in emp)
////            {
////                scripIds.Add(new ScripId { id = stu.id, name = stu.name
////    });
////            }
////foreach (var stu in students)
////{
////    scripIds.Add(new ScripId { id = stu.id, name = stu.name });
////}
////var final = scripIds.Select(x => new { x.id, x.name }).Distinct().ToList();

////List<Need> needs = new List<Need>();
////foreach (var fin in final)
////{
////    Need need = new Need();

////    if (studentDict.TryGetValue(fin, out var student))
////        SetStudentInfo(need, student);
////    if (empDict.TryGetValue(fin, out var employee))
////        SetEmpInfo(need, employee);

////    needs.Add(need);
////}
//    }
//}

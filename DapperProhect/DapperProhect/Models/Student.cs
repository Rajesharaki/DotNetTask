using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Models
{
    [MessagePackObject]
    public class Student
    {
        [MessagePack.Key(0)]
        public int StudentId { get; set; }
        [MessagePack.Key(1)]
        public string Name { get; set; }
        [MessagePack.Key(2)]
        public string Contact { get; set; }
    }
}

using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagePackProject
{
    //[MessagePackObject]
    public class Student
    {
        //[Key(0)]
        public int id { get; set; }
        //[Key(1)]
        public string Name { get; set; }
    }
}

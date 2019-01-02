using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApp.Entities
{
    class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Class StudentClass { get; set; }

    }
}

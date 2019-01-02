using System.Collections.Generic;

namespace AttendanceApp.Entities
{
    class Class
    {
        
        public int ClassId { get; private set; }
        public int Year { get; set; }
        public int Section { get; set; }
        public int Semester { get; set; }

        public List<Course> Courses { get; set; }

        public Class(int classId)
        {
            ClassId = classId;
        }

    }
}

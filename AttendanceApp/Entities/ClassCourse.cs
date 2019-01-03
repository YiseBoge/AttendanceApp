using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceApp.Entities
{
    public class ClassCourse
    {
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }
        public Course Course { get; set; }

        public ClassCourse(int classCourseId, Class aClass, Course aCourse)
        {
            this.ClassCourseId = classCourseId;
            this.Class = aClass;
            this.Course = aCourse;
        }


        public override string ToString()
        {

            string returnable = string.Format($"{Course.CourseName}, Year {Class.Year}, Semester {Class.Semester}, {Class.Department}, Section {Class.Section}");
            return base.ToString();
        }
    }


    public class Class
    {

        public int ClassId { get; private set; }
        public int Year { get; set; }
        public string Department { get; set; }
        public int Section { get; set; }
        public int Semester { get; set; }

        public Class(int classId, int year, int semester, string department, int section)
        {
            ClassId = classId;
        }

    }

    public class Course
    {

        public int CourseId { get; }
        public string CourseName { get; set; }

        public Course(int courseId, string courseCourseName)
        {
            this.CourseId = courseId;
            this.CourseName = courseCourseName;
        }

        public override string ToString() => this.CourseName;
    }
}

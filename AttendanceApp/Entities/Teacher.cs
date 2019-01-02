using System.Collections.Generic;

namespace AttendanceApp.Entities
{
    class Teacher : User
    {

        public Class UserClass { get; set; }
        public Course UserCourse { get; set; }

        public Teacher(int userId, string email, string password, Class userClass, Course userCourse) : base(userId, email, password)
        {
            this.UserClass = userClass;
            this.UserCourse = userCourse;
        }

        public Teacher(int userId, string email, string password) : base(userId, email, password)
        {
            this.UserClass = null;
            this.UserCourse = null;
        }

        public void MarkStudent(string userId, string password)
        {

        }

        public List<Student> getAttendingStudents()
        {
            return new List<Student>();
        }

//        public List<Attendance> getAttendanceList()
//        {
//            return new List<Attendance>();
//        }
    }
}
  
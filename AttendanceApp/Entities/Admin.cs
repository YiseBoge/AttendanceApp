using System.Collections.Generic;

namespace AttendanceApp.Entities
{
    class Admin :User
    {
        public Admin(int userId, string name, string email, string password) : base(userId, name, email, password)
        {
        }

        public bool AddCourse(string courseName)
        {
            return false;
        }

        public bool AddClass(int year, int semester, string department, int section, List<Course> courses)
        {
            return false;
        }

        public bool AddStudent(string studentId, string name, string password, Class theClass)
        {
            return false;
        }

        public bool MakeAdmin(Teacher aTeacher)
        {
            return false;
        }
    }
}

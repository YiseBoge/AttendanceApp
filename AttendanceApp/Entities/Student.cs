

namespace AttendanceApp.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Class StudentClass { get; set; }

        public Student(string studentId, string name, string password, Class theClass)
        {
            this.StudentId = studentId;
            this.Name = name;
            this.Password = password;
            this.StudentClass = theClass;
        }
        
    }
}

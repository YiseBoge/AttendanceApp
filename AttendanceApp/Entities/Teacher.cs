using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttendanceApp.DataManagement;

namespace AttendanceApp.Entities
{
    public class Teacher : User
    {
        private DatabaseManager DatabaseManager { get; set; }

        public Class UserClass { get; set; }
        public Course UserCourse { get; set; }
        public List<ClassCourse> ClassCourses { get; set; }

        public Teacher(int userId, string name, string email, string password, List<ClassCourse> classCourses) : base(userId, name, email, password)
        {
            DatabaseManager = new DatabaseManager();
            this.ClassCourses = classCourses;
        }

        public string MarkStudent(string studentId, string password, List<Student> allStudents, List<Student> todayStudents)
        {
            foreach (Student student in allStudents)
            {
                if (student.StudentId.Equals(studentId, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (student.Password.Equals(password))
                    {
                        if (todayStudents.Contains(student))
                        {
                            return "You have Already Checked In.";
                        }
                        todayStudents.Add(student);
                        return "Welcome to class, "+ student.Name;
                    }

                    return "passwordmissmatch";
                }
                
                
               
            }

            return "notfound";
        }
  

        public bool SaveTodayAttendance(List<Student> todayStudents, ClassCourse classCourse)
        {
            bool result = false;
            foreach (Student student in todayStudents)
            {

                result = DatabaseManager.SaveStudentAttendance(student.StudentId, classCourse.ClassCourseId);

            }

            todayStudents.Clear();
            return result;

        }


        public List<Student> GetFullClass(Class aClass)
        {
            Console.WriteLine(aClass.ClassId);
            List<Student> list = DatabaseManager.GetAllStudentsInClass(aClass);
            return list;
        }

    }
}

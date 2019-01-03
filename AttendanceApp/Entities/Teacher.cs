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
            this.ClassCourses = classCourses;
        }

        public string MarkStudent(string studentId, string password, List<Student> allStudents, List<Student> todayStudents)
        {
            foreach (Student student in allStudents)
            {
                if (student.StudentId.Equals(studentId))
                {
                    if (student.Password.Equals(password))
                    {
                        if (todayStudents.Contains(student))
                        {
                            return "alreadyexisting";
                        }
                        else
                        {
                            todayStudents.Add(student);
                            return "success";
                        }
                    }

                    return "passwordmissmatch";
                }
                
                
               
            }

            return "notfound";
        }

        public string MarkStudent(string studentId, List<Student> allStudents, List<Student> todayStudents)
        {
            foreach (Student student in allStudents)
            {
                if (student.StudentId.Equals(studentId))
                {
                    
                        if (todayStudents.Contains(student))
                        {
                            return "alreadyexisting";
                        }
                        else
                        {
                            todayStudents.Add(student);
                            return "success";
                        }
                   
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

            if (result)
            {
                return true;
            }

            return false;

        }


        public List<Student> GetTodayStudents()
        {
            return new List<Student>();
        }

        //        public List<Attendance> getAttendanceList()
        //        {
        //            return new List<Attendance>();
        //        }
    }
}

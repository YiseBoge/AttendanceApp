

using System;
using static System.Console;
using System.Collections.Generic;
using System.Data;
using AttendanceApp.Entities;
using MySql.Data.MySqlClient;

namespace AttendanceApp.DataManagement
{
    public partial class DatabaseManager
    {
        private string DataSource { get; }
        private string Port { get; }
        private string Username { get; }
        private string Password { get; }
        private string Database { get; }
        private string ConnectionString { get; }

        public static MySqlConnection TheConnection { get; private set; }

        public DatabaseManager()
        {
            DataSource = "localhost";
            Port = "3306";
            Username = "attendance_user";
            Password = "@endance_password";
            Database = "attendance_database";

            ConnectionString =
                string.Format($"datasource={DataSource};" +
                              $"port={Port};" +
                              $"username={Username};" +
                              $"password={Password};" +
                              $"database={Database}");
            StartConnection();
        }



        private void StartConnection()
        {

            try
            {
                TheConnection = new MySqlConnection(ConnectionString);
                TheConnection.Open();
                if (TheConnection.State == ConnectionState.Open)
                {
                    WriteLine("It is Connected Now");
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }

            TheConnection.Close();
        }



        public User GetUser(int id)
        {

            List<User> usersList = new List<User>();
            MySqlCommand theCommand = TheConnection.CreateCommand();
            string commandText = "select * from users where id=@id";


            theCommand.CommandText = commandText;
            theCommand.Parameters.AddWithValue("id", id);


            TheConnection.Open();

            MySqlDataReader reader = theCommand.ExecuteReader();

            while (reader.Read())
            {
                int userId = (int)reader["user_id"];
                string name = (string)reader["name"];
                string email = (string)reader["email"];
                string password = (string)reader["password"];
                string role = (string)reader["role"];


                User user;
                if (role.ToUpper().Equals("ADMIN"))
                {
                    user = new Admin(userId, name, email, password);
                }
                else
                {
                    user = new Teacher(userId, name, email, password, GetClassCoursesForUser(userId));
                }


                usersList.Add(user);
            }

            TheConnection.Close();
            return usersList[0];
        }
        
        public List<User> GetAllUsers()
        {
            List<User> usersList = new List<User>();
            MySqlCommand theCommand = TheConnection.CreateCommand();
            string commandText = "select * from users";


            theCommand.CommandText = commandText;

            TheConnection.Open();
            

            MySqlDataReader reader = theCommand.ExecuteReader();

            while (reader.Read())
            {
                int userId = (int)reader["user_id"];
                string name = (string) reader["name"];
                string email = (string)reader["email"];
                string password = (string)reader["password"];
                string role = (string)reader["role"];


                User user;
                if (role.ToUpper().Equals("ADMIN"))
                {
                    user = new Admin(userId, name, email, password);
                }
                else
                {
                    user = new Teacher(userId, name, email, password, GetClassCoursesForUser(userId));
                }


                usersList.Add(user);
            }

           
            TheConnection.Close();
            foreach (var VARIABLE in COLLECTION)
            {
                
            }
            return usersList;
        }

            public List<ClassCourse> GetClassCoursesForUser(int theTeacherId)
        {
            List<ClassCourse> classCoursesList = new List<ClassCourse>();
            MySqlCommand theCommand = TheConnection.CreateCommand();
            string commandText = "select * from class_courses where teacher_id=@teacherId";

            theCommand.CommandText = commandText;
            theCommand.Parameters.AddWithValue("teacherId", theTeacherId);

            TheConnection.Open();

            MySqlDataReader reader = theCommand.ExecuteReader();

            while (reader.Read())
            {
                int classCourseId = (int)reader["class_course_id"];
                int classId = (int)reader["class_id"];
                int courseId = (int)reader["coiurse_id"];

                ClassCourse theClassCourse = new ClassCourse(classCourseId, GetClass(classId),GetCourse(courseId));
                
                classCoursesList.Add(theClassCourse);
            }

            TheConnection.Close();
            return classCoursesList;
        }
        
                public Class GetClass(int id)
                {
                    List<Class> usersList = new List<Class>();
                    MySqlCommand theCommand = TheConnection.CreateCommand();
                    string commandText = "select * from classes where class_id=@id";

                    theCommand.CommandText = commandText;
                    theCommand.Parameters.AddWithValue("class_id", id);

                    TheConnection.Open();

                    MySqlDataReader reader = theCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        int classId = (int)reader["class_id"];
                        int year = (int)reader["year"];
                        int semester = (int)reader["semester"];
                        string department = (string)reader["department"];
                        int section = (int)reader["section"];
                        


                        Class theClass = new Class(classId, year, semester, department, section);
                        
                        usersList.Add(theClass);
                    }

                    TheConnection.Close();
                    return usersList[0];
                }

                public Course GetCourse(int id)
                {
                    List<Course> usersList = new List<Course>();
                    MySqlCommand theCommand = TheConnection.CreateCommand();
                    string commandText = "select * from classes where course_id=@id";

                    theCommand.CommandText = commandText;
                    theCommand.Parameters.AddWithValue("course_id", id);

                    TheConnection.Open();

                    MySqlDataReader reader = theCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        int courseId = (int)reader["course_id"];
                        string courseName = (string)reader["course_name"];
                        

                       Course theCourse = new Course(courseId, courseName);

                        usersList.Add(theCourse);
                    }

                    TheConnection.Close();
                    return usersList[0];
                }



        
        public bool SaveStudentAttendance(string studentId, int classCourseId)
        {
            MySqlCommand theCommand = TheConnection.CreateCommand();
            string commandText = "INSERT INTO attendance(`student_id`, `class_course_id`) VALUES(@studentId, @classCourseId)";

            theCommand.CommandText = commandText;

            #region DataAddingRegion

            theCommand.Parameters.AddWithValue("studentId", studentId);
            theCommand.Parameters.AddWithValue("classCourseId", classCourseId);

            TheConnection.Open();

            if (theCommand.ExecuteNonQuery() > 0)
            {
                TheConnection.Close();
                return true;
            }


            #endregion

            TheConnection.Close();
            return false;
        }


    }

}

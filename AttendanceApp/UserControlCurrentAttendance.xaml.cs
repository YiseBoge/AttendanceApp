using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AttendanceApp.Entities;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for UserControlCurrentAttendance.xaml
    /// </summary>
    public partial class UserControlCurrentAttendance : UserControl
    {
        public List<Student> AttendingStudents { get; set; }
        public Teacher TheTeacher { get; set; }

        public UserControlCurrentAttendance(Teacher theTeacher, List<Student> attendingStudents)
        {
            InitializeComponent();

            TheTeacher = theTeacher;
            AttendingStudents = attendingStudents;
            PopulateDataGrid();
        }


        public void PopulateDataGrid()
        {
            StudentsListTable.Items.Clear();
            foreach (Student student in AttendingStudents)
            {
                StudentsListTable.Items.Add(student);
            }
        }

        private void SaveAttendance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Saved Attendance will not be shown on this list.", "Are you sure?", MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                TheTeacher.SaveTodayAttendance(AttendingStudents, HomeWindow.CurrentClassCourse);
                PopulateDataGrid();
            }
            
        }
    }
}

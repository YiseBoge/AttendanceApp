
using System.Collections.Generic;
using System.Windows.Controls;
using AttendanceApp.DataManagement;
using AttendanceApp.Entities;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for UserControlAttendanceList.xaml
    /// </summary>
    public partial class UserControlAttendanceList : UserControl
    {
        private List<Student> AttendingStudent { get; set; }

        public UserControlAttendanceList(List<Student> attendingStudents)
        {
            InitializeComponent();
            this.AttendingStudent = attendingStudents;
        }
    }
}

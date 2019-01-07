using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            if (AttendingStudents == null)
            {
                SetMessageText(Colors.DodgerBlue, "No Unsaved Attendances at the moment.");
            }
            else
            {
                SetMessageText(Colors.DodgerBlue, "");
            }
            ConfirmPasswordText.Text = "Confirm your Password to Save";
        }

        private void SaveAttendance_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
        }

        private void SetMessageText(Color color, string message)
        {
            MessageBlock.Foreground = new SolidColorBrush(color);
            MessageBlock.Text = message;
        }

        private void PasswordConfirmBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordField.Password == TheTeacher.Password)
            {
                TheTeacher.SaveTodayAttendance(AttendingStudents, HomeWindow.CurrentClassCourse);
                PopulateDataGrid();

                SetMessageText(Colors.DodgerBlue, "No Unsaved Attendances at the moment.");
                ConfirmPasswordPopup.IsOpen = false;
                ConfirmPasswordText.Text = "Confirm your Password to Save";
            }
            else
            {
                ConfirmPasswordText.Text = "Incorrect Password";
            }

            ConfirmPasswordField.Clear();

        }
    }
}

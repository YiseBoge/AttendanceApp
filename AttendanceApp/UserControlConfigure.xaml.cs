using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AttendanceApp.Entities;
using AttendanceApp.TeacherViews;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for UserControlConfigure.xaml
    /// </summary>
    public partial class UserControlConfigure : UserControl
    {
        public Teacher TheTeacher { get; set; }

        public UserControlConfigure(Teacher theTeacher)
        {
            InitializeComponent();
            this.TheTeacher = theTeacher;
            
            SetupFields();
            SetMessageText(Colors.OrangeRed, "Current Course: Not Assigned");
        }

        private void SetupFields()
        {
            EditNameField.Text = TheTeacher.Name;
            EditEmailField.Text = TheTeacher.Email;

            foreach (ClassCourse theTeacherClassCourse in TheTeacher.ClassCourses)
            {
                EditCourseField.Items.Add(theTeacherClassCourse.ToString());
            }
        }

        private void EditAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EditCourseField.SelectedIndex >= 0)
            {
                HomeWindow.CurrentClassCourse = TheTeacher.ClassCourses[EditCourseField.SelectedIndex];
                SetMessageText(Colors.LimeGreen, "Current Course: " + HomeWindow.CurrentClassCourse);
            }
            
        }

        private void SetMessageText(Color color, string message)
        {
            MessageBlock.Foreground = new SolidColorBrush(color);
            MessageBlock.Text = message;
        }
    }
}

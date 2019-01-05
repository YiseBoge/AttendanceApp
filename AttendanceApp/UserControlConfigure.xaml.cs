using System.Windows;
using System.Windows.Controls;
using AttendanceApp.Entities;


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
            HomeWindow.CurrentClassCourse = TheTeacher.ClassCourses[EditCourseField.SelectedIndex];
            TextBlock.Text = HomeWindow.CurrentClassCourse.ToString();
        }
    }
}

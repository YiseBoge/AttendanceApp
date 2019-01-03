using System.Collections.Generic;
using System.Windows;
using AttendanceApp.DataManagement;
using AttendanceApp.Entities;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private DatabaseManager DatabaseManager { get; set; }
        private List<User> AllUsers { get; set; }
        private User LoggingInUser { get; set; }

        private string CurrentUsername = "abebe";
        private string CurrentUserEmail = "abebe@abebe.com";
        private string CurrentPassword = "password";
        public LoginWindow()
        {
            DatabaseManager = new DatabaseManager();
            AllUsers = DatabaseManager.GetAllUsers();

            InitializeComponent();
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckCredentials())
            {
                if (LoggingInUser is Teacher)
                {
                    new HomeWindow((Teacher)LoggingInUser).Show();
                }
                
                Close();
            }

        }

        private bool CheckCredentials()
        {
            string givenEmail = EmailField.Text.Trim();
            string givenPassword = PasswordField.Password.Trim();
            foreach (User user in AllUsers)
            {
                if (user.CheckEmail(givenEmail))
                {
                    if (user.CheckPassword(givenPassword))
                    {
                        LoggingInUser = user;
                        return true;
                    }
                        
                }
            }
            return false;
        }

    }
}

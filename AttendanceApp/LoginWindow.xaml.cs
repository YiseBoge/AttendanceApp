using System.Collections.Generic;
using System.Windows;
using AttendanceApp.Entities;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        List<User> CurentUsersList = new List<User>();

        private string CurrentUsername = "abebe";
        private string CurrentUserEmail = "abebe@abebe.com";
        private string CurrentPassword = "password";
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (checkCredentials())
            {
                new HomeWindow(CurrentUsername).Show();
                Close();
            }

        }

        private bool checkCredentials()
        {
            if (EmailField.Text.Trim() == CurrentUserEmail)
            {
                if (PasswordField.Password.Trim() == CurrentPassword)
                {
                    return true;
                }

                return false;
            }
            return false;
        }
    }
}

using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using AttendanceApp.DataManagement;
using AttendanceApp.Entities;
using AttendanceApp.TeacherViews;

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
                if (LoggingInUser is Teacher teacher)
                {
                    new HomeWindow(teacher).Show();
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
                    else
                    {
                        ShowPopup("Wrong Password for the specified email.");
                        return false;
                    }
                        
                }
            }
            ShowPopup("Account not Found.");
            return false;
        }


        private void EmailField_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Down)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);

                if (Keyboard.FocusedElement is UIElement keyboardFocus)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            }
        }

        private void PasswordField_OnKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);

                if (Keyboard.FocusedElement is UIElement keyboardFocus)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }

                e.Handled = true;
            } 
        }

        private void ShowPopup(string message)
        {
            PopupMessage.Text = message;
            LoginErrorPopup.IsOpen = true;
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using AttendanceApp.Entities;

namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private User LoggedInUser { get; set; }

        private String _currentUser = "";

        public HomeWindow(string currentUsername)
        {
            _currentUser = currentUsername;
            InitializeComponent();

            CurentUserName.Text = string.Format($"Welcome, {currentUsername}");
            GridMain.Children.Add(new UserControlAttendanceList());

        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemCheckIn":
                    usc = new UserControlCheckIn();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemCurrentAttendance":
                    usc = new UserControlCurrentAttendance();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemAttendanceList":
                    usc = new UserControlAttendanceList();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }

        private void ConfigureBtn_OnClick(object sender, RoutedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            usc = new UserControlConfigure();
            GridMain.Children.Add(usc);
        }

        private void LogoutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _currentUser = null;
            new LoginWindow().Show();
            Close();

        }
    }
}

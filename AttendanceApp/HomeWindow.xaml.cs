using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
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
        private Teacher TheTeacher { get; set; }
        public static ClassCourse CurrentClassCourse { get; set; }

        public static List<Student> AttendingStudents { get; set; } = new List<Student>();

        private UserControl TheCheckInUserControl { get; set; }
        private UserControl TheConfigureUserControl { get; set; }
        private UserControl TheCurrentAttendanceUserControl { get; set; }
        private UserControl TheAttendanceListUserControl { get; set; }  

        public HomeWindow(Teacher loggedInTeacher)
        {
            InitializeComponent();
            TheTeacher = loggedInTeacher;
            
            CurentUserName.Text = string.Format($"Welcome, {TheTeacher.Name}");

            LoadUserControl("Configure");

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

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemCheckIn":
                    if (CurrentClassCourse == null)
                    {
                        MessageBox.Show("Please Select a Course for the Current Session.", "No Class Selected.", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadUserControl("Configure");
                        break;
                    }
                    LoadUserControl("CheckIn");
                    break;
                case "ItemCurrentAttendance":
                    LoadUserControl("CurrentAttendance");
                    break;
                case "ItemAttendanceList":
                    LoadUserControl("AttendanceList");
                    break;
            }
        }

        private void LoadUserControl(string which)
        {
            if (TheCheckInUserControl != null)
            {
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    ((UserControlCheckIn)TheCheckInUserControl).LocalWebCam.Stop();

                }));
                
            }
            GridMain.Children.Clear();
            switch (which)
            {
                case "CheckIn":
                    if (this.TheCheckInUserControl == null)
                    {
                        this.TheCheckInUserControl = new UserControlCheckIn(AttendingStudents, TheTeacher, CurrentClassCourse);
                    }
                    GridMain.Children.Add(TheCheckInUserControl);
                    break;
                case "Configure":
                    if (this.TheConfigureUserControl == null)
                    {
                        this.TheConfigureUserControl = new UserControlConfigure(TheTeacher);
                    }
                    GridMain.Children.Add(TheConfigureUserControl);
                    break;
                case "CurrentAttendance":
                    if (this.TheCurrentAttendanceUserControl == null)
                    {
                        this.TheCurrentAttendanceUserControl = new UserControlCurrentAttendance(TheTeacher, AttendingStudents);
                    }

                    ((UserControlCurrentAttendance)TheCurrentAttendanceUserControl).PopulateDataGrid();
                    GridMain.Children.Add(TheCurrentAttendanceUserControl);
                    break;
                case "AttendanceList":
                    if (this.TheAttendanceListUserControl == null)
                    {
                        this.TheAttendanceListUserControl = new UserControlAttendanceList(AttendingStudents);
                    }
                    GridMain.Children.Add(TheAttendanceListUserControl);
                    break;
                default:
                    break;
            }
        }

        private void ConfigureBtn_OnClick(object sender, RoutedEventArgs e)
        {
           LoadUserControl("Configure");
        }

        private void LogoutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TheTeacher = null;
            MessageBoxResult result = MessageBox.Show("Are You sure You wish to Logout?", "Logging Out", MessageBoxButton.YesNo,MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                new LoginWindow().Show();

                Close();
            }
            

        }


        protected override void OnClosed(EventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                ((UserControlCheckIn)TheCheckInUserControl).LocalWebCam.Stop();

            }));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                ((UserControlCheckIn)TheCheckInUserControl).LocalWebCam.Stop();

            }));

        }
    }
}

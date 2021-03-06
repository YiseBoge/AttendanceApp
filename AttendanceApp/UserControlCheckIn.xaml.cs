﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AttendanceApp.Entities;
using AttendanceApp.Services;
using Color = System.Windows.Media.Color;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for UserControlCheckIn.xaml
    /// </summary>
    public partial class UserControlCheckIn : UserControl
    {
        public Teacher TheTeacher { get; set; }
        private ClassCourse CurrentClassCourse { get; set; }
        public List<Student> FullClass { get; set; }

        public List<Student> AttendingStudents { get; set; }
        private QRService QrService { get; set; }
        public VideoCaptureDevice LocalWebCam { get; set; }

        public UserControlCheckIn(List<Student> attendingStudents, Teacher theTeacher, ClassCourse currentClassCourse)
        {
            
            InitializeComponent();

            this.AttendingStudents = attendingStudents;
            this.TheTeacher = theTeacher;
            this.CurrentClassCourse = currentClassCourse;
            this.FullClass = theTeacher.GetFullClass(CurrentClassCourse.Class);




            this.QrService = new QRService();


            //  Code to generate QR code for students in the class as the attendance page opens

//            foreach (Student student in FullClass)
//            {
//                if (QrService.SaveQrImage(QrService.EncodeTextToQr(student.StudentId+"*"+student.Password), student.Name))
//                {
//                    
//                }
//                else
//                {
//                    MessageBox.Show("Not Saved");
//                }
//
//            }


            Loaded += UserControlCheckIn_Loaded;
        }

        

        #region Camera displaying Region

        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            try
            {
                Bitmap img = image;

                MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    LiveView.Source = bi;
                    
                    string theString = QrService.DecodeTextFromQr(image);
                    if (theString != null)
                    {
                        

                        CheckInStudent(theString);

                        LocalWebCam.Start();
                    }

                }));
             
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

        
        void UserControlCheckIn_Loaded(object sender, RoutedEventArgs e)
        {

            LocalWebCam = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[0].MonikerString);
            LocalWebCam.NewFrame += Cam_NewFrame;

            LocalWebCam.Start();
        }

        #endregion


        private void CheckInBtn_Click(object sender, RoutedEventArgs e)
        {
            CheckInStudent(StudentIdField.Text, StudentPasswordField.Password);
            StudentIdField.Clear();
            StudentPasswordField.Clear();
            LocalWebCam.Start();
        }

        private void CheckInStudent(string studentId, string password)
        {
            foreach (Student student in FullClass)
            {
                if (student.StudentId == studentId)
                {
                    if (student.Password == password)
                    {
                        string result = TheTeacher.MarkStudent(studentId, password, FullClass, AttendingStudents);

                        if (!result.Equals("You have Already Checked In."))
                        {
                            LocalWebCam.Stop();

                            ShowPopup(result);
                            SystemSounds.Beep.Play();
                            SetMessageText(Colors.LimeGreen, result);
                            return;
                        }
                        SetMessageText(Colors.DodgerBlue, result+student.Name);

                        return;
                    }
                    SetMessageText(Colors.OrangeRed, "Incorrect Password");
                }

            }
            SetMessageText(Colors.OrangeRed, "No Student of that Id was found in this Class.");
        }

        private void CheckInStudent(string theString)
        {
            string studentId;
            string password;
            try
            {
                studentId = theString.Split('*')[0];
                password = theString.Split('*')[1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            CheckInStudent(studentId,password);
            
        }

        private void ShowPopup(string message)
        {
            PopupMessage.Text = message;
            StudentRecognizedPopup.IsOpen = true;
        }

        private void SetMessageText(Color color, string message)
        {
            MessageBlock.Foreground = new SolidColorBrush(color);
            MessageBlock.Text = message;
        }

        private void StudentIdField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SetMessageText(Colors.OrangeRed, "");

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

        private void StudentPasswordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
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
    }
}

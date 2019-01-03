using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using AttendanceApp.Services;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;


namespace AttendanceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private QRService QrService { get; set; }
        VideoCaptureDevice _localWebCam;


        public MainWindow()
        {
            InitializeComponent();
            QrService = new QRService();

//            if (QrService.SaveQrImage(QrService.EncodeTextToQr("ATR/8083/09"), "Yisehak Bogale"))
//            {
//                TestTextBlock.Text = "Written";
//            }
//            else
//            {
//                TestTextBlock.Text = "Unwritten";
//            }
            
            
            Loaded += MainWindow_Loaded;
        }

        #region Camera displaying Region

        void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap) eventArgs.Frame.Clone();
            try
            {
                Image img = image;

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
                    frameHolder.Source = bi;

                    string theString = QrService.DecodeTextFromQr(image);
                    if (theString!=null)
                    {
                        SystemSounds.Beep.Play();
                        string string1 = string.Format($"Welcome, {theString}");
                        MessageBox.Show(string1, "Check In Message",MessageBoxButton.OK);
                        _localWebCam.Stop();
                        TestTextBlock.Text = theString;
                    }
                    
                }));
            }
            catch (Exception ex)
            {

            }


            
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            _localWebCam = new VideoCaptureDevice(new FilterInfoCollection(FilterCategory.VideoInputDevice)[0].MonikerString);
            _localWebCam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);

            _localWebCam.Start();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_localWebCam.IsRunning)
            {
                _localWebCam.Stop();
                TestTextBlock.Text = "Camera Stopped.";
            }
        }



        #endregion

        private void TheButton_Click(object sender, RoutedEventArgs e)
        {
            if (_localWebCam.IsRunning)
            {
                _localWebCam.Stop();
                TestTextBlock.Text = "Camera Stopped.";
            }
            else
            {
                _localWebCam.Start();
                TestTextBlock.Text = "Camera Started.";

            }
            
        }
    }

}

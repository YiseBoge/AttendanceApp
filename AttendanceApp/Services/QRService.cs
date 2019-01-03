using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AttendanceApp.Entities;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;


namespace AttendanceApp.Services
{
    class QRService
    {

        public Bitmap EncodeTextToQr(string studentId)
        {
            BarcodeWriter writer = new BarcodeWriter
                { Format = BarcodeFormat.QR_CODE };
            var result = writer.Write(studentId);
            var barcodeBitmap = new System.Drawing.Bitmap(result);

            return barcodeBitmap;
        }


        public string DecodeTextFromQr(Bitmap picture)
        {
            BarcodeReader bareCodeReader = new BarcodeReader();

            return bareCodeReader.Decode(picture)?.Text;
        }
        
        public bool SaveQrImage(Bitmap picture, string studentName)
        {
            try
            {
                picture.Save("..\\..\\Assets\\QRImages\\"+studentName+".jpg", ImageFormat.Jpeg);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

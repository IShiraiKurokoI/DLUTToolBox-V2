using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QRCoder;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// QRPayCodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QRPayCodeWindow : Window
    {
        public QRPayCodeWindow(string uri)
        {
            InitializeComponent();
            LoadUrlToQRCode(uri);
        }

        void LoadUrlToQRCode(string url)
        {
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(1);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        System.Drawing.Bitmap bmp = qrCode.GetGraphic(20, System.Drawing.Color.Black, System.Drawing.Color.White,false);
                        IntPtr hBitmap = bmp.GetHbitmap();
                        System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        QRCode.Source = WpfBitmap;
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

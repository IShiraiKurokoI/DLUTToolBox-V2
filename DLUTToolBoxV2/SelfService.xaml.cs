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
using HandyControl.Controls;
using System.Diagnostics;
using System.IO;
using Microsoft.Web.WebView2.Core;
using System.Drawing;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// SelfService.xaml 的交互逻辑
    /// </summary>
    public partial class SelfService :  HandyControl.Controls.Window
    {
        int count = 0;
        public SelfService()
        {
            InitializeComponent();
            SetBackgroundImage();
            if ((this.Width - 480) > 100)
            {
                if (AddressBox.Visibility == Visibility.Hidden)
                {
                    AddressBox.Visibility = Visibility.Visible;
                }
                AddressBox.Width = this.Width - 480;
            }
            else
            {
                AddressBox.Visibility = Visibility.Hidden;
            }
            Web.DefaultBackgroundColor = System.Drawing.Color.Transparent;
        }

        private void SetBackgroundImage()
        {
            if (Properties.Settings.Default.BackgroundImageUrl != "")
                if (File.Exists(Properties.Settings.Default.BackgroundImageUrl))
                {
                    ImageBrush b = new ImageBrush();
                    b.ImageSource = new BitmapImage(new Uri(Properties.Settings.Default.BackgroundImageUrl));
                    if (Properties.Settings.Default.BackGroundStrechMethod == "Fill")
                    {
                        b.Stretch = Stretch.Fill;
                    }
                    else if (Properties.Settings.Default.BackGroundStrechMethod == "Uniform")
                    {
                        b.Stretch = Stretch.Uniform;
                    }
                    else if (Properties.Settings.Default.BackGroundStrechMethod == "UniformToFill")
                    {
                        b.Stretch = Stretch.UniformToFill;
                    }
                    this.Background = b;
                }
        }

        private void Web_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            AddressBox.Text = Web.Source.AbsoluteUri;
            if (Web.Source.AbsoluteUri == "http://172.20.20.1:8800/user/operate/index")
            {
                Web.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('radio')[0].innerHTML='<label><input type=\"radio\" name=\"OperateForm[shiftType]\" value=\"1\"> 立即生效</label><label><input type=\"radio\" name=\"OperateForm[shiftType]\" value=\"2\"> 下个周期生效</label>'");
                Web.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('select')[1].innerHTML+='<option value=\"13\">包月限100G</option>'");
            }
            if (Web.Source.AbsoluteUri == "http://172.20.20.1:8800/home/base/index")
            {
                CAPTCHA.Visibility = Visibility.Hidden;
                CAPTCHABox.Visibility = Visibility.Hidden;
                Web.Height = this.Height;
                BackgroundBlur.Visibility = Visibility.Visible;
                Growl.ClearGlobal();
            }
            if (Web.Source.AbsoluteUri == "http://172.20.20.1:8800/")
            {
                Web.CoreWebView2.ExecuteScriptAsync("document.getElementById('loginform-username').value=" + Properties.Settings.Default.Uid);
                Web.CoreWebView2.ExecuteScriptAsync("document.getElementById('loginform-password').value='" + Properties.Settings.Default.NetworkPassword + "'");
                Web.CoreWebView2.ExecuteScriptAsync("var img=document.getElementById('loginform-verifycode-image')");
                Web.CoreWebView2.ExecuteScriptAsync("var canvas = document.createElement('canvas')");
                Web.CoreWebView2.ExecuteScriptAsync("canvas.width = img.width");
                Web.CoreWebView2.ExecuteScriptAsync("canvas.height = img.height");
                Web.CoreWebView2.ExecuteScriptAsync("var ctx = canvas.getContext('2d')");
                Web.CoreWebView2.ExecuteScriptAsync("ctx.drawImage(img, 0, 0, img.width, img.height)");
                Web.CoreWebView2.ExecuteScriptAsync("var dataURL = canvas.toDataURL('image / png')");
                Web.CoreWebView2.ExecuteScriptAsync("window.chrome.webview.postMessage('cat:'+dataURL)");
            }
            count++;
        }
        async Task login()
        {
            await Web.CoreWebView2.ExecuteScriptAsync("document.getElementById('loginform-verifycode').value='" + CAPTCHABox.Password + "'");
            await Web.CoreWebView2.ExecuteScriptAsync("document.getElementsByName('login-button')[0].click()");
            await Task.Delay(1700);
            await Web.CoreWebView2.ExecuteScriptAsync("window.chrome.webview.postMessage(document.getElementsByClassName('alert alert-danger error-summary')[0].getElementsByTagName('li')[0].innerText)");
        }
        private void Web_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            if (e.WebMessageAsJson.ToString().IndexOf("验证码") != -1)
            {
                Growl.ErrorGlobal("验证码有误，请重新输入！");
                CAPTCHABox.Password = "";
                CAPTCHA.Visibility = Visibility.Visible;
                CAPTCHABox.Visibility = Visibility.Visible;
                CAPTCHABox.Focus();
                return;
            }
            if (e.WebMessageAsJson.ToString().IndexOf("cat:") != -1)
            {
                string data = e.WebMessageAsJson.ToString().Substring(1, e.WebMessageAsJson.ToString().Length - 2);
                ToImage(data);
                CAPTCHABox.Focus();
                return;
            }
            else
            {
                Web.Height = this.Height;
                BackgroundBlur.Visibility = Visibility.Visible;
                Growl.ClearGlobal();
            }
            Growl.ErrorGlobal("⚠登陆失败⚠\n" + e.WebMessageAsJson.ToString());
        }
        private void ToImage(string img64)

        {
            string strbase64 = img64.Trim().Substring(img64.IndexOf(",") + 1);
            byte[] bytes = Convert.FromBase64String(strbase64);
            MemoryStream memStream = new MemoryStream(bytes);
            Bitmap bmpt = new Bitmap(memStream);
            this.CAPTCHA.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmpt.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); ;
        }
        private void Web_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            //Web.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            Web.CoreWebView2.Settings.AreDevToolsEnabled = false;
            Web.CoreWebView2.Settings.AreHostObjectsAllowed = false;
            Web.CoreWebView2.Settings.IsStatusBarEnabled = false;
            Web.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            Web.CoreWebView2.Settings.IsZoomControlEnabled = false;
        }

        private void PinBox_Completed(object sender, RoutedEventArgs e)
        {
            CAPTCHA.Visibility = Visibility.Hidden;
            CAPTCHABox.Visibility = Visibility.Hidden;
            login();
        }

        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            Web.GoBack();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            Web.GoForward();
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if ((this.Width - 480) > 100)
            {
                if (AddressBox.Visibility == Visibility.Hidden)
                {
                    AddressBox.Visibility = Visibility.Visible;
                }
                AddressBox.Width = this.Width - 480;
            }
            else
            {
                AddressBox.Visibility = Visibility.Hidden;
            }
        }
    }
}

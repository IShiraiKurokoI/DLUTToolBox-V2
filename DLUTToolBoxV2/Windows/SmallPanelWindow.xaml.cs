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
using System.IO;
using System.Diagnostics;
using HandyControl.Controls;
using System.IO.Pipes;
using System.Security.Principal;
using System.Drawing;
using HandyControl.Themes;
using HandyControl.Data;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// SmallPanelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SmallPanelWindow :  HandyControl.Controls.Window
    {
        bool Restarted = false;
        bool BG = false;
        public SmallPanelWindow(int i)
        {
            InitializeComponent();
            ThemeManager.Current.SystemThemeChanged += OnSystemThemeChanged;
            SetBackgroundImage();
            switch(i)
            {
                case 0:
                    {
                        this.TitleLabel.Content = "Adobe下载指引";
                        this.Title = "Adobe下载指引";
                        Adobe.Visibility = Visibility.Visible;
                        break;
                    }
                case 1:
                    {
                        this.TitleLabel.Content = "文件夹背景调整";
                        this.Title = "文件夹背景调整";
                        BG = true;
                        Folderbg.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        this.TitleLabel.Content = "文件夹背景调整";
                        this.Title = "文件夹背景调整";
                        BG = true;
                        Folderbg.Visibility = Visibility.Visible;
                        Restarted = true;
                        break;
                    }
            }
        }

        private void OnSystemThemeChanged(object sender, FunctionEventArgs<ThemeManager.SystemTheme> e)
        {
            ThemeManager.Current.ApplicationTheme = e.Info.CurrentTheme;
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

        private void CreativeCloud_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://creativecloud.adobe.com/apps/all/desktop?promoid=KSPBI");
        }

        private void Genp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://mediafire.com/file/3lpsrxiz47mlhu2/Adobe-GenP-2.7.zip/file");
            Process.Start("https://pan.baidu.com/s/1HBfqKUxpFIEpyDGRgB60-g?pwd=0301");
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.ghxi.com/adobemaster2022.html");
        }

        private void MS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421fde40f982b257c1e7b0c9ce29b5b/help/adobe/course");
            Process.Start("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421fde40f982b257c1e7b0c9ce29b5b/download/list/adobe.html");
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("31reset");
        }

        string SmallViewstring = Properties.Settings.Default.str1;
        string ListViewstring = Properties.Settings.Default.str2;
        string FlatViewstring = Properties.Settings.Default.str3;
        string MidBigViewstring = Properties.Settings.Default.str4;
        string DetailViewstring = Properties.Settings.Default.str5;
        string ContentViewstring = Properties.Settings.Default.str6;

        void tryloadprevioussettings()
        {
            if (File.Exists("paths.txt"))
            {
                string[] PicPaths = new string[6];
                int i = 0;
                using (StreamReader sr = new StreamReader("paths.txt"))
                {
                    string line;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        PicPaths[i] = line;
                        if (i == 5)
                        {
                            break;
                        }
                        i++;
                    }
                    sr.Close();
                }
                if (i == 5)
                {
                    if (File.Exists(PicPaths[0]))
                    {
                        SmallViewstring = PicPaths[0];
                        Bitmap bitmap=new Bitmap(PicPaths[0]);
                        ImageBrush ib=new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox1.Background = ib;
                    }
                    if (File.Exists(PicPaths[1]))
                    {
                        ListViewstring = PicPaths[1];
                        Bitmap bitmap = new Bitmap(PicPaths[1]);
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox2.Background = ib;
                    }
                    if (File.Exists(PicPaths[2]))
                    {
                        FlatViewstring = PicPaths[2];
                        Bitmap bitmap = new Bitmap(PicPaths[2]);
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox3.Background = ib;
                    }
                    if (File.Exists(PicPaths[3]))
                    {
                        MidBigViewstring = PicPaths[3];
                        Bitmap bitmap = new Bitmap(PicPaths[3]);
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox4.Background = ib;
                    }
                    if (File.Exists(PicPaths[4]))
                    {
                        DetailViewstring = PicPaths[4];
                        Bitmap bitmap = new Bitmap(PicPaths[4]);
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox5.Background = ib;
                    }
                    if (File.Exists(PicPaths[5]))
                    {
                        ContentViewstring = PicPaths[5];
                        Bitmap bitmap = new Bitmap(PicPaths[5]);
                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        pictureBox6.Background = ib;
                    }
                }
            }
            else
            {
                if (File.Exists(Properties.Settings.Default.str1))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str1);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox1.Background = ib;
                }
                if (File.Exists(Properties.Settings.Default.str2))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str2);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox2.Background = ib;
                }
                if (File.Exists(Properties.Settings.Default.str3))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str3);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox3.Background = ib;
                }
                if (File.Exists(Properties.Settings.Default.str4))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str4);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox4.Background = ib;
                }
                if (File.Exists(Properties.Settings.Default.str5))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str5);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox5.Background = ib;
                }
                if (File.Exists(Properties.Settings.Default.str6))
                {
                    Bitmap bitmap = new Bitmap(Properties.Settings.Default.str6);
                    ImageBrush ib = new ImageBrush();
                    ib.ImageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    pictureBox6.Background = ib;
                }
            }
        }


        async Task SendMessageToCore(string msg)
        {
            NamedPipeClientStream PipeSender = new NamedPipeClientStream("localhost", "ToolBoxCorePipe", PipeDirection.Out, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
            StreamWriter sw = null;
            bool started = true;
            LogHelper.WriteInfoLog("尝试向后台IPC发送信息:" + msg);
            if (System.Diagnostics.Process.GetProcessesByName("ToolBox.Core").ToList().Count == 0)
            {
                LogHelper.WriteInfoLog("后台IPC未启动，正在启动 ");
                Process P = new Process();
                P.StartInfo.UseShellExecute = true;
                P.StartInfo.FileName = System.IO.Directory.GetCurrentDirectory() + "\\Binary\\Win64\\Core\\ToolBox.Core.exe";
                P.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                P.Start();
                started = false;
            }
            try
            {
                if (started == true)
                {
                    LogHelper.WriteInfoLog("尝试连接IPC");
                    PipeSender.Connect(2000);
                }
                else
                {
                    LogHelper.WriteInfoLog("尝试连接IPC");
                    PipeSender.Connect(5000);
                }
            }
            catch (Exception e)
            {
                Growl.InfoGlobal("⚠IPC连接失败！⚠");
                LogHelper.WriteErrLog(e);
                return;
            }
            sw = new StreamWriter(PipeSender);
            sw.AutoFlush = true;
            LogHelper.WriteInfoLog("发送信息");
            sw.WriteLine(msg);
            LogHelper.WriteInfoLog("结束发送");
            sw.WriteLine("End");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("path.txt"))
            {
                File.Delete("paths.txt");
            }
            FileStream fs = new FileStream("paths.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            if (SmallViewstring.Length * ListViewstring.Length * FlatViewstring.Length * MidBigViewstring.Length * DetailViewstring.Length * ContentViewstring.Length == 0)
            {
                Growl.WarningGlobal("⚠提示⚠\n所有视图图片均不可为空！\n强制设置空图片将会导致资源管理器无法打开对应视图！");
                sw.Close();
                File.Delete("paths.txt");
                return;
            }
            sw.WriteLine(SmallViewstring);
            sw.WriteLine(ListViewstring);
            sw.WriteLine(FlatViewstring);
            sw.WriteLine(MidBigViewstring);
            sw.WriteLine(DetailViewstring);
            sw.WriteLine(ContentViewstring);
            sw.Close();
            SendMessageToCore("31" + Environment.CurrentDirectory + "\\paths.txt");
            Properties.Settings.Default.str1 = SmallViewstring;
            Properties.Settings.Default.str2 = ListViewstring;
            Properties.Settings.Default.str3 = FlatViewstring;
            Properties.Settings.Default.str4 = MidBigViewstring;
            Properties.Settings.Default.str5 = DetailViewstring;
            Properties.Settings.Default.str6 = ContentViewstring;
            Properties.Settings.Default.Save();
        }

        private void pictureBox1_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str1=pictureBox1.Uri.ToString().Split(new string[] { "file:///" },StringSplitOptions.RemoveEmptyEntries)[0];
            SmallViewstring= pictureBox1.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void pictureBox2_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str2 = pictureBox2.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            ListViewstring = pictureBox2.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void pictureBox3_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str3 = pictureBox3.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            FlatViewstring = pictureBox3.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void pictureBox4_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str4 = pictureBox4.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            MidBigViewstring = pictureBox4.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void pictureBox5_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str5 = pictureBox5.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            DetailViewstring = pictureBox5.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void pictureBox6_ImageSelected(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.str6 = pictureBox6.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            ContentViewstring = pictureBox6.Uri.ToString().Split(new string[] { "file:///" }, StringSplitOptions.RemoveEmptyEntries)[0];
            Properties.Settings.Default.Save();
            pictureBox1.Background = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(BG==true)
            {
                tryloadprevioussettings();
            }
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

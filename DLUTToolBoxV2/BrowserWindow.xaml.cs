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
using Microsoft.Web.WebView2.Core;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// BrowserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BrowserWindow : HandyControl.Controls.Window
    {
        string FinalUri = "";
        string rejump = "";
        int count = 0;
        int time = 0;
        int specialhandlenum = 0;
        public BrowserWindow(string Url, string _FinalUri, string _rejump, int jumpwaittime, int _specialhandlenum, string _name)
        {
            InitializeComponent();
            this.TitleLabel.Content = _name;
            count = 0;
            time = jumpwaittime;
            Web.Source = new Uri(Url);
            FinalUri = _FinalUri;
            rejump = _rejump;
            specialhandlenum = _specialhandlenum; 
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
            if (specialhandlenum == 23)
            {
                Web.Visibility = Visibility.Visible;
            }
            if (specialhandlenum == 19)
            {
                this.Width = this.Width - 850;
                this.Height = this.Height + 100;
                this.Backward.Visibility = Visibility.Hidden;
                this.Forward.Visibility = Visibility.Hidden;
                this.ResizeMode = ResizeMode.CanMinimize;
            }
            if (specialhandlenum == 24)
            {
                this.Width = this.Width - 900;
                this.ResizeMode = ResizeMode.CanMinimize;
            }
            if (specialhandlenum == 25)
            {
                this.Width = this.Width +200;
            }
            SetBackgroundImage();
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

        async Task newtab()
        {
            try
            {
                await Task.Delay(5000);
                Web.CoreWebView2.ExecuteScriptAsync("var num=document.getElementsByClassName('get-into').length;for(i = 0; i < num; i++){document.getElementsByClassName('get-into')[i].target = '_blank'}");
                Browser_Background.Content = "";
                LoadingCircle.Visibility = Visibility.Hidden;
                Web.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        void resize()
        {
            Web.ZoomFactor = 0.5;
            string rs = "document.getElementsByClassName('main-container')[0].style='margin:0px;padding:0px;width:100%'";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
            rs = "document.getElementsByClassName('container')[2].style='margin:0px;padding:0px;width:100%;'";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
            rs = "document.getElementsByClassName('row')[2].style='margin:0px;padding:0px;width:100%'";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
        }

        void resize_back()
        {
            Web.ZoomFactor = 1;
            string rs = "document.getElementsByClassName('main-container')[0].style=''";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
            rs = "document.getElementsByClassName('container')[2].style=''";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
            rs = "document.getElementsByClassName('row')[2].style=''";
            Web.CoreWebView2.ExecuteScriptAsync(rs);
        }

        CoreWebView2DownloadOperation downloadOperation;
        private void Web_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            Web.CoreWebView2.Settings.AreDevToolsEnabled = false;
            Web.CoreWebView2.Settings.AreHostObjectsAllowed = false;
            Web.CoreWebView2.Settings.IsStatusBarEnabled = false;
            Web.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            Web.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
            Web.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindow;
            Web.DefaultBackgroundColor = System.Drawing.Color.Transparent;
        }

        private void CoreWebView2_NewWindow(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            if (specialhandlenum != 7)
            {
                Web.CoreWebView2.ExecuteScriptAsync("window.location.href='" + e.Uri.ToString() + "'");
                e.Handled = true;
            }
            else
            {

            }
        }
        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.Description = "请选择文件保存位置:";
            folderDlg.ShowDialog();
            string filename = e.DownloadOperation.ResultFilePath.ToString().Split(new[] { "\\" }, StringSplitOptions.None)[4];
            if (folderDlg.SelectedPath == "")
            {
                Growl.InfoGlobal("⚠⚠提示⚠⚠\n由于未指定另存为路径，已将保存位置重定向至桌面");
                e.ResultFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "\\" + filename;
            }
            else
            {
                e.ResultFilePath = folderDlg.SelectedPath + "\\" + filename;
            }
            downloadOperation = e.DownloadOperation; // Store the 'DownloadOperation' for later use in event
            downloadOperation.StateChanged += DownloadOperation_StateChanged;
            Growl.InfoGlobal("下载已启动！\n" + "文件名：" + filename + "\n储存路径：" + e.ResultFilePath.ToString() + "\n文件大小：" + formatdataflow(downloadOperation.TotalBytesToReceive.Value.ToString()) + "\n预计完成时间：" + downloadOperation.EstimatedEndTime.ToString());
        }
        string formatdataflow(string num)
        {
            double temp = double.Parse(num);
            string re = "";
            if (temp > 1000000000)
            {
                temp /= (double)(1024 * 1024 * 1024);
                re = temp.ToString() + "G";
            }
            else if (temp > 1000000)
            {
                temp /= (double)(1024 * 1024);
                re = temp.ToString() + "M";
            }
            else
            {
                temp /= (double)1024;
                re = temp.ToString() + "K";
            }
            return re + "B";
        }
        private void DownloadOperation_StateChanged(object sender, object e)
        {
            if (downloadOperation.State.ToString() == "Completed")
            {
                string[] dataspace = downloadOperation.ResultFilePath.ToString().Split(new[] { "\\" }, StringSplitOptions.None);
                string filename = dataspace[dataspace.Length - 1];
                Growl.SuccessGlobal("文件" + filename + "已完成下载！" + "储存位置：" + downloadOperation.ResultFilePath.ToString() + "\n总下载大小:" + formatdataflow(downloadOperation.TotalBytesToReceive.Value.ToString()));
            }
        }

        async Task apilogin()
        {
            string jscode = "username.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "password.value='" + Properties.Settings.Default.UnionPassword + "'";
            await Web.CoreWebView2.ExecuteScriptAsync(jscode);
            await Web.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jsenable = "btnpc.disabled=''";
            await Web.CoreWebView2.ExecuteScriptAsync(jsenable);
            string jscode2 = "btnpc.click()";
            await Web.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        async Task login()
        {
            string jscode = "un.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "pd.value='" + Properties.Settings.Default.UnionPassword + "'";
            string rm = "rememberName.checked='checked'";
            await Web.CoreWebView2.ExecuteScriptAsync(rm);
            await Web.CoreWebView2.ExecuteScriptAsync(jscode);
            await Web.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jscode2 = "login()";
            await Web.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        int innercount = 0;
        bool warnshown = false;
        bool webvpn = false;
        private void Web_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if(Web.Source.AbsolutePath.IndexOf("C:/") ==-1)
            {
                AddressBox.Text = Web.Source.AbsoluteUri;
            }
            else
            {
                AddressBox.Text = "校园网状态监控页面";
            }
            if (Web.CoreWebView2.DocumentTitle.IndexOf("过期") != -1)
            {
                Web.ExecuteScriptAsync("document.getElementsByClassName('layui-layer-btn0')[0].click()");
                Web.ExecuteScriptAsync("document.getElementsByClassName('layui-layer-btn0')[0].click()");
                Web.ExecuteScriptAsync("document.getElementsByClassName('layui-layer-btn0')[0].click()");
                if (warnshown == false)
                {
                    Growl.InfoGlobal("⚠来自网页的安全提示⚠\n根据信息安全等级保护要求，用户密码需定期更换。请尽快到【校园门户】-【我的信息】-【安全设置】中修改！");
                    warnshown = true;
                }
                return;
            }
            if (Web.Source.AbsoluteUri == "https://webvpn.dlut.edu.cn/login")
            {
                string jsjump = "window.location.href='/login?cas_login=true'";
                Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                webvpn = true;
                return;
            }
            if (Web.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=") != -1)
            {
                login();
                return;
            }
            if (Web.Source.AbsoluteUri.IndexOf("cas/login?service=https%3A%2F%2Fwebvpn.dlut.edu.cn%2Flogin") != -1)
            {
                login();
                return;
            }
            if (Web.Source.AbsoluteUri.IndexOf("api") != -1)
            {
                apilogin();
            }
            switch (specialhandlenum)
            {
                case 0:
                    {
                        Browser_Background.Content = "";
                        LoadingCircle.Visibility = Visibility.Hidden;
                        Web.Visibility = Visibility.Visible;
                        break;
                    }
                case 1:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/lesson-search'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("lesson-search") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 2:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/program-completion-preview'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("program-completion-preview") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 3:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/exam-arrange'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("exam-arrange") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 4:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/exam-delay-apply'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("exam-delay-apply") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 5:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/grade/sheet'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("grade/sheet") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 6:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/evaluation/summative'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("evaluation") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 7:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/course-select'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("for-std/course-select") != -1)
                        {
                            Growl.InfoGlobal("正在优化页面内容, 请等待五秒。。。");
                            newtab();
                        }
                        break;
                    }
                case 8:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/course-table'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("course-table") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 9:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("/student/home") != -1)
                        {
                            Web.ExecuteScriptAsync("window.location.href='/student/for-std/adminclass-course-table'");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("adminclass-course-table") != -1)
                        {
                            Browser_Background.Content = "";
                            LoadingCircle.Visibility = Visibility.Hidden;
                            Web.Visibility = Visibility.Visible;
                        }
                        break;
                    }
                case 10:
                    {
                        Browser_Background.Content = "";
                        LoadingCircle.Visibility = Visibility.Hidden;
                        Web.Visibility = Visibility.Visible;
                        break;
                    }
                case 11:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri == "https://webvpn.dlut.edu.cn/login")
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                        Web.CoreWebView2.ExecuteScriptAsync("alert('账户dllgdx密码wfdllgdx')");
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    if (webvpn == true)
                                    {
                                        Web.CoreWebView2.ExecuteScriptAsync("alert('账户dllgdx密码wfdllgdx')");
                                    }
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 12:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri == "https://webvpn.dlut.edu.cn/login")
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421f7b9569d2936695e790c88b8991b203a18454272/index.html'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421f7b9569d2936695e790c88b8991b203a18454272/index.html'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 13:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri == "https://webvpn.dlut.edu.cn/login")
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421e7e056d22b396a1e7a049cb8d65027204e7199/sjkdhejlbyAtoZ/ALL.htm'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421e7e056d22b396a1e7a049cb8d65027204e7199/sjkdhejlbyAtoZ/ALL.htm'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 14:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri == "https://webvpn.dlut.edu.cn/login")
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 15:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri.IndexOf("https://webvpn.dlut.edu.cn/login") != -1)
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f9fa4e93247e6c5c6b1cc7a99c406d3642/sso/dlut'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        login();
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 16:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri.IndexOf("https://webvpn.dlut.edu.cn/login") != -1)
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='http://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f4e2558f267e6c5c6b1cc7a99c406d36b7/cas'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        login();
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 17:
                    {
                        Browser_Background.Content = "";
                        LoadingCircle.Visibility = Visibility.Hidden;
                        Web.Visibility = Visibility.Visible;
                        break;
                    }
                case 18:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    string jscode = "uid.value='" + Properties.Settings.Default.MailAddress + "'";
                                    Web.CoreWebView2.ExecuteScriptAsync(jscode);
                                    string jscode1 = "password.value='" + Properties.Settings.Default.MailPassword + "'";
                                    Web.CoreWebView2.ExecuteScriptAsync(jscode1);
                                    string rm = "domain.value='mail.dlut.edu.cn'";
                                    Web.CoreWebView2.ExecuteScriptAsync(rm);
                                    Web.CoreWebView2.ExecuteScriptAsync(jscode);
                                    string jscode2 = "document.getElementsByClassName('u-btn u-btn-primary submit j-submit')[0].click()";
                                    Web.CoreWebView2.ExecuteScriptAsync(jscode2);
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 19:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 20:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri.IndexOf("https://webvpn.dlut.edu.cn/login") != -1)
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f5f4408e23206949730d87b8d6512f209640763a21f75b0c/'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        login();
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 21:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri.IndexOf("https://webvpn.dlut.edu.cn/login") != -1)
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f5f4408e23206949730d87b8d6512f209640763a21f75b0c/#/project/pay/eleCostOfDlutPay'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        login();
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 22:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    if (Web.Source.AbsoluteUri.IndexOf("https://webvpn.dlut.edu.cn/login") != -1)
                                    {
                                        string jsjump = "window.location.href='/login?cas_login=true'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        webvpn = true;
                                    }
                                    else
                                    {
                                        string jsjump = "window.location.href='https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f5f4408e23206949730d87b8d6512f209640763a21f75b0c/#/project/pay/netCostOfSlPay'";
                                        Web.CoreWebView2.ExecuteScriptAsync(jsjump);
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    if (webvpn == true)
                                    {
                                        login();
                                    }
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 23:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 24:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("https://card.m.dlut.edu.cn/elepay/openElePay?openid=") != -1)
                        {
                            Web.CoreWebView2.ExecuteScriptAsync(Properties.Resources.jsfunc);
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("https://card.m.dlut.edu.cn/virtualcard/openVirtualcard?") != -1)
                        {
                            Web.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('code')[0].className=''");
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("https://card.m.dlut.edu.cn/homerj/openHomePage?") != -1)
                        {

                        }
                        switch (count)
                        {
                            case 0:
                                {
                                    Web.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Linux; Android 10; EBG-AN00 Build/HUAWEIEBG-AN00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/83.0.4103.106 Mobile Safari/537.36 weishao(3.2.2.74616)";
                                    Web.Source = new Uri("https://api.m.dlut.edu.cn/oauth/authorize?client_id=19b32196decf419a&redirect_uri=https%3A%2F%2Fcard.m.dlut.edu.cn%2Fhomerj%2FopenRjOAuthPage&response_type=code&scope=base_api&state=weishao");
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                break;
                        }
                        break;
                    }
                case 25:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    Web.Source = new Uri("http://seat.lib.dlut.edu.cn/yanxiujian/client/areaSelectSeat.php");
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                {
                                    if (Web.Source.AbsoluteUri.ToString().IndexOf("http://seat.lib.dlut.edu.cn/yanxiujian/client/orderSeat.php") != -1)
                                    {
                                        resize();
                                    }
                                    else
                                    {
                                        resize_back();
                                    }
                                    if (Web.Source.AbsoluteUri.ToString().IndexOf("http://seat.lib.dlut.edu.cn/yanxiujian/client/areaSelectSeat.php") != -1 || Web.Source.AbsoluteUri.ToString().IndexOf("http://seat.lib.dlut.edu.cn/yanxiujian/client/index.php") != -1)
                                    {
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        Browser_Background.Content = "";
                                        LoadingCircle.Visibility = Visibility.Hidden;
                                        Web.Visibility = Visibility.Visible;
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case 26:
                    {
                        switch (count)
                        {
                            case 0:
                                {
                                    Web.Source = new Uri("http://seat.lib.dlut.edu.cn/yanxiujian/client/orderInfo.php");
                                    break;
                                }
                            case 1:
                                {
                                    Browser_Background.Content = "";
                                    LoadingCircle.Visibility = Visibility.Hidden;
                                    Web.Visibility = Visibility.Visible;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        if (Web.Source.AbsoluteUri.IndexOf("api") != -1)
                        {
                            apilogin();
                        }
                        if (Web.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=") != -1)
                        {
                            login();
                        }
                        Browser_Background.Content = "";
                        LoadingCircle.Visibility = Visibility.Hidden;
                        Web.Visibility = Visibility.Visible;
                        break;
                    }
            }
            count++;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Web.Dispose();
        }

        private void Web_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            Console.WriteLine(e.Uri);
            if(e.Uri.StartsWith("https://ibsbjstar.ccb.com.cn/CCBIS/B2CMainPlat"))
            {
                if(e.Uri.IndexOf("?CLIENTIP=&BRANCHID=") !=-1)
                {
                    Growl.SuccessGlobal("正在打开建行Web支付页面，请自行支付!");
                    Process.Start(e.Uri);
                }
            }
            if(e.Uri.StartsWith("alipays://"))
            {
                new QRPayCodeWindow(e.Uri).Show();
            }
            if(e.Uri.StartsWith("https://mclient.alipay.com/cashier/mobilepay.htm?"))
            {
                Growl.SuccessGlobal("链接获取成功，请点击打开支付宝APP后使用手机支付宝扫码付款！");
            }
            if (e.Uri.StartsWith("weixin://"))
            {
                Growl.InfoGlobal("暂不支持微信支付功能,敬请期待（TNND微信接口太难用了）");
                e.Cancel = true;
            }
            if (e.Uri.IndexOf("mobile/api/unifiedOrderIndex.action?")!=-1)
            {
                Growl.SuccessGlobal("链接获取成功，请使用云闪付手机APP扫码付款！");
                new QRPayCodeWindow(e.Uri).Show();
            }
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            Web.GoBack();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            Web.GoForward();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((this.Width - 480) > 100)
            {
                if(AddressBox.Visibility==Visibility.Hidden)
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

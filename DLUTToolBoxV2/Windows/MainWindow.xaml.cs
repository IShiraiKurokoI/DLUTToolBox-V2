﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using HandyControl.Controls;
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.IO.Pipes;
using System.Security.Principal;
using TaskScheduler;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using Microsoft.Web.WebView2.Core;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using HandyControl.Themes;
using HandyControl.Data;
using DLUTToolBox_V2.Helper;
using Microsoft.Web.WebView2.WinForms;

namespace DLUTToolBox_V2
{

    public class GithubLatest
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public Author author { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public Asset[] assets { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
    }

    public class Author
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Asset
    {
        public string url { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public object label { get; set; }
        public Uploader uploader { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string browser_download_url { get; set; }
    }

    public class Uploader
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }


    public class DrcomStatus
    {
        public int result { get; set; }
        public int time { get; set; }
        public long flow { get; set; }
        public int fsele { get; set; }
        public int fee { get; set; }
        public int m46 { get; set; }
        public string v46ip { get; set; }
        public string myv6ip { get; set; }
        public long oltime { get; set; }
        public long olflow { get; set; }
        public string lip { get; set; }
        public string stime { get; set; }
        public string etime { get; set; }
        public string uid { get; set; }
        public int v6af { get; set; }
        public int v6df { get; set; }
        public int v46m { get; set; }
        public string v4ip { get; set; }
        public string v6ip { get; set; }
        public string AC { get; set; }
        public string ss5 { get; set; }
        public string ss6 { get; set; }
        public int vid { get; set; }
        public string ss1 { get; set; }
        public string ss4 { get; set; }
        public int cvid { get; set; }
        public int pvid { get; set; }
        public int hotel { get; set; }
        public int aolno { get; set; }
        public int eport { get; set; }
        public int eclass { get; set; }
        public int zxopt { get; set; }
        public string NID { get; set; }
        public int olno { get; set; }
        public string udate { get; set; }
        public string olmac { get; set; }
        public int ollm { get; set; }
        public string olm1 { get; set; }
        public string olm2 { get; set; }
        public int olm3 { get; set; }
        public int olmm { get; set; }
        public int olm5 { get; set; }
        public int gid { get; set; }
        public int ispid { get; set; }
        public string opip { get; set; }
        public int actM { get; set; }
        public int actt { get; set; }
        public int actdf { get; set; }
        public int actuf { get; set; }
        public int act6df { get; set; }
        public int act6uf { get; set; }
        public int allfm { get; set; }
        public int d1 { get; set; }
        public int u1 { get; set; }
        public int d2 { get; set; }
        public int u2 { get; set; }
        public int o1 { get; set; }
        public int nd1 { get; set; }
        public int nu1 { get; set; }
        public int nd2 { get; set; }
        public int nu2 { get; set; }
        public int no1 { get; set; }
    }


    public enum PanelSelected
    {
        Overview,
        NetWork,
        Electricty,
        Exam,
        Study,
        WorkSpace,
        Library,
        Other,
        System,
        Settings,
        About
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : HandyControl.Controls.Window
    {
        PanelSelected _PanelSelected = PanelSelected.Overview;
        bool DoSettingsInitialized = false;
        public MainWindow()
        {
            InitializeComponent();
            ClassTable.CreationProperties = new Microsoft.Web.WebView2.Wpf.CoreWebView2CreationProperties
            {
                UserDataFolder = "UDFS\\ClassTableUDF"
            };
            ClassTable.Source = new Uri("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fcourseschedule&response_type=code");
            Eleinfo.CreationProperties = new Microsoft.Web.WebView2.Wpf.CoreWebView2CreationProperties
            {
                UserDataFolder = "UDFS\\EleinfoUDF"
            };
            Eleinfo.Source = new Uri("https://api.m.dlut.edu.cn/oauth/authorize?client_id=19b32196decf419a&redirect_uri=https%3A%2F%2Fcard.m.dlut.edu.cn%2Fhomerj%2FopenRjOAuthPage&response_type=code");
            WorkSpace_Web.CreationProperties = new Microsoft.Web.WebView2.Wpf.CoreWebView2CreationProperties
            {
                UserDataFolder = "UDFS\\EHallUDF"
            };
            //WorkSpace_Web.Source = new Uri("https://sso.dlut.edu.cn/cas/login?service=https%3A%2F%2Fehall.dlut.edu.cn%2Ffp%2Fview%3Fm%3Dfp#act=fp/formHome");
            WorkSpace_Web.Source = new Uri("https://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421f5ff40902b7e6c5c6b1cc7a99c406d3607/fp/view?m=fp&filter=app&from=rj");
            WeatherBar.CreationProperties = new Microsoft.Web.WebView2.Wpf.CoreWebView2CreationProperties
            {
                UserDataFolder = "UDFS\\WeatherUDF"
            };
            WeatherBar.Source = new Uri("http://www.weather.com.cn/");
            ThemeManager.Current.SystemThemeChanged += OnSystemThemeChanged;
            this.TitleLabel.Content = "DLUTToolBox V2-信息总览";
            Overview.Visibility = Visibility.Visible;
            try
            {
                SettingsInitializer();
                SetBackgroundImage();
                AutoLoginChecker();
                FetchColorFromSystem();
                SettingsChecker();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                LogHelper.WriteErrLog(e);
            }
            AddressBox.Width = this.Width - 470;
        }

        private void OnSystemThemeChanged(object sender, FunctionEventArgs<ThemeManager.SystemTheme> e)
        {
            ThemeManager.Current.ApplicationTheme = e.Info.CurrentTheme;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Growl.ClearGlobal();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private async void Window_SourceInitialized(object sender, EventArgs e)
        {
            try
            {
                netstatusload();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                LogHelper.WriteErrLog(ex);
            }
        }

        bool netload = false;
        async Task netstatusload()
        {
            await Task.Delay(400);
            Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("curl --max-time 2 --connect-timeout 2 --url http://mrtg.dlut.edu.cn/internal/dut.html  & exit");
                p.StandardInput.AutoFlush = true;
                string strre = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
                if (strre.IndexOf("Unauthorized") != -1)
                {
                    Overview_NetworkInfo.Dispatcher.Invoke(new outputDelegate(OverviewSetText), "当前不在校园网内\n部分功能无法使用!");
                    NetWork_NetworkInfo.Dispatcher.Invoke(new outputDelegate(NetWorkSetText), "当前不在校园网内\n部分功能无法使用!");
                    if (Properties.Settings.Default.DoAutoUpdate == true)
                    {
                        checkforupdate();
                    }
                }
                else
                {
                    Overview_NetworkInfo.Dispatcher.Invoke(new outputDelegate(OverviewSetText), "校园网已接入");
                    NetWork_NetworkInfo.Dispatcher.Invoke(new outputDelegate(NetWorkSetText), "校园网已接入");
                    loadinfo();
                }
            });
        }



        private delegate void outputDelegate(string msg);

        private void OverviewSetText(string text)
        {
            Overview_NetworkInfo.Content = text;
        }
        private void NetWorkSetText(string text)
        {
            NetWork_NetworkInfo.Content = text;
        }

        private void OverviewAddText(string text)
        {
            Overview_NetworkInfo.Content = Overview_NetworkInfo.Content + text;
        }
        private void NetWorkAddText(string text)
        {
            NetWork_NetworkInfo.Content = NetWork_NetworkInfo.Content + text;
        }

        private void ReloadWeather(string text)
        {
            WeatherBar.Source = new Uri("http://www.weather.com.cn/");
        }

        void loadinfo()
        {
            using (WebClientPro client = new WebClientPro())
            {
                try
                {
                    string result = client.DownloadString("http://172.20.30.1/drcom/chkstatus?callback=");
                    string data = result.Split(new[] { "(" }, StringSplitOptions.None)[1].Split(new[] { ")" }, StringSplitOptions.None)[0];
                    DrcomStatus drcomStatus = JsonConvert.DeserializeObject<DrcomStatus>(data);
                    if (data.IndexOf("\"result\":1,") != -1)
                    {
                        double fee = drcomStatus.fee;
                        fee /= 10000;
                        string V4IP = drcomStatus.v4ip;
                        string flowused = formatdataflow_2(drcomStatus.flow);
                        string infostring = "\n校园网余额：" + fee + "\n本机校园网已用流量：\n" + flowused + "\nIPV4地址：" + V4IP + "\n网卡MAC：" + drcomStatus.olmac;
                        if (drcomStatus.flow > 96636764160)
                        {
                            infostring +="\n|本月流量使用已超过90G，请留意！！|\n";
                        }
                        Overview_NetworkInfo.Dispatcher.Invoke(new outputDelegate(OverviewAddText), infostring);
                        NetWork_NetworkInfo.Dispatcher.Invoke(new outputDelegate(NetWorkAddText), infostring);
                        netload = true;
                        if (Properties.Settings.Default.DoAutoUpdate == true)
                        {
                            checkforupdate();
                        }
                    }
                    else
                    {
                        Overview_NetworkInfo.Dispatcher.Invoke(new outputDelegate(OverviewAddText), "\n校园网并未认证");
                        NetWork_NetworkInfo.Dispatcher.Invoke(new outputDelegate(NetWorkAddText), "\n校园网并未认证");
                    }
                    if(Refresh == true)
                    {
                        Refresh = false;
                        Growl.SuccessGlobal("刷新成功");
                    }
                }
                catch (System.Net.WebException e)
                {
                    Growl.WarningGlobal("无法加载校园网信息，五秒超时已到");
                    Overview_NetworkInfo.Dispatcher.Invoke(new outputDelegate(OverviewAddText), "\n获取数据失败");
                    NetWork_NetworkInfo.Dispatcher.Invoke(new outputDelegate(NetWorkAddText), "\n获取数据失败");
                    if (Properties.Settings.Default.DoAutoUpdate == true)
                    {
                        checkforupdate();
                    }
                }
            }
        }

        string formatdataflow_2(long num)
        {
            double temp = num;
            string re = "";
            if (temp > 1048576)
            {
                temp /= (double)(1024 * 1024);
                re = temp.ToString() + "GB";
            }
            else if (temp > 1024)
            {
                temp /= (double)(1024);
                re = temp.ToString() + "MB";
            }
            else
            {
                re = temp + "KB";
            }
            return re;
        }

        private void SettingsChecker()
        {
            if (Properties.Settings.Default.Uid.Length * Properties.Settings.Default.UnionPassword.Length == 0)
            {
                this.TitleLabel.Content = "DLUTToolBox V2-参数配置";
                Growl.InfoGlobal("请先完善信息配置！\n邮箱及其密码为非必填项\n您可以稍后点击参数设置打开此界面\n输入完成后使用其他功能即可\n所有更改均会自动保存");
                HideOthers();
                SettingsPanel.Visibility = Visibility.Visible;
                _PanelSelected = PanelSelected.Settings;
            }
        }

        [System.Runtime.InteropServices.DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization, out bool pfOpaqueBlend);

        async Task FetchColorFromSystem()
        {
            if (Properties.Settings.Default.DoAutoThemeFollow == true)
            {
                DwmGetColorizationColor(out int pcrColorization, out _);
                String data = String.Format("{0:X}", pcrColorization);
                int A = Convert.ToInt32(data.Substring(0, 2), 16);
                int R = Convert.ToInt32(data.Substring(2, 2), 16);
                int G = Convert.ToInt32(data.Substring(4, 2), 16);
                int B = Convert.ToInt32(data.Substring(6, 2), 16);
                SideMenuColorBrush.Color = Color.FromArgb(Convert.ToByte(A), Convert.ToByte(R), Convert.ToByte(G), Convert.ToByte(B));
            }
            else
            {
                SideMenuColorBrush.Color = Color.FromRgb(Convert.ToByte("128"), Convert.ToByte("128"), Convert.ToByte("128"));
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

        private void AutoLoginChecker()
        {
            LogHelper.WriteInfoLog("正在检查自动登录配置文件");
            if (File.Exists("Network.config"))
            {
                string[] Paths = new string[2];
                int i = 0;
                using (StreamReader sr = new StreamReader("Network.config"))
                {
                    string line;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        Paths[i] = line;
                        if (i == 1)
                        {
                            break;
                        }
                        i++;
                    }
                    sr.Close();
                }
                if (Paths[0] != Properties.Settings.Default.Uid || Paths[1] != Properties.Settings.Default.UnionPassword)
                {
                    File.Delete("Network.config");
                    FileStream fs = new FileStream("Network.config", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(Properties.Settings.Default.Uid);
                    sw.WriteLine(Properties.Settings.Default.UnionPassword);
                    sw.Close();
                }
            }
            else
            {
                FileStream fs = new FileStream("Network.config", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(Properties.Settings.Default.Uid);
                sw.WriteLine(Properties.Settings.Default.UnionPassword);
                sw.Close();
            }
            if (Properties.Settings.Default.DoAutoLogin == false)
            {
                if (IsExists("AutoLogin3.0"))
                {
                    SendMessageToCore("false");
                }
                return;
            }
            else if (Properties.Settings.Default.DoAutoLogin == true)
            {
                if (IsExists("AutoLogin3.0"))
                {
                    IRegisteredTaskCollection tasks_exists = GetAllTasks();
                    for (int i = 1; i <= tasks_exists.Count; i++)
                    {
                        IRegisteredTask t = tasks_exists[i];
                        if (t.Name.Equals("AutoLogin3.0"))
                        {
                            String MainApplicationPath = System.IO.Directory.GetCurrentDirectory();
                            MainApplicationPath = MainApplicationPath + "\\DLUTToolBoxV2.exe";
                            if (t.Xml.IndexOf(MainApplicationPath) == -1)
                            {
                                SendMessageToCore("true");
                            }
                            break;
                        }
                    }
                    return;
                }
                else
                {
                    String MainApplicationPath = System.IO.Directory.GetCurrentDirectory();
                    MainApplicationPath = MainApplicationPath + "\\DLUTToolBoxV2.exe";
                    SendMessageToCore("true");
                }
            }
        }

        public static bool IsExists(string taskName)
        {
            var isExists = false;
            IRegisteredTaskCollection tasks_exists = GetAllTasks();
            for (int i = 1; i <= tasks_exists.Count; i++)
            {
                IRegisteredTask t = tasks_exists[i];
                if (t.Name.Equals(taskName))
                {
                    isExists = true;
                    break;
                }
            }
            return isExists;
        }
        public static IRegisteredTaskCollection GetAllTasks()
        {
            TaskSchedulerClass ts = new TaskSchedulerClass();
            ts.Connect(null, null, null, null);
            ITaskFolder folder = ts.GetFolder("\\");
            IRegisteredTaskCollection tasks_exists = folder.GetTasks(1);
            return tasks_exists;
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

        //处理侧边栏点击事件

        private void SettingsInitializer()
        {
            _Uid.Text = Properties.Settings.Default.Uid;
            UnionPassword.Password = Properties.Settings.Default.UnionPassword;
            MailAddress.Text = Properties.Settings.Default.MailAddress;
            MailPassword.Password = Properties.Settings.Default.MailPassword;
            DoAutoLogin.IsChecked = Properties.Settings.Default.DoAutoLogin;
            DoAutoThemeFollow.IsChecked = Properties.Settings.Default.DoAutoThemeFollow;
            DoAutoUpdate.IsChecked = Properties.Settings.Default.DoAutoUpdate;
            if (Properties.Settings.Default.BackGroundStrechMethod == "Fill")
            {
                Fill.IsChecked = true;
            }
            else if (Properties.Settings.Default.BackGroundStrechMethod == "Uniform")
            {
                Uniform.IsChecked = true;
            }
            else if (Properties.Settings.Default.BackGroundStrechMethod == "UniformToFill")
            {
                UniformToFill.IsChecked = true;
            }
            DoSettingsInitialized = true;
        }

        private void SideMenuItem_Selected(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Overview;
            this.TitleLabel.Content = "DLUTToolBox V2-信息总览";
            Overview.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_1(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.NetWork;
            this.TitleLabel.Content = "DLUTToolBox V2-网络工具";
            Network.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_2(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Electricty;
            this.TitleLabel.Content = "DLUTToolBox V2-日常缴费";
            Electricity.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_3(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Exam;
            this.TitleLabel.Content = "DLUTToolBox V2-考试教务";
            Exam.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_4(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Study;
            this.TitleLabel.Content = "DLUTToolBox V2-学习生活";
            Study.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_5(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.WorkSpace;
            this.TitleLabel.Content = "DLUTToolBox V2-办事大厅";
            WorkSpace.Visibility = Visibility.Visible;
            Backward.Visibility = Visibility.Visible;
            Forward.Visibility = Visibility.Visible;
            AddressBox.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_6(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Library;
            this.TitleLabel.Content = "DLUTToolBox V2-图书馆";
            Library.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_7(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Other;
            this.TitleLabel.Content = "DLUTToolBox V2-其它系统";
            Other.Visibility = Visibility.Visible;
        }

        bool System_PanelInitialized = false;

        private void SideMenuItem_Selected_8(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.System;
            this.TitleLabel.Content = "DLUTToolBox V2-系统工具";
            System_Panel.Visibility = Visibility.Visible;
            if (System_PanelInitialized == false)
            {
                kbbanloader();
                spacebanloader();
                TSXloader();
                VBSloader();
                TimeLineloader();
                onedriveloader();
                HighResolutionLoader();
                System_PanelInitialized = true;
            }
        }

        private void SideMenuItem_Selected_9(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.Settings;
            this.TitleLabel.Content = "DLUTToolBox V2-参数配置";
            SettingsPanel.Visibility = Visibility.Visible;
        }

        private void SideMenuItem_Selected_10(object sender, RoutedEventArgs e)
        {
            HideOthers();
            _PanelSelected = PanelSelected.About;
            this.TitleLabel.Content = "DLUTToolBox V2-关于软件";
            AboutPanel.Visibility = Visibility.Visible;
        }

        private void HideOthers()
        {
            switch (_PanelSelected)
            {
                case PanelSelected.Overview:
                    {
                        Overview.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.NetWork:
                    {
                        Network.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Electricty:
                    {
                        Electricity.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Exam:
                    {
                        Exam.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Study:
                    {
                        Study.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.WorkSpace:
                    {
                        WorkSpace.Visibility = Visibility.Hidden;
                        Backward.Visibility = Visibility.Hidden;
                        Forward.Visibility = Visibility.Hidden;
                        AddressBox.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Library:
                    {
                        Library.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Other:
                    {
                        Other.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.System:
                    {
                        System_Panel.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.Settings:
                    {
                        SettingsPanel.Visibility = Visibility.Hidden;
                        break;
                    }
                case PanelSelected.About:
                    {
                        AboutPanel.Visibility = Visibility.Hidden;
                        break;
                    }

            }
        }

        //处理设置面板事件

        private void Uid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.Uid = _Uid.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void MailAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.MailAddress = MailAddress.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void UnionPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.UnionPassword = UnionPassword.Password;
                Properties.Settings.Default.Save();
            }
        }

        private void MailPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.MailPassword = MailPassword.Password;
                Properties.Settings.Default.Save();
            }
        }

        private void DoAutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoLogin = (bool)DoAutoLogin.IsChecked;
                Properties.Settings.Default.Save();
                AutoLoginChecker();
                Growl.SuccessGlobal("已开启校园网自动登录");
            }
        }

        private void DoAutoLogin_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoLogin = (bool)DoAutoLogin.IsChecked;
                Properties.Settings.Default.Save();
                AutoLoginChecker();
                Growl.SuccessGlobal("已关闭校园网自动登录");
            }
        }

        private void DoAutoThemeFollow_Checked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoThemeFollow = (bool)DoAutoThemeFollow.IsChecked;
                Properties.Settings.Default.Save();
                FetchColorFromSystem();
                Growl.SuccessGlobal("已开启自动跟随系统主题");
            }
        }

        private void DoAutoThemeFollow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoThemeFollow = (bool)DoAutoThemeFollow.IsChecked;
                Properties.Settings.Default.Save();
                FetchColorFromSystem();
                Growl.SuccessGlobal("已关闭自动跟随系统主题");
            }
        }

        bool ManualCheck = false;

        private void DoAutoUpdate_Checked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoUpdate = (bool)DoAutoUpdate.IsChecked;
                Properties.Settings.Default.Save();
                ManualCheck = true;
                checkforupdate();
                Growl.SuccessGlobal("已开启自动更新");
            }
        }

        private void DoAutoUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.DoAutoUpdate = (bool)DoAutoUpdate.IsChecked;
                Properties.Settings.Default.Save();
                Growl.SuccessGlobal("已取消自动更新");
            }
        }

        private void SetBackground_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择想作为背景的图片:";
            dialog.InitialDirectory = "C:";
            dialog.Filter = "图片文件|*.jpg;*.png;*.bmp";
            dialog.CheckFileExists = true;
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                Properties.Settings.Default.BackgroundImageUrl = dialog.FileName;
                Properties.Settings.Default.Save();
                SetBackgroundImage();
                Growl.SuccessGlobal("背景设置成功！");
            }
        }

        private void ResetBackground_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BackgroundImageUrl = "";
            Properties.Settings.Default.Save();
            this.Background = null;
            Growl.SuccessGlobal("背景重置成功！");
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.BackGroundStrechMethod = "Fill";
                Properties.Settings.Default.Save();
                SetBackgroundImage();
            }
        }

        private void UniformToFill_Click(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.BackGroundStrechMethod = "UniformToFill";
                Properties.Settings.Default.Save();
                SetBackgroundImage();
            }
        }

        private void Uniform_Click(object sender, RoutedEventArgs e)
        {
            if (DoSettingsInitialized == true)
            {
                Properties.Settings.Default.BackGroundStrechMethod = "Uniform";
                Properties.Settings.Default.Save();
                SetBackgroundImage();
            }
        }

        private void CopySettings_Click(object sender, RoutedEventArgs e)
        {
            string configstring = "DLUTToolBoxV2SettingString/" + Properties.Settings.Default.Uid + "/" + Properties.Settings.Default.UnionPassword + "/ /" + Properties.Settings.Default.MailAddress + "/" + Properties.Settings.Default.MailPassword + "/" + Properties.Settings.Default.BackgroundImageUrl + "/" + Properties.Settings.Default.BackGroundStrechMethod + "/" + Properties.Settings.Default.DoAutoLogin + "/" + Properties.Settings.Default.DoAutoThemeFollow + "/" + Properties.Settings.Default.DoAutoUpdate;
            var array = Encoding.UTF8.GetBytes(configstring);
            configstring = Convert.ToBase64String(array);
            Clipboard.SetText(configstring);
            Growl.SuccessGlobal("配置串已复制至剪贴板！");
        }

        private void PasteSettings_Click(object sender, RoutedEventArgs e)
        {
            string configstring = Clipboard.GetText();
            try
            {
                var output = Convert.FromBase64String(configstring);
                configstring = Encoding.UTF8.GetString(output);
            }
            catch
            {
                Growl.InfoGlobal("错误的配置串！");
                return;
            }
            string[] config_all = configstring.Split(new[] { "/" }, StringSplitOptions.None);
            if (config_all[0] == "DLUTToolBoxV2SettingString")
            {
                try
                {
                    Properties.Settings.Default.Uid = config_all[1];
                    Properties.Settings.Default.UnionPassword = config_all[2];
                    Properties.Settings.Default.MailAddress = config_all[4];
                    Properties.Settings.Default.MailPassword = config_all[5];
                    Properties.Settings.Default.BackgroundImageUrl = config_all[6];
                    Properties.Settings.Default.BackGroundStrechMethod = config_all[7];
                    Properties.Settings.Default.DoAutoLogin = Convert.ToBoolean(config_all[8]);
                    Properties.Settings.Default.DoAutoThemeFollow = Convert.ToBoolean(config_all[9]);
                    Properties.Settings.Default.DoAutoUpdate = Convert.ToBoolean(config_all[10]);
                    Properties.Settings.Default.Save();
                }
                catch
                {
                    Growl.InfoGlobal("应用失败");
                    return;
                }
                SettingsInitializer();
                SetBackgroundImage();
                Clipboard.Clear();
                Growl.SuccessGlobal("应用成功！");
            }
            else
            {
                Growl.InfoGlobal("错误的配置串！");
                return;
            }
        }

        private void CleanFile_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(System.IO.Path.GetTempPath() + @"\NewVersion.rar"))
            {
                File.Delete(System.IO.Path.GetTempPath() + @"\NewVersion.rar");
            }
            if (Directory.Exists(System.IO.Path.GetTempPath() + @".\NewVersion\"))
            {
                Directory.Delete(System.IO.Path.GetTempPath() + @".\NewVersion\", true);
            }
            CoreWebView2Profile webViewProfile = ClassTable.CoreWebView2.Profile;
            webViewProfile.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            CoreWebView2Profile webViewProfile1 = WorkSpace_Web.CoreWebView2.Profile;
            webViewProfile1.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            CoreWebView2Profile webViewProfile2 = Eleinfo.CoreWebView2.Profile;
            webViewProfile2.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            CoreWebView2Profile webViewProfile3 = WeatherBar.CoreWebView2.Profile;
            webViewProfile3.ClearBrowsingDataAsync(CoreWebView2BrowsingDataKinds.AllProfile);
            DeleteDirectory(System.Environment.CurrentDirectory + "\\UDFS\\BrowserViewUDF");
            Growl.SuccessGlobal("清理完成！");
        }

        void DeleteDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] childs = dir.GetDirectories();
                foreach (DirectoryInfo child in childs)
                {
                    child.Delete(true);
                }
                dir.Delete(true);
            }
        }

        private void OpenLogFolder_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Log\\")))
            {
                Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Log\\"));
            }
            else
            {
                Growl.InfoGlobal("日志记录为空！");
            }
        }

        private void ManualUpdate_Click(object sender, RoutedEventArgs e)
        {
            Growl.InfoGlobal("正在检查更新，请稍候。。。");
            ManualCheck = true;
            checkforupdate();
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
        private string GetWebRequest(string url, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                int statusCode = (int)response.StatusCode;
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.WriteErrLog(ex);
            }
            return ret;
        }
        async Task checkforupdate()
        {
            Task.Run(async () =>
            {
                try
                {
                    string updmessage = GitUpdateRequest.GetWebRequest("https://api.github.com/repos/IShiraiKurokoI/DLUTToolBox-V2/releases/latest", Encoding.ASCII);
                    GithubLatest latest = JsonConvert.DeserializeObject<GithubLatest>(updmessage);
                    string versionname = latest.name;
                    string size = formatdataflow(latest.assets[0].size.ToString());
                    string upddate = latest.assets[0].updated_at.ToLocalTime().ToLongDateString() + latest.assets[0].updated_at.ToLocalTime().ToLongTimeString();
                    string downloadurl = latest.assets[0].browser_download_url;
                    string body = latest.body;
                    string[] Ver = new string[1];
                    if (File.Exists("Version.txt"))
                    {
                        using (StreamReader sr = new StreamReader("Version.txt"))
                        {
                            // 从文件读取并显示行，直到文件的末尾 
                            Ver[0] = sr.ReadLine();
                            sr.Close();
                        }
                    }
                    if (versionname != Ver[0])
                    {
                        LogHelper.WriteInfoLog("检测到新版本" + versionname + "\n大小：" + size + "\n更新日期：" + upddate + "\n更新内容：\n" + body + "\n" + downloadurl);
                        ShowUpdateMessage("最新版本：" + versionname + "\n大小：" + size + "\n更新日期：" + upddate + "\n更新内容：\n" + body, downloadurl);
                    }
                    else
                    {
                        if (ManualCheck == true)
                        {
                            Growl.InfoGlobal("当前已经是最新版本！");
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);
                    LogHelper.WriteErrLog(e);
                    Growl.InfoGlobal(e.Message);
                    return;
                }
                catch (Newtonsoft.Json.JsonSerializationException ee)
                {
                    Console.WriteLine(ee.Message + "\n" + ee.StackTrace);
                    LogHelper.WriteErrLog(ee);
                    Growl.InfoGlobal(ee.Message);
                    return;
                }
                catch (Newtonsoft.Json.JsonReaderException ee)
                {
                    Console.WriteLine(ee.Message + "\n" + ee.StackTrace);
                    LogHelper.WriteErrLog(ee);
                    Growl.InfoGlobal(ee.Message);
                    return;
                }
            });
        }

        void ShowUpdateMessage(string a, string url)
        {
            System.Windows.MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Ask(a, "检测到新版本！是否更新？");
            if (messageBoxResult == System.Windows.MessageBoxResult.OK)
            {
                StartDownloadAndSetup(url);
            }
            else
            {
                Growl.SuccessGlobal("更新已取消");
            }
        }

        void StartDownloadAndSetup(string url)
        {
            if (File.Exists(System.IO.Path.GetTempPath() + @"\NewVersion.rar"))
            {
                File.Delete(System.IO.Path.GetTempPath() + @"\NewVersion.rar");
            }
            if (Directory.Exists(System.IO.Path.GetTempPath() + @".\NewVersion\"))
            {
                Directory.Delete(System.IO.Path.GetTempPath() + @".\NewVersion\", true);
            }
            Growl.InfoGlobal("开始下载新版本\n从Github下载可能较慢，请耐心等待！");
            Console.WriteLine(url);
            if (HttpDownloadFile(url, System.IO.Path.GetTempPath() + @"\NewVersion.rar"))
            {
                Process p = new Process();
                p.StartInfo.FileName = Environment.CurrentDirectory + @"\ThirdParty\Unrar\UnRAR.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "x " + System.IO.Path.GetTempPath() + @"\NewVersion.rar " + System.IO.Path.GetTempPath() + @".\NewVersion\";
                p.StartInfo.RedirectStandardError = true;
                p.Start();
                p.WaitForExit();
                p.Close();
                Growl.ClearGlobal();
                System.Windows.MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show("新版下载完成！是否现在安装？", "新版下载完成！是否现在安装？", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == System.Windows.MessageBoxResult.Yes)
                {
                    try
                    {
                        Process.Start(System.IO.Path.GetTempPath() + @".\NewVersion\DLUTToolBoxV2\setup.exe");
                        Environment.Exit(0);
                    }
                    catch (Exception e)
                    {
                        Growl.FatalGlobal(e.Message);
                        LogHelper.WriteErrLog(e);
                    }
                }
                else
                {
                    try
                    {
                        Process.Start(System.IO.Path.GetTempPath() + @".\NewVersion\DLUTToolBoxV2\");
                        Growl.SuccessGlobal("已为您打开安装程序文件夹，您可以稍后自行手动安装！");
                    }
                    catch (Exception e)
                    {
                        Growl.FatalGlobal(e.Message);
                        LogHelper.WriteErrLog(e);
                    }
                }
            }
        }
        /// <summary>
        /// 设置证书安全性
        /// </summary>
        private static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        ///  <summary>
        ///  远程证书验证
        ///  </summary>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
        /// <summary>
        /// Http下载文件
        /// </summary>
        public static bool HttpDownloadFile(string url, string path)
        {
            try
            {
                using (var web = new WebClient())
                {
                    web.DownloadFile(url, path);
                }
                //// 设置参数
                //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //request.Timeout = 2000;
                ////发送请求并获取相应回应数据
                //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                ////直到request.GetResponse()程序才开始向目标网页发送Post请求
                //Stream responseStream = response.GetResponseStream();
                ////创建本地文件写入流
                //Stream stream = new FileStream(path, FileMode.Create);
                //byte[] bArr = new byte[10240000];
                //int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                //while (size > 0)
                //{
                //    stream.Write(bArr, 0, size);
                //    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                //}
                //stream.Close();
                //responseStream.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                LogHelper.WriteErrLog(e);
                Growl.InfoGlobal(e.Message);
                return false;
            }

        }

        //关于面板
        private void Email_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://mail.qq.com/cgi-bin/qm_share?t=qm_mailme&email=ishirai_kurokoi@qq.com");
        }

        private void BiliBili_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://space.bilibili.com/310144483");
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/MuoRanLY/DLUTToolBox-V2");
        }

        //信息总览
        private void WeatherBar_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            WeatherBar.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            WeatherBar.CoreWebView2.Settings.AreDevToolsEnabled = false;
            WeatherBar.CoreWebView2.Settings.AreHostObjectsAllowed = false;
            WeatherBar.CoreWebView2.Settings.IsStatusBarEnabled = false;
            WeatherBar.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            WeatherBar.CoreWebView2.Settings.IsZoomControlEnabled = false;
            WeatherBar.DefaultBackgroundColor = System.Drawing.Color.Transparent;
        }

        private void WeatherBar_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (WeatherBar.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login") != -1)
            {
                LogHelper.WriteInfoLog("检测到未登录打开工具箱，尝试登陆");
                LogHelper.WriteDebugLog("执行sso登录注入");
                string jscode = "un.value='" + Properties.Settings.Default.Uid + "'";
                string jscode1 = "pd.value='" + Properties.Settings.Default.UnionPassword + "'";
                string rm = "rememberName.checked='checked'";
                WeatherBar.CoreWebView2.ExecuteScriptAsync(rm);
                WeatherBar.CoreWebView2.ExecuteScriptAsync(jscode);
                WeatherBar.CoreWebView2.ExecuteScriptAsync(jscode1);
                string jscode2 = "login()";
                WeatherBar.CoreWebView2.ExecuteScriptAsync(jscode2);
                ClassTable.Height = 0;
                TableCirlcle.Visibility = Visibility.Visible;
            }
            else if (WeatherBar.Source.AbsoluteUri.IndexOf("http://www.weather.com.cn/") != -1)
            {
                WeatherHandle();
            }
            else if (WeatherBar.Source.AbsoluteUri.IndexOf("http://172.20.30.2:8080/Self/dashboard;jsessionid=") != -1)
            {
                ClassTable.Source = new Uri("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fcourseschedule&response_type=code");
                Eleinfo.Source = new Uri("https://api.m.dlut.edu.cn/login?client_id=19b32196decf419a&redirect_uri=https%3A%2F%2Fcard.m.dlut.edu.cn%2Fhomerj%2FopenRjOAuthPage&response_type=code&scope=base_api&state=weishao");
                WorkSpace_Web.Source = new Uri("https://sso.dlut.edu.cn/cas/login?service=https%3A%2F%2Fehall.dlut.edu.cn%2Ffp%2Fview%3Fm%3Dfp#act=fp/formHome");
                netstatusload();
            }
        }

        async Task WeatherHandle()
        {
            LogHelper.WriteInfoLog("天气页面优化");
            await WeatherBar.CoreWebView2.ExecuteScriptAsync("document.body.innerHTML=document.getElementsByClassName('myWeather')[0].outerHTML");
            await WeatherBar.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('myWeatherTop')[0].outerHTML=''");
            await WeatherBar.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('a')[0].outerHTML=\"\"");
            await WeatherBar.CoreWebView2.ExecuteScriptAsync("document.body.style=\"overflow:hidden;background-color:transparent\"");
            await WeatherBar.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('div')[0].style='background-color:transparent'");
            WeatherBar.Height = 680;
            WeatherCircle.Visibility = Visibility.Hidden;
        }

        private void ClassTable_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            ClassTable.CoreWebView2.Settings.AreHostObjectsAllowed = false;
            ClassTable.CoreWebView2.Settings.AreDevToolsEnabled = false;
            ClassTable.CoreWebView2.Settings.IsStatusBarEnabled = false;
            ClassTable.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            ClassTable.CoreWebView2.Settings.IsZoomControlEnabled = false;
            ClassTable.DefaultBackgroundColor = System.Drawing.Color.Transparent;
        }

        bool warnshown_2 = false;

        private void ClassTable_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            string un = Properties.Settings.Default.Uid;
            string upd = Properties.Settings.Default.UnionPassword;
            if (un.Length * upd.Length == 0)
            {
                if (warnshown_2 == false && (Overview.Visibility == Visibility.Visible))
                {
                    Growl.InfoGlobal("未配置信息，无法查询课表！");
                    warnshown_2 = true;
                }
                ClassTable.Source = new Uri("about:blank");
                return;
            }
            if (ClassTable.Source.AbsoluteUri.IndexOf("api") != -1)
            {
                apiloginforClassTable();
            }
            if (ClassTable.Source.AbsoluteUri == "https://lightapp.m.dlut.edu.cn/courseschedule")
            {
                resize();
            }
        }

        async Task apiloginforClassTable()
        {
            LogHelper.WriteInfoLog("执行主界面课表认证");
            string jscode = "username.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "password.value='" + Properties.Settings.Default.UnionPassword + "'";
            await ClassTable.CoreWebView2.ExecuteScriptAsync(jscode);
            await ClassTable.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jsenable = "btnpc.disabled=''";
            await ClassTable.CoreWebView2.ExecuteScriptAsync(jsenable);
            string jscode2 = "btnpc.click()";
            await ClassTable.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        async Task resize()
        {
            LogHelper.WriteInfoLog("课表页面优化");
            await Task.Delay(1000);
            await ClassTable.CoreWebView2.ExecuteScriptAsync("document.getElementsByTagName('div')[4].style=\"overflow:auto;\"");
            await ClassTable.CoreWebView2.ExecuteScriptAsync("document.body.style=\"background-color:transparent\"");
            //await FastClass.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('course')[0].innerHTML=document.getElementsByClassName('wrap')[0].outerHTML");
            await ClassTable.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('header')[0].outerHTML=\"\"");
            await ClassTable.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('bottom')[0].style=\"height: 650px; \"");
            await ClassTable.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('wrap')[0].style=\"background-color:transparent\"");
            ClassTable.Height = 600;
            TableCirlcle.Visibility = Visibility.Hidden;
        }

        private void Eleinfo_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            Eleinfo.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Linux; Android 10; EBG-AN00 Build/HUAWEIEBG-AN00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/83.0.4103.106 Mobile Safari/537.36 weishao(3.2.2.74616)";
        }
        bool warnshown = false;
        private void Eleinfo_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            string un = Properties.Settings.Default.Uid;
            string upd = Properties.Settings.Default.UnionPassword;
            if (un.Length * upd.Length == 0)
            {
                if (warnshown == false && (Overview.Visibility == Visibility.Visible || Electricity.Visibility == Visibility.Visible))
                {
                    Growl.InfoGlobal("未配置信息，无法查询电费！");
                    warnshown = true;
                }
                Eleinfo.Source = new Uri("about:blank");
                Overview_ElectricityInfo.Content = "未设置相关个人信息\n无法查询！";
                //Electricity_Info.Text = "未设置相关个人信息\n无法查询！";
            }
            if (int.Parse(DateTime.Now.Hour.ToString()) <= 1 || int.Parse(DateTime.Now.Hour.ToString()) >= 23)
            {
                Eleinfo.Dispose();
                Overview_ElectricityInfo.Content = "当前不在查询时间！";
                Electricity_ElectricityInfo.Content = "当前不在查询时间！";
                return;
            }
            if (Eleinfo.Source.AbsoluteUri.IndexOf("api") != -1)
            {
                apiloginforEleinfo();
            }
            if (Eleinfo.Source.AbsoluteUri.IndexOf("homerj") != -1 && Eleinfo.Source.AbsoluteUri.IndexOf("api") == -1)
            {
                Eleinfo.CoreWebView2.ExecuteScriptAsync("window.location.href='https://card.m.dlut.edu.cn/elepay/openElePay?openid='+openid[0].value+'&displayflag=1&id=30'");
                Overview_ElectricityInfo.Content = "玉兰卡信息初始化完成。。。";
                Electricity_ElectricityInfo.Content = "玉兰卡信息初始化完成。。。";
            }
            if (Eleinfo.Source.AbsoluteUri.IndexOf("openElePay") != -1)
            {
                Eleinfo.CoreWebView2.ExecuteScriptAsync(Properties.Resources.Send);
                Overview_ElectricityInfo.Content = "前置函数部署完成\n正在发起查询。。。。";
                Electricity_ElectricityInfo.Content = "前置函数部署完成\n正在发起查询。。。。";
                Eleinfo.CoreWebView2.ExecuteScriptAsync(Properties.Resources.Eleget);
            }
        }
        async Task apiloginforEleinfo()
        {
            LogHelper.WriteInfoLog("执行主界面电费认证");
            string jscode = "username.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "password.value='" + Properties.Settings.Default.UnionPassword + "'";
            await Eleinfo.CoreWebView2.ExecuteScriptAsync(jscode);
            await Eleinfo.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jsenable = "btnpc.disabled=''";
            await Eleinfo.CoreWebView2.ExecuteScriptAsync(jsenable);
            string jscode2 = "btnpc.click()";
            await Eleinfo.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        private void ClassTable_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.IndexOf("courseschedule") == -1)
            {
                ClassTable.Source = new Uri("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fcourseschedule&response_type=code");
                e.Cancel = true;
            }
        }

        private void Eleinfo_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string data = e.WebMessageAsJson.ToString().Split(new[] { "\"" }, StringSplitOptions.None)[1].Split(new[] { "：" }, StringSplitOptions.None)[1];
            if (data != "0.00")
            {
                Overview_ElectricityInfo.Content = "您寝室剩余电量为" + data + "度";
                Electricity_ElectricityInfo.Content = "您寝室剩余电量为" + data + "度";
                if (double.Parse(data) <= 10)
                {
                    Overview_ElectricityInfo.Content += "\n您剩余电量不足10度!\n请及时缴费,避免影响使用!";
                    Electricity_ElectricityInfo.Content += "\n您剩余电量不足10度!\n请及时缴费，避免影响正常使用！";
                }
                //Eleinfo.Dispose();
            }
            else
            {
                Overview_ElectricityInfo.Content = "服务器返回错误\n正在重新发起请求。。。";
                Electricity_ElectricityInfo.Content = "服务器返回错误\n正在重新发起请求。。。";
                Eleinfo.CoreWebView2.ExecuteScriptAsync(Properties.Resources.Eleget);
            }
        }

        //网络工具

        private void Network_Status_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            string dir = Environment.CurrentDirectory;
            string dir2 = dir + @"\\Binary\\PlainText\\monitor.html";
            new BrowserWindow(dir2, "", "", 0, 23, "校园网状态监控").ShowDialog();
            this.Show();
        }

        private void Network_SelfService_Click(object sender, RoutedEventArgs e)
        {
            if (PingIp("172.20.30.1"))
            {
                this.Hide();
                new BrowserWindow("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login%3Flogin_method%3D1", "", "", 0, 21, "开发区校园网自服务").ShowDialog();
                this.Show();
            }
            else
            {
                System.Windows.MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show("当前检测到可能无法连接至开发区校区认证服务器\n是否继续开启开发区校区自服务界面？点击否将会打开网络自助系统", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                if (messageBoxResult == System.Windows.MessageBoxResult.Yes)
                {
                    this.Hide();
                    new BrowserWindow("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login%3Flogin_method%3D1", "", "", 0, 21, "开发区校园网自服务").ShowDialog();
                    this.Show();
                }
                else
                {
                    Hide();
                    new BrowserWindow("https://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421e0f85388263c2654721d9de29d51367b3449/sso/sso_zzxt.jsp?filter=app&from=rj", "", "", 0, 15, "网络自助").ShowDialog();
                    Show();
                }
            }
        }

        private void Network_Charge_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://sso.dlut.edu.cn/cas/login?from=rj&service=https%3A%2F%2Fsso.dlut.edu.cn%2Fcas%2Flogin%3Fservice%3Dhttps%253A%252F%252Fehall.dlut.edu.cn%252Ffp%252FvisitService%253Fservice_id%253D2ac10b19-e8af-43a7-bded-a9da71ea31bc", "", "", 0, 22, "网费充值").ShowDialog();
            this.Show();
        }
        private void Network_RefreshDNS_Click(object sender, RoutedEventArgs e)
        {
            refreshdns();
        }
        async Task refreshdns()
        {
            await Task.Run(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.StandardInput.WriteLine("ipconfig /flushdns");
                p.StandardInput.WriteLine("exit");
                p.StandardInput.AutoFlush = true;
                string result = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                if (result.IndexOf("成功") != -1)
                {
                    Growl.SuccessGlobal("刷新成功！");
                }
                else
                {
                    Growl.InfoGlobal("刷新失败！\n" + result);
                }
            });
        }
        private void Network_LSPFix_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("26");
            Growl.SuccessGlobal("修复成功！");
        }

        bool ReConnect = false;
        private void Network_ManualConnect_Click(object sender, RoutedEventArgs e)
        {
            ReConnect = true;
            Growl.SuccessGlobal("登录请求已经发送！\n请等待。。。。");
            manualconnect();
        }

        async Task manualconnect()
        {
            using (WebClientPro client = new WebClientPro())
            {
                string result = client.DownloadString("http://172.20.30.1/drcom/chkstatus?callback=");
                string data = result.Split(new[] { "(" }, StringSplitOptions.None)[1].Split(new[] { ")" }, StringSplitOptions.None)[0];
                DrcomStatus drcomStatus = JsonConvert.DeserializeObject<DrcomStatus>(data);
                if (data.IndexOf("\"result\":1,") != -1)
                {
                    Growl.InfoGlobal("您已在线,无需登录!");
                }
                else
                {
                    WebView2 loginweb = new WebView2();
                    loginweb.CoreWebView2InitializationCompleted += (sender, args) =>
                    {
                        loginweb.NavigationCompleted += (sender1, args1) =>
                        {
                            if (loginweb.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login") != -1)
                            {
                                LogHelper.WriteDebugLog("执行sso登录注入");
                                string jscode = "un.value='" + Properties.Settings.Default.Uid + "'";
                                string jscode1 = "pd.value='" + Properties.Settings.Default.UnionPassword + "'";
                                string rm = "rememberName.checked='checked'";
                                loginweb.CoreWebView2.ExecuteScriptAsync(rm);
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode);
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode1);
                                string jscode2 = "login()";
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode2);
                            }
                            else if (loginweb.Source.AbsoluteUri.IndexOf("http://172.20.30.2:8080/Self/dashboard;jsessionid=") != -1)
                            {
                                Growl.SuccessGlobal("登陆成功！");
                                NetWork_NetworkInfo.Content = "正在加载信息。。。";
                                Overview_NetworkInfo.Content = "正在加载信息。。。";
                                netstatusload();
                                loginweb.CoreWebView2.CookieManager.DeleteAllCookies();
                            }
                        };
                        loginweb.Source = new Uri("http://172.20.20.1");
                    };
                    loginweb.EnsureCoreWebView2Async();
                }
            }
        }

        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.61 Safari/537.36";
                webReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LogHelper.WriteErrLog(ex);
            }
            return ret;
        }

        private void Network_ManualDisconnect_Click(object sender, RoutedEventArgs e)
        {
            Growl.SuccessGlobal("注销请求已经发送！\n请等待。。。。");
            using (WebClientPro client = new WebClientPro())
            {
                string result = client.DownloadString("http://172.20.30.1/drcom/chkstatus?callback=");
                string data = result.Split(new[] { "(" }, StringSplitOptions.None)[1].Split(new[] { ")" }, StringSplitOptions.None)[0];
                DrcomStatus drcomStatus = JsonConvert.DeserializeObject<DrcomStatus>(data);
                if (data.IndexOf("\"result\":1,") != -1)
                {
                    WebView2 logoutweb = new WebView2();
                    bool logoutshown = false;
                    logoutweb.CoreWebView2InitializationCompleted += (sender1, args) =>
                    {
                        logoutweb.NavigationCompleted += (sender2, args1) =>
                        {
                            Console.WriteLine(logoutweb.Source.AbsoluteUri.ToString());
                            logoutweb.ExecuteScriptAsync("user.unbind_mac(\"\", \"\", 1);");
                            netstatusload();
                            using (WebClientPro client1 = new WebClientPro())
                            {
                                string result1 = client1.DownloadString("http://172.20.30.1/drcom/chkstatus?callback=");
                                string data1 = result.Split(new[] { "(" }, StringSplitOptions.None)[1].Split(new[] { ")" }, StringSplitOptions.None)[0];
                                if (data.IndexOf("\"result\":1,") == -1)
                                {
                                    logoutweb.CoreWebView2.CookieManager.DeleteAllCookies();
                                    netstatusload();
                                    if(!logoutshown)
                                    {
                                        Growl.SuccessGlobal("注销成功");
                                        logoutshown= true;
                                    }
                                }
                            }

                        };
                        logoutweb.Source = new Uri("http://172.20.30.1/");
                    };
                    logoutweb.EnsureCoreWebView2Async();
                }
                else
                {
                    try
                    {
                        if (!PingIp("172.20.20.1"))
                        {
                            Growl.InfoGlobal("抱歉！\n暂不支持开发区校区之外的注销功能！");
                        }
                        else
                        {
                            Growl.InfoGlobal("你尚未连接到校园网，无法断开连接");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        LogHelper.WriteErrLog(ex);
                    }
                }
            }
        }

        private bool PingIp(string strIP)
        {
            bool bRet = false;
            try
            {
                Ping pingSend = new Ping();
                PingReply reply = pingSend.Send(strIP, 1000);
                if (reply.Status == IPStatus.Success)
                    bRet = true;
            }
            catch (Exception)
            {
                bRet = false;
            }
            return bRet;
        }

        private void Network_Modify_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("23");
            Growl.SuccessGlobal("优化成功！");
        }

        bool Refresh = false;

        private void Network_StatusRefresh_Click(object sender, RoutedEventArgs e)
        {
            if(PingIp("172.20.30.1"))
            {
                NetWork_NetworkInfo.Content = "正在加载信息。。。";
                Overview_NetworkInfo.Content = "正在加载信息。。。";
                Refresh = true;
                netstatusload();
            }
            else
            {
                Growl.InfoGlobal("似乎没有连接至校园网。。。");
            }
            
        }

        //日常缴费

        private void Electricity_Charge_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://sso.dlut.edu.cn/cas/login?from=rj&service=https%3A%2F%2Fsso.dlut.edu.cn%2Fcas%2Flogin%3Fservice%3Dhttps%253A%252F%252Fehall.dlut.edu.cn%252Ffp%252FvisitService%253Fservice_id%253Dca2b52e6-1145-4b63-9ea9-e443b376da0d", "", "", 0, 21, "电费充值").ShowDialog();
            this.Show();
        }

        private void ELectricity_Ecard_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("about:blank", "", "", 0, 24, "玉兰卡").ShowDialog();
            this.Show();
        }

        private void Electricity_Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(DateTime.Now.Hour.ToString()) <= 1 || int.Parse(DateTime.Now.Hour.ToString()) >= 23)
            {
                Eleinfo.Dispose();
                Overview_ElectricityInfo.Content = "当前不在查询时间！";
                Electricity_ElectricityInfo.Content = "当前不在查询时间！";
                return;
            }
            else
            {
                Eleinfo.Reload();
            }
        }

        //考试教务

        private void Exam_PublicNotice_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fnotice&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "公共通知（学院通知）").ShowDialog();
            this.Show();
        }

        private void Exam_Calender_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fschcalendar&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "学校校历").ShowDialog();
            this.Show();
        }

        private void Exam_LessionSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 7, "选课系统").ShowDialog();
            this.Show();
        }

        private void Exam_Info_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 3, "考试信息").ShowDialog();
            this.Show();
        }

        private void Exam_MainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 0, "教务主页").ShowDialog();
            this.Show();
        }

        private void Exam_Score_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 5, "成绩信息").ShowDialog();
            this.Show();
        }

        private void Exam_Delay_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 4, "缓考申请").ShowDialog();
            this.Show();
        }

        private void Exam_Evaluate_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 6, "评教系统-总结性评教").ShowDialog();
            this.Show();
        }

        private void Exam_Graduate_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/oauth/authorize?client_id=abccc53d6b78cb01&redirect_uri=http%3a%2f%2fdutgs.dlut.edu.cn%3a443%2fSmartWap%2foAuth.do&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "研究生系统").ShowDialog();
            this.Show();
        }

        //学习生活

        private void Study_WeekTable_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fcourseschedule&response_type=code", "", "", 0, 19, "本周课表").ShowDialog();
            this.Show();
        }

        private void Study_Table_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 8, "我的课表").ShowDialog();
            this.Show();
        }

        private void Study_ClassTable_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 9, "班级课表").ShowDialog();
            this.Show();
        }

        private void Study_Plan_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 2, "培养方案").ShowDialog();
            this.Show();
        }

        private void Study_Library_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://sso.dlut.edu.cn/cas/login?service=http://seat.lib.dlut.edu.cn/yanxiujian/client/login.php?redirect=areaSelectSeat.php", "", "", 0, 25, "图书馆选座").ShowDialog();
            this.Show();
        }

        private void Study_Notice_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2fmemo&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "校内通知").ShowDialog();
            this.Show();
        }

        private void Study_Lession_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421faef4690693464456a468ca88d1b203b/student/ucas-sso/login?filter=app&from=rj", "", "", 0, 1, "开课信息").ShowDialog();
            this.Show();
        }

        private void Study_Room_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2femptyclassroom&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "空闲教室列表").ShowDialog();
            this.Show();
        }

        private void Study_Report_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://ehall.dlut.edu.cn/fp/s/qyUXbf?from=rj", "", "", 0, 23, "外出报备").ShowDialog();
            this.Show();
        }

        //办事大厅

        CoreWebView2DownloadOperation downloadOperation;
        private void WorkSpace_Web_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            WorkSpace_Web.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;
            WorkSpace_Web.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindow;
        }

        private void CoreWebView2_NewWindow(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            WorkSpace_Web.CoreWebView2.ExecuteScriptAsync("window.location.href='" + e.Uri.ToString() + "'");
            e.Handled = true;
        }
        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDlg = new System.Windows.Forms.FolderBrowserDialog();
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
            downloadOperation = e.DownloadOperation; // Store the 'DownloadOperation' for later use in events
            downloadOperation.StateChanged += DownloadOperation_StateChanged;
            Growl.InfoGlobal("下载已启动！\n" + "文件名：" + filename + "\n储存路径：" + e.ResultFilePath.ToString() + "\n文件大小：" + formatdataflow(downloadOperation.TotalBytesToReceive.Value.ToString()) + "\n预计完成时间：" + downloadOperation.EstimatedEndTime.ToString());
        }

        private void DownloadOperation_StateChanged(object sender, object e)
        {
            if (downloadOperation.State.ToString() == "Completed")
            {
                string[] dataspace = downloadOperation.ResultFilePath.ToString().Split(new[] { "\\" }, StringSplitOptions.None);
                string filename = dataspace[dataspace.Length - 1];
                Growl.SuccessGlobal("文件" + filename + "已完成下载！\n" + "储存位置：" + downloadOperation.ResultFilePath.ToString() + "\n总下载大小:" + formatdataflow(downloadOperation.TotalBytesToReceive.Value.ToString()));
            }
        }

        private void WorkSpace_Web_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            AddressBox.Text = WorkSpace_Web.Source.OriginalString;
            if (WorkSpace_Web.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=") != -1)
            {
                login();
                return;
            }
            if (WorkSpace_Web.Source.AbsoluteUri.IndexOf("http://sso.dlut.edu.cn/cas/login?service=") != -1)
            {
                login();
                return;
            }
            if (WorkSpace_Web.Source.AbsoluteUri.IndexOf("https://api.m.dlut.edu.cn/") != -1)
            {
                apilogin();
            }
        }

        async Task apilogin()
        {
            LogHelper.WriteDebugLog("执行api登录注入");
            string jscode = "username.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "password.value='" + Properties.Settings.Default.UnionPassword + "'";
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode);
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jscode2 = "$(\"#formpc\").submit()";
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        async Task login()
        {
            string jscode = "un.value='" + Properties.Settings.Default.Uid + "'";
            string jscode1 = "pd.value='" + Properties.Settings.Default.UnionPassword + "'";
            string rm = "rememberName.checked='checked'";
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(rm);
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode);
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode1);
            string jscode2 = "login()";
            await WorkSpace_Web.CoreWebView2.ExecuteScriptAsync(jscode2);
        }

        private void WorkSpace_Web_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            //if (e.Uri.IndexOf("portal") != -1)
            //{
            //    e.Cancel = true;
            //    WorkSpace_Web.Source = new Uri("https://ehall.dlut.edu.cn/fp?from=rj");
            //}
            //if (e.Uri.IndexOf("ecard") != -1)
            //{
            //    e.Cancel = true;
            //    WorkSpace_Web.Source = new Uri("https://ehall.dlut.edu.cn/fp?from=rj");
            //}
        }

        //图书馆

        private void Library_Zhiwang_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421f5e7549e6933665b774687a98c/kns/brief/result.aspx?filter=app&from=rj", "", "", 0, 10, "知网搜索").ShowDialog();
            this.Show();
        }

        private void Library_WanFang_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421a1af13d27e6226022e5dc7fecc01/dljc/?filter=app&from=rj", "", "", 0, 11, "万方查重").ShowDialog();
            this.Show();
        }

        private void Library_Dupilicate_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421f7b9569d2936695e790c88b8991b203a18454272/index.html?filter=app&from=rj", "", "", 0, 12, "万方搜索").ShowDialog();
            this.Show();
        }

        private void Library_Resource_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421e7e056d22b396a1e7a049cb8d65027204e7199/sjkdhejlbyAtoZ/ALL.htm?filter=app&from=rj", "", "", 0, 13, "图书馆资源列表").ShowDialog();
            this.Show();
        }

        private void Library_Book_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://api.m.dlut.edu.cn/login?client_id=9qXqHnRQuhhViycC&redirect_uri=https%3a%2f%2flightapp.m.dlut.edu.cn%2fcheck%2flibrary&response_type=code&scope=base_api&state=dlut", "", "", 0, 23, "图书借阅").ShowDialog();
            this.Show();
        }

        private void Library_Record_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://sso.dlut.edu.cn/cas/login?service=http://seat.lib.dlut.edu.cn/yanxiujian/client/login.php?redirect=areaSelectSeat.php", "", "", 0, 26, "图书馆座位预约记录").ShowDialog();
            this.Show();
        }

        //其它系统

        private void Other_Pan_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421e0f64fd2233c7d44300d8db9d6562d/cas?filter=app&from=rj", "", "", 0, 17, "大工网盘").ShowDialog();
            this.Show();
        }

        private void Other_Mail_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new BrowserWindow("https://mail.dlut.edu.cn/", "", "", 0, 18, "大工邮箱").ShowDialog();
            Show();
        }

        private void Other_Work_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f4e2558f267e6c5c6b1cc7a99c406d36b7/cas?filter=app&from=rj", "", "", 0, 16, "学工系统").ShowDialog();
            Show();
        }

        private void Other_Mooc_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f9fa4e93247e6c5c6b1cc7a99c406d3642/sso/dlut?filter=app&from=rj", "", "", 0, 15, "大工慕课").ShowDialog();
            Show();
        }

        private void Other_Charge_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f5f4408e23206949730d87b8d6512f209640763a21f75b0c/?filter=app&from=rj", "", "", 0, 20, "校园缴费平台").ShowDialog();
            Show();
        }

        private void Other_Network_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/https/77726476706e69737468656265737421e0f85388263c2654721d9de29d51367b3449/sso/sso_zzxt.jsp?filter=app&from=rj", "", "", 0, 14, "网络自助").ShowDialog();
            Show();
        }

        private void Other_Market_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://res.dlut.edu.cn/tp_cgp/view?m=cgp", "", "", 0, 28, "跳蚤市场").ShowDialog();
            this.Show();
        }

        private void Other_Return_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://ehall.dlut.edu.cn/fp/s/dcLKox?from=rj", "", "", 0, 23, "返校申请").ShowDialog();
            this.Show();
        }

        private void Other_Forum_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://webvpn.dlut.edu.cn/http/77726476706e69737468656265737421f2f552cd693464456a468ca88d1b203b/?filter=app&from=rj", "", "", 0, 27, "青年之家").ShowDialog();
            this.Show();
        }

        private void Other_DUTer_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new BrowserWindow("https://ehall.dlut.edu.cn/fp/s/QZMKMV?from=rj", "", "", 0, 23, "DUTer建言").ShowDialog();
            this.Show();
        }

        //系统工具

        private void System_Panel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        bool PatchInitialized = false;

        private void MeltdownPatchSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (PatchInitialized == true)
            {
                SendMessageToCore("12");
                kbbanreloader();
            }
        }

        private void MeltdownPatchSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PatchInitialized == true)
            {
                SendMessageToCore("12");
                kbbanreloader();
            }
        }

        async Task kbbanloader()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hkmm = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", false);
            if (hkmm.GetValue("FeatureSettingsOverride") == null)
            {
                MeltdownPatchSwitch.IsChecked = true;
            }
            else
            {
                if (hkmm.GetValue("FeatureSettingsOverride").ToString() == "0")
                {
                    MeltdownPatchSwitch.IsChecked = true;
                }
                else
                {
                    MeltdownPatchSwitch.IsChecked = false;
                }
            }
            PatchInitialized = true;
        }
        async Task kbbanreloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hkmm = hklm.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management", false);
                if (hkmm.GetValue("FeatureSettingsOverride") == null)
                {
                    Growl.SuccessGlobal("补丁启用成功!\n重启计算机生效！");
                }
                else
                {
                    if (hkmm.GetValue("FeatureSettingsOverride").ToString() == "0")
                    {
                        Growl.SuccessGlobal("补丁启用成功!\n重启计算机生效！");
                    }
                    else
                    {
                        Growl.SuccessGlobal("补丁禁用成功!\n重启计算机生效！");
                    }
                }
            });
        }
        bool ShipInitialized = false;

        private void ReserveShipping_Checked(object sender, RoutedEventArgs e)
        {
            if (ShipInitialized == true)
            {
                SendMessageToCore("11");
                spacebanreloader();
            }
        }

        private void ReserveShipping_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ShipInitialized == true)
            {
                SendMessageToCore("11");
                spacebanreloader();
            }
        }

        async Task spacebanloader()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hkrm = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ReserveManager", false);
            if (hkrm.GetValue("ShippedWithReserves").ToString() == "1")
            {
                ReserveShipping.IsChecked = true;
            }
            else
            {
                ReserveShipping.IsChecked = false;
            }
            ShipInitialized = true;
        }

        async Task spacebanreloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hkrm = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ReserveManager", false);
                if (hkrm.GetValue("ShippedWithReserves").ToString() == "1")
                {
                    Growl.SuccessGlobal("预留空间启用成功！\n重启计算机生效！");
                }
                else
                {
                    Growl.SuccessGlobal("预留空间禁用成功！\n重启计算机生效！");
                }
            });
        }

        bool TSXInitialized = false;

        private void TSX_Checked(object sender, RoutedEventArgs e)
        {
            if (TSXInitialized == true)
            {
                SendMessageToCore("14");
                TSXreloader();
            }
        }

        private void TSX_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TSXInitialized == true)
            {
                SendMessageToCore("14");
                TSXreloader();
            }
        }
        async Task TSXloader()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hksk = hklm.OpenSubKey(@"System\CurrentControlSet\Control\Session Manager\kernel", false);
            if (hksk.GetValue("DisableTsx") == null)
            {
                TSX.IsChecked = false;
            }
            else
            {
                if (hksk.GetValue("DisableTsx").ToString() == "1")
                {
                    TSX.IsChecked = false;
                }
                else
                {
                    TSX.IsChecked = true;
                }
            }
            TSXInitialized = true;
        }

        async Task TSXreloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hksk = hklm.OpenSubKey(@"System\CurrentControlSet\Control\Session Manager\kernel", false);
                if (hksk.GetValue("DisableTsx") == null)
                {
                    Growl.SuccessGlobal("TSX禁用成功\n重启计算机生效！");
                }
                else
                {
                    if (hksk.GetValue("DisableTsx").ToString() == "1")
                    {
                        Growl.SuccessGlobal("TSX禁用成功\n重启计算机生效！");
                    }
                    else
                    {
                        Growl.SuccessGlobal("TSX启用成功\n重启计算机生效！");
                    }
                }

            });
        }


        bool VBSInitialized = false;

        private void VBS_Checked(object sender, RoutedEventArgs e)
        {
            if (VBSInitialized == true)
            {
                SendMessageToCore("13");
                VBSreloader();
            }
        }


        private void VBS_Unchecked(object sender, RoutedEventArgs e)
        {
            if (VBSInitialized == true)
            {
                SendMessageToCore("13");
                VBSreloader();
            }
        }
        async Task VBSloader()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hkcd = hklm.OpenSubKey(@"System\CurrentControlSet\Control\DeviceGuard", false);
            if (hkcd == null)
            {
                VBS.IsChecked = true;
            }
            else
            {
                if (hkcd.GetValue("EnableVirtualizationBasedSecurity") == null)
                {
                    VBS.IsChecked = true;
                }
                else
                {
                    if (hkcd.GetValue("EnableVirtualizationBasedSecurity").ToString() == "1")
                    {
                        VBS.IsChecked = true;
                    }
                    else
                    {
                        VBS.IsChecked = false;
                    }
                }
            }
            VBSInitialized = true;
        }


        async Task VBSreloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hkcd = hklm.OpenSubKey(@"System\CurrentControlSet\Control\DeviceGuard", false);
                if (hkcd == null)
                {
                    Growl.SuccessGlobal("启用基于虚拟化的安全性成功！\n重启计算机生效！");
                }
                else
                {
                    if (hkcd.GetValue("EnableVirtualizationBasedSecurity") == null | hkcd.GetValue("EnableVirtualizationBasedSecurity").ToString() == "1")
                    {
                        Growl.SuccessGlobal("启用基于虚拟化的安全性成功！\n重启计算机生效！");
                    }
                    else
                    {
                        Growl.SuccessGlobal("禁用基于虚拟化的安全性成功！\n重启计算机生效！");
                    }
                }
            });
        }


        bool TimeLineInitialized = false;
        private void TimeLine_Checked(object sender, RoutedEventArgs e)
        {
            if (TimeLineInitialized == true)
            {
                SendMessageToCore("15");
                TimeLinereloader();
            }
        }

        private void TimeLine_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TimeLineInitialized == true)
            {
                SendMessageToCore("15");
                TimeLinereloader();
            }
        }
        async Task TimeLineloader()
        {
            RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey hksk = hklm.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", false);
            if (hksk.GetValue("EnableActivityFeed") == null)
            {
                TimeLine.IsChecked = true;
            }
            else
            {
                if (hksk.GetValue("EnableActivityFeed").ToString() == "1")
                {
                    TimeLine.IsChecked = true;
                }
                else
                {
                    TimeLine.IsChecked = false;
                }
            }
            TimeLineInitialized = true;
        }


        async Task TimeLinereloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey hksk = hklm.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Windows\System", false);
                if (hksk.GetValue("EnableActivityFeed") == null)
                {
                    Growl.SuccessGlobal("TimeLine启用成功！");
                }
                else
                {
                    if (hksk.GetValue("EnableActivityFeed").ToString() == "1")
                    {
                        Growl.SuccessGlobal("TimeLine启用成功！");
                    }
                    else
                    {
                        Growl.SuccessGlobal("TimeLine禁用成功！");
                    }
                }

            });
        }
        private void Setup_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\SystemPropertiesAdvanced.exe");
        }

        private void Performance_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\SystemPropertiesPerformance.exe");
        }

        private void BrightnessFix_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("17");
            Growl.SuccessGlobal("修复成功！");
        }

        private void SysFix_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("22");
            Growl.SuccessGlobal("进程已启动");
        }

        private void QQClean_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("21");
            CleanWaiter();
        }

        async Task CleanWaiter()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                while (Process.GetProcessesByName("ToolBox.User.Core").Length != 0)
                {

                }
                Growl.SuccessGlobal("清理QQ缓存成功！");
            });
        }

        private void CPan_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\windows\\System32\\cleanmgr.exe", "");
        }

        private void Spacesniffer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dir1 = System.Windows.Forms.Application.StartupPath;
                string dir3 = "\"" + dir1 + "\\ThirdParty\\SpaceSniffer\\SpaceSniffer.exe" + "\"";
                Process.Start(dir3);
            }
            catch (Exception ee)
            {
                Growl.FatalGlobal(ee.Message);
            }
        }

        private void DiskManager_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\diskmgmt.msc");
        }

        private void Backuper_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("C:\\Windows\\System32\\SystemPropertiesProtection.exe");
        }

        private void CCleaner_Click(object sender, RoutedEventArgs e)
        {
            string dir1 = System.Windows.Forms.Application.StartupPath;
            string dir3 = "\"" + dir1 + "\\ThirdParty\\CCleaner\\CCleaner64.exe" + "\"";
            Process.Start(dir3);
        }

        bool OneDriveInitialized = false;

        private void OneDrive_Checked(object sender, RoutedEventArgs e)
        {
            if (OneDriveInitialized == true)
            {
                SendMessageToCore("16");
                onedrivereloader();
            }
        }

        private void OneDrive_Unchecked(object sender, RoutedEventArgs e)
        {
            if (OneDriveInitialized == true)
            {
                SendMessageToCore("16");
                onedrivereloader();
            }
        }

        async Task onedriveloader()
        {
            RegistryKey hkcr = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
            RegistryKey hkod = hkcr.OpenSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", false);
            if (hkod == null)
            {
                OneDrive.IsEnabled = false;
                return;
            }
            if (hkod.OpenSubKey(@"\ShellFolder") == null)
            {
                OneDrive.IsChecked = false;
            }
            else
            {
                OneDrive.IsChecked = true;
            }
            OneDriveInitialized = true;
        }
        async Task onedrivereloader()
        {
            Task.Run(async () =>
            {
                while (Process.GetProcessesByName("ToolBox.Core").Length == 0)
                {

                }
                await Task.Delay(1000);
                RegistryKey hkcr = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Registry64);
                RegistryKey hkod = hkcr.OpenSubKey(@"CLSID\{018D5C66-4533-4307-9B53-224DE2ED1FE6}", false);
                if (hkod == null)
                {
                    Growl.FatalGlobal("未安装Onedrive");
                    return;
                }
                if (hkod.OpenSubKey(@"\ShellFolder") == null)
                {
                    Growl.SuccessGlobal("侧边栏关闭成功！");
                }
                else
                {
                    Growl.SuccessGlobal("侧边栏开启成功！");
                }
            });
        }

        private void FolderBackground_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new SmallPanelWindow(1).ShowDialog();
            this.Show();
        }

        private void ResetScreenShot_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("24");
            Growl.SuccessGlobal("截图序号重置成功！");
        }

        private void DesktopIcon_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\rundll32.exe", "shell32.dll,Control_RunDLL desk.cpl,,0");
        }

        private void RestartExplorer_Click(object sender, RoutedEventArgs e)
        {
            SendMessageToCore("18");
            Growl.SuccessGlobal("文件资源管理器重启成功！");
        }

        private void Adobe_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new SmallPanelWindow(0).ShowDialog();
            this.Show();
        }

        bool HighResolutionLoaded = false;

        private void HighResolution_Click(object sender, RoutedEventArgs e)
        {
            if (HighResolutionLoaded == true)
            {
                SendMessageToCore("25");
                HighResolutionReloader();
            }
        }

        private void HighResolutionLoader()
        {
            RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey hkcd = hkcu.OpenSubKey(@"Control Panel\Desktop", false);
            if (hkcd.GetValue("JPEGImportQuality") == null)
            {
                HighResolution.IsChecked = false;
            }
            else
            {
                if (hkcd.GetValue("JPEGImportQuality").ToString() == "100")
                {
                    HighResolution.IsChecked = true;
                }
                else
                {
                    HighResolution.IsChecked = false;
                }
            }
            HighResolutionLoaded = true;
        }
        private void HighResolutionReloader()
        {
            RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey hkcd = hkcu.OpenSubKey(@"Control Panel\Desktop", false);
            if (hkcd.GetValue("JPEGImportQuality") == null)
            {
                Growl.SuccessGlobal("已启用桌面高清壁纸！");
            }
            else
            {
                if (hkcd.GetValue("JPEGImportQuality").ToString() == "100")
                {
                    Growl.SuccessGlobal("已禁用桌面高清壁纸！");
                }
                else
                {
                    Growl.SuccessGlobal("已启用桌面高清壁纸！");
                }
            }
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace_Web.GoForward();
        }

        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            WorkSpace_Web.GoBack();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AddressBox.Width = this.Width - 470;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.Width - 470 > 0)
            {
                AddressBox.Width = this.Width - 470;
            }
        }
    }
}

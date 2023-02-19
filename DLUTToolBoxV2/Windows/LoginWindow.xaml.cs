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
using System.Diagnostics;
using System.Threading;
using HandyControl.Controls;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;
using DLUTToolBox_V2.Helper;
using Newtonsoft.Json;
using Microsoft.Web.WebView2.Wpf;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : System.Windows.Window
    {
        int count = 0;
        string un;
        string pd;
        string html = "";
        public LoginWindow()
        {
            LogHelper.WriteInfoLog("登录窗口已打开");
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            un = Properties.Settings.Default.Uid;
            pd = Properties.Settings.Default.NetworkPassword;
            LoginProcedureStarter();
            //TODO:想个合适的方法把凌水校区登录加进来
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
            }
            return ret;
        }
        async Task LoginProcedureStarter()
        {
                if(!PreProcedureNeteorkCheck())
                {
                    if(PingIp("172.20.30.1"))
                    {
                        //Console.WriteLine("EDA");
                        //EDALoginProcedure();
                    }
                    else
                    {
                        System.Windows.MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show("当前检测到可能无法连接至开发区校区认证服务器\n是否执行主校区网络登录程序？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information, MessageBoxResult.No);
                        if (messageBoxResult == System.Windows.MessageBoxResult.Yes)
                        {
                            LingShuiLoginProcedure();
                        }
                        else
                        {
                            //EDALoginProcedure();
                        }
                    }
                }
        }

        string info;
        bool PreProcedureNeteorkCheck()
        {
            PostHide(3000);
            string checkcommand = "curl --connect-timeout 3 https://www.baidu.com";
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.RedirectStandardInput = true;
            p1.StartInfo.RedirectStandardOutput = true;
            p1.StartInfo.RedirectStandardError = true;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.StandardInput.WriteLine(checkcommand);
            p1.StandardInput.WriteLine("exit");
            p1.StandardInput.AutoFlush = true;
            string strre = p1.StandardOutput.ReadToEnd();
            p1.WaitForExit();
            p1.Close();
            LogHelper.WriteDebugLog(strre);
            if (strre.Length > 1000)
            {
                LogHelper.WriteInfoLog("网络已连接无需登录");
                loadinfo();
                Growl.SuccessGlobal("校园网已经登入！\n" + info);
                PostExit(8000);
                return true;
            }
            else
            {
                return false;
            }
        }

        void EDALoginProcedure()
        {
            LoginHandler_EDA();
        }

        async Task PostExit(int time)
        {

            await Task.Delay(time);
            await Task.Run(() =>
            {
                LogHelper.WriteInfoLog("-----------------------------------------程序退出-----------------------------------------");
                Environment.Exit(0);
            });
        }
        async Task PostHide(int time)
        {
            await Task.Delay(time);
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Visibility = Visibility.Hidden;
            });
        }

        string formatdataflow_2(long num)
        {
            double temp = num;
            string re = "";
            if (temp > 1073741824)
            {
                temp /= (double)(1024 * 1024);
                re = temp.ToString() + "G";
            }
            else if (temp > 1024)
            {
                temp /= (double)(1024);
                re = temp.ToString() + "M";
            }
            else
            {
                re = temp + "KB";
            }
            return re;
        }

        void loadinfo()
        {
            if (PingIp("172.20.30.1"))
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
                            info = "校园网余额：" + fee + "\n校园网已用流量：\n" + flowused ;
                            if (drcomStatus.flow > 96636764160)
                            {
                                info += "\n|本月流量使用已超过90G，请留意！！|\n";
                            }
                        }
                    }
                    catch (System.Net.WebException e)
                    {
                        Growl.WarningGlobal("无法加载校园网信息，五秒超时已到");
                    }
                }
            }
            else
            {
                info = "";
            }

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
        void checkerror()//检查无法连接原因:eg.服务器连接超时、欠费、账号或密码错误。。。
        {
            if (PingIp("172.20.30.1") == false)
            {
                Growl.FatalGlobal("认证服务器无法连接\n请检查网线是否插紧或WIFI是否连接正常");
                Thread.Sleep(100000);
            }
            else
            {
                    Growl.FatalGlobal("⚠⚠连接失败⚠⚠\n" + html);
            }
        }

        void LoginHandler_EDA()
        {
            
            string[] Paths = new string[2];
            if (File.Exists(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config")))
            {
                int i = 0;
                using (StreamReader sr = new StreamReader(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config")))
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
                un = Paths[0];
                pd= Paths[1];
            }
            else
            {
                Growl.FatalGlobal("配置文件丢失!\n" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config"));
                Thread.Sleep(3000);
                Growl.ClearGlobal();
                return;
            }
            string Uid = Paths[0];
            string NetworkPassword = Paths[1];
            using (WebClientPro client = new WebClientPro())
            {
                string result = client.DownloadString("http://172.20.30.1/drcom/chkstatus?callback=");
                string data = result.Split(new[] { "(" }, StringSplitOptions.None)[1].Split(new[] { ")" }, StringSplitOptions.None)[0];
                DrcomStatus drcomStatus = JsonConvert.DeserializeObject<DrcomStatus>(data);
                Console.WriteLine(data);
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
                            Console.WriteLine(loginweb.Source.AbsoluteUri);
                            if (loginweb.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login") != -1)
                            {
                                LogHelper.WriteDebugLog("执行sso登录注入");
                                string jscode = "un.value='" + Uid + "'";
                                string jscode1 = "pd.value='" + NetworkPassword + "'";
                                string rm = "rememberName.checked='checked'";
                                loginweb.CoreWebView2.ExecuteScriptAsync(rm);
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode);
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode1);
                                string jscode2 = "login()";
                                loginweb.CoreWebView2.ExecuteScriptAsync(jscode2);
                            }
                            else if (loginweb.Source.AbsoluteUri.IndexOf("http://172.20.30.2:8080/Self/dashboard;jsessionid=") != -1)
                            {
                                loginweb.CoreWebView2.CookieManager.DeleteAllCookies();
                                LogHelper.WriteInfoLog("登陆成功！");
                                NetworkStateChecker_EDA();
                            }
                        };
                        loginweb.Source = new Uri("http://172.20.20.1");
                    };
                    loginweb.EnsureCoreWebView2Async();
                }
            }
        }

        void NetworkStateChecker_EDA()//检查联网状态
        {
            string strre = "";
            string checkcommand = "curl --connect-timeout 3 https://www.baidu.com";
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.RedirectStandardInput = true;
            p1.StartInfo.RedirectStandardOutput = true;
            p1.StartInfo.RedirectStandardError = true;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.StandardInput.WriteLine(checkcommand);
            p1.StandardInput.WriteLine("exit");
            p1.StandardInput.AutoFlush = true;
            strre = p1.StandardOutput.ReadToEnd();
            p1.WaitForExit();
            p1.Close();
            if (strre.Length > 1000)
            {
                loadinfo();
                Growl.SuccessGlobal("连接成功\n您已正常连接互联网\n" + info + "\n登录模块自动退出");
                PostExit(8000);
            }
            else
            {
                count++;
                if (count <5)
                {
                    Growl.InfoGlobal("⚠⚠连接失败⚠⚠\n"+info+"\n剩余尝试次数"+(5-count)+"次\n连接冷却：4秒");
                    Thread.Sleep(4000);
                    EDALoginProcedure();
                }
                else if (count == 5)
                {
                    Growl.InfoGlobal("正在尝试查找登陆问题。。。");
                    checkerror();
                }
            }
        }

        void LingShuiLoginProcedure()
        {
            LoginHandler_LingShui();
            NetworkStateChecker_LingShui();
            PostExit(4000);
        }

        void LoginHandler_LingShui()
        {
            string re = "";
            string command = "curl  -d \"userId=" + un + "password=" + pd + "\" \"http://auth.dlut.edu.cn/eportal/InterFace.do?method=login\"";
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.RedirectStandardInput = true;
            p1.StartInfo.RedirectStandardOutput = true;
            p1.StartInfo.RedirectStandardError = true;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.StandardInput.WriteLine(command);
            p1.StandardInput.WriteLine("exit");
            p1.StandardInput.AutoFlush = true;
            re = p1.StandardOutput.ReadToEnd();
            p1.WaitForExit();
            p1.Close();
        }

        void NetworkStateChecker_LingShui()//检查联网状态
        {
            string strre = "";
            string checkcommand = "curl --connect-timeout 3 https://www.baidu.com";
            Process p1 = new Process();
            p1.StartInfo.FileName = "cmd.exe";
            p1.StartInfo.UseShellExecute = false;
            p1.StartInfo.RedirectStandardInput = true;
            p1.StartInfo.RedirectStandardOutput = true;
            p1.StartInfo.RedirectStandardError = true;
            p1.StartInfo.CreateNoWindow = true;
            p1.Start();
            p1.StandardInput.WriteLine(checkcommand);
            p1.StandardInput.WriteLine("exit");
            p1.StandardInput.AutoFlush = true;
            strre = p1.StandardOutput.ReadToEnd();
            p1.WaitForExit();
            p1.Close();
            if (strre.Length > 1000)
            {
                loadinfo();
                ThisWindow.Visibility = Visibility.Hidden;
                Growl.SuccessGlobal("连接成功\n您已正常连接互联网\n" + info + "\n登录模块自动退出");
                PostExit(6000);
            }
            else
            {
                count++;
                if (count == 3)
                {
                    Growl.FatalGlobal("⚠⚠连接失败⚠⚠\n即将自动重试，最大次数6次\n连接冷却：4秒");
                }
                else if (count == 10)
                {
                    Growl.FatalGlobal("⚠⚠连接失败⚠⚠\n已达到最大重试次数\n程序将尝试检查错误原因");
                    checkerror_LS();
                    return;
                }
                Thread.Sleep(4000);
                LingShuiLoginProcedure();
            }
        }

        void checkerror_LS()
        {
            Growl.FatalGlobal("⚠⚠连接失败⚠⚠");
            PostExit(4000);
        }

        private void WebView2_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            if (EDALoginWeb.Source.AbsoluteUri.IndexOf("https://sso.dlut.edu.cn/cas/login?service=http%3A%2F%2F172.20.30.2%3A8080%2FSelf%2Fsso_login") != -1)
            {

                string[] Paths = new string[2];
                if (File.Exists(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config")))
                {
                    int i = 0;
                    using (StreamReader sr = new StreamReader(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config")))
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
                    un = Paths[0];
                    pd = Paths[1];
                }
                else
                {
                    Growl.FatalGlobal("配置文件丢失!\n" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Replace("DLUTToolBoxV2.exe", "Network.config"));
                    Thread.Sleep(3000);
                    Growl.ClearGlobal();
                    Growl.InfoGlobal("配置文件加载成功！");
                    return;
                }
                string Uid = Paths[0];
                string NetworkPassword = Paths[1];
                LogHelper.WriteDebugLog("执行sso登录注入");
                string jscode = "un.value='" + Uid + "'";
                string jscode1 = "pd.value='" + NetworkPassword + "'";
                string rm = "rememberName.checked='checked'";
                EDALoginWeb.CoreWebView2.ExecuteScriptAsync(rm);
                EDALoginWeb.CoreWebView2.ExecuteScriptAsync(jscode);
                EDALoginWeb.CoreWebView2.ExecuteScriptAsync(jscode1);
                string jscode2 = "login()";
                EDALoginWeb.CoreWebView2.ExecuteScriptAsync(jscode2);
            }
            else if (EDALoginWeb.Source.AbsoluteUri.IndexOf("http://172.20.30.2:8080/Self/dashboard;jsessionid=") != -1)
            {
                EDALoginWeb.CoreWebView2.CookieManager.DeleteAllCookies();
                loadinfo();
                Growl.SuccessGlobal("连接成功\n" + info + "\n登录模块自动退出");
                PostExit(8000);
            }
        }
    }
}

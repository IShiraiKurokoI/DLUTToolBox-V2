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
            Task.Run(() =>
            {
                if(!PreProcedureNeteorkCheck())
                {
                    if(PingIp("172.20.20.1"))
                    {
                        EDALoginProcedure();
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
                            EDALoginProcedure();
                        }
                    }
                }
            });
        }

        string info;
        bool PreProcedureNeteorkCheck()
        {
            string checkcommand = "curl --connect-timeout 3 39.156.66.18";
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
            NetworkStateChecker_EDA();
            PostExit(8000);
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

        void loadinfo()
        {
            if (PingIp("172.20.20.1"))
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("curl -d \"action=get_online_info\" \"http://172.20.20.1:801/include/auth_action.php\" & exit");
                //http://172.20.20.1:801/include/auth_action.php?action=get_online_info
                p.StandardInput.AutoFlush = true;
                string strre2 = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
                string data_all = strre2.Split(new[] { "& exit" }, StringSplitOptions.None)[1];
                string[] data = data_all.Split(new[] { "," }, StringSplitOptions.None);
                if (data.Length > 2)
                {
                    info = "校园网余额:" + data[2] + "\n校园网已用流量:\n" + formatdataflow(data[0]) + "\nIPV4地址:\n" + data[5] + "\n网卡MAC地址:\n" + data[3];
                    LogHelper.WriteInfoLog("登陆成功"+info);
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
            if (PingIp("172.20.20.1") == false)
            {
                Growl.FatalGlobal("认证服务器无法连接\n请检查网线是否插紧或WIFI是否连接正常");
                Thread.Sleep(100000);
            }
            else
            {
                if (html.IndexOf("please") == -1 && html.IndexOf("nline") == -1)
                {
                    Growl.FatalGlobal("⚠⚠连接失败⚠⚠\n" + html);
                }
                else
                {
                    loadinfo();
                    Growl.SuccessGlobal("虽然但是，连接成功了。。。\n您已正常访问互联网\n" + info + "\n登录模块自动退出");
                }
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
            string command = "action=login&ac_id=3&user_ip=&nas_ip=&user_mac=&url=&username=" + Uid + "&password=" + NetworkPassword + "&save_me=1";
            string re = PostWebRequest("http://172.20.20.1:801/srun_portal_pc.php?ac_id=3&", command, Encoding.ASCII);
            try
            {
                re = re.Split(new string[] { Properties.Resources.Split }, StringSplitOptions.None)[1].Split(new string[] { "</td>" }, StringSplitOptions.None)[0];
                Console.WriteLine(re);
                html = re;
                if (re.IndexOf("please") == -1&&re.IndexOf("nline")==-1)
                {
                    Growl.InfoGlobal(re);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void NetworkStateChecker_EDA()//检查联网状态
        {
            string strre = "";
            string command = "curl -s  --connect-timeout 10 --header 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9'--compressed--header 'Accept-Language: en-US,en;q=0.9'--header 'Cache-Control: max-age=0'--header 'Connection: keep-alive'--header 'Origin: http://172.20.20.1:801'--header 'Referer: http://172.20.20.1:801/srun_portal_pc.php?ac_id=3&'--header 'Upgrade-Insecure-Requests: 1'--user-agent 'Mozilla/5.0 (Windows; U; Windows NT 4.0) AppleWebKit/533.43.4 (KHTML, like Gecko) Version/4.0.5 Safari/533.43.4'--data-binary 'action=login&ac_id=3&user_ip=&nas_ip=&user_mac=&url=&username=" + un + "&password=" + pd + "' 'http://172.20.20.1:801/srun_portal_pc.php?ac_id=3&'";
            string checkcommand = "curl --connect-timeout 3 39.156.66.18";
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
                PostExit(6000);
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
            string command = "curl -s  --connect-timeout 10 --header 'Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9'--compressed--header 'Accept-Language: en-US,en;q=0.9'--header 'Cache-Control: max-age=0'--header 'Connection: keep-alive'--header 'Origin: http://172.20.20.1:801'--header 'Referer: http://172.20.20.1:801/srun_portal_pc.php?ac_id=3&'--header 'Upgrade-Insecure-Requests: 1'--user-agent 'Mozilla/5.0 (Windows; U; Windows NT 4.0) AppleWebKit/533.43.4 (KHTML, like Gecko) Version/4.0.5 Safari/533.43.4'--data-binary 'action=login&ac_id=3&user_ip=&nas_ip=&user_mac=&url=&username=" + un + "&password=" + pd + "' 'http://172.20.20.1:801/srun_portal_pc.php?ac_id=3&'";
            string checkcommand = "curl --connect-timeout 3 39.156.66.18";
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
    }
}

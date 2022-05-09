using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO.Pipes;
using HandyControl.Controls;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // 接收参数数组
            var args = e.Args;
            if(args.Length!=0)
            {
                if (args[0] == ("login"))
                {
                    new LoginWindow().Show();
                }
                if (args[0] == ("restart"))
                {
                    new SmallPanelWindow(2).Show();
                }
            }
            else
            {
                Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--proxy-server=\"direct://\"");
                new MainWindow().Show();
            }
        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName("ToolBox.Core").ToList().Count > 0)
            {
                NamedPipeClientStream PipeSender = new NamedPipeClientStream("localhost", "ToolBoxCorePipe", PipeDirection.Out, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
                StreamWriter sw = null;
                PipeSender.Connect(5000);
                sw = new StreamWriter(PipeSender);
                sw.AutoFlush = true;
                sw.WriteLine("Exit");
            }
            //如果存在工具箱后台主进程，则关闭
            Growl.ClearGlobal();
            //清空growl消息
            //TODO:写入日志
            base.OnExit(e);
        }
    }
}

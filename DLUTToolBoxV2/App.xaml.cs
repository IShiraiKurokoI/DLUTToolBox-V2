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
            LogHelper.InitLog4Net();
            RegisterEvents();
            LogHelper.WriteInfoLog("----------------------------------程序启动，开始初始化----------------------------------");
            LogHelper.WriteInfoLog("日志初始化成功！");
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
                LogHelper.WriteInfoLog("初始化WebView参数");
                Environment.SetEnvironmentVariable("WEBVIEW2_ADDITIONAL_BROWSER_ARGUMENTS", "--proxy-server=\"direct://\"");
                LogHelper.WriteInfoLog("程序初始化完成，打开主窗口");
                new MainWindow().Show();
            }
        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName("ToolBox.Core").ToList().Count > 0)
            {
                LogHelper.WriteInfoLog("尝试关闭后台进程");
                NamedPipeClientStream PipeSender = new NamedPipeClientStream("localhost", "ToolBoxCorePipe", PipeDirection.Out, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
                StreamWriter sw = null;
                PipeSender.Connect(5000);
                sw = new StreamWriter(PipeSender);
                sw.AutoFlush = true;
                sw.WriteLine("Exit");
            }
            //如果存在工具箱后台主进程，则关闭
            Growl.ClearGlobal();
            LogHelper.WriteInfoLog("清空Growl消息");
            //清空growl消息
            LogHelper.WriteInfoLog("-----------------------------------------程序退出-----------------------------------------");
            base.OnExit(e);
        }

        private void RegisterEvents()
        {
            //Task线程内未捕获异常处理事件
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //UI线程未捕获异常处理事件（UI主线程）
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        //Task线程内未捕获异常处理事件
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            try
            {
                if (e.Exception is Exception exception)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.SetObserved();
            }
        }

        //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionObject is Exception exception)
                {
                    HandleException(exception);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        //UI线程未捕获异常处理事件（UI主线程）
        private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                HandleException(e.Exception);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                e.Handled = true;
            }
        }

        //日志记录
        private static void HandleException(Exception ex)
        {
            //记录日志
            LogHelper.WriteErrLog(ex);
        }
    }
}

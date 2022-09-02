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
using System.Threading;
using System.Runtime.InteropServices;

namespace DLUTToolBox_V2
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LogHelper.InitNlog();
            RegisterEvents();
            LogHelper.WriteInfoLog("----------------------------------程序启动，开始初始化----------------------------------");
            LogHelper.WriteInfoLog("日志初始化成功！");
            DeleteFile(System.Environment.CurrentDirectory + @"\\Log\\", 3);
            base.OnStartup(e);
            // 接收参数数组
            var args = e.Args;
            if(args.Length!=0)
            {
                if (args[0] == ("login"))
                {
                    using (Mutex mutex = new Mutex(false, "DLUTToolBoxV2"))
                    {
                        if (mutex.WaitOne(0, false))
                        {
                            new LoginWindow().ShowDialog(); //conduct the login procedure
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
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

        private void DeleteFile(string fileDirect, int saveDay)
        {
            try
            {
                DateTime nowTime = DateTime.Now;
                string[] files = Directory.GetFiles(fileDirect, "*.log", SearchOption.AllDirectories);  //获取该目录下所有 .txt文件
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TimeSpan t = DateTime.Now - fileInfo.CreationTime;  //当前时间  减去 文件创建时间
                    int day = t.Days;
                    if (day > saveDay)   //保存的时间，单位：天
                    {
                        if (IsOccupy(fileInfo.FullName)) //判断文件是否被占用
                        {
                            System.IO.File.Delete(fileInfo.FullName); //删除文件
                        }
                        else
                        {
                            LogHelper.WriteErrLog(new Exception("文件被占用，无法操作!"));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                LogHelper.WriteErrLog(err);
                throw;
            }

        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        /// <summary>
        /// 判断文件是否被占用
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool IsOccupy(string file)
        {
            bool result = true; //默认状态此文件未被占用
            try
            {
                //string vFileName = @"c:\temp\temp.bmp";
                string vFileName = file;
                if (!System.IO.File.Exists(vFileName))
                {
                    //Logger.Info("文件都不存在!");
                    result = false;
                }
                IntPtr vHandle = _lopen(vFileName, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    result = false;
                }
                CloseHandle(vHandle);
            }
            catch (Exception err)
            {
                result = false;
                LogHelper.WriteErrLog(err);
            }
            return result;
        }

    }
}

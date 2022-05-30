using System;
using System.IO;

namespace DLUTToolBox_V2
{
    public class LogHelper
    {
        public static NLog.Logger logger;
        public static void InitLog4Net()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public static void WriteInfoLog(string info)
        {
            logger.Info(info);
        }

        public static void WriteErrLog(Exception ex)
        {
            logger.Error(ex);
        }
        public static void WriteDebugLog(string info)
        {
            logger.Debug(info);
        }
        public static void WriteWarnLog(string info)
        {
            logger.Warn(info);
        }
    }
}

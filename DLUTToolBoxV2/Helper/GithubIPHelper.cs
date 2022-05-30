using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace DLUTToolBox_V2
{
    internal class GithubIPHelper
    {
        
        public static string GetGithubIP()
        {
            var ips = GetAppSettingsValue("ips");
            string Ip = GetFastIPDomain(ips);
            if(Ip!=null)
            {
                return Ip;
            }
            else
            {
                return null;
            }
        }

        public static string GetFastIPDomain(string ips, string domain = "github.com")
        {
            Dictionary<string, long> dic = new Dictionary<string, long>();
            foreach (var ip in ips.Split(','))
            {
                var time = PingIp(ip);
                if (time > 0)
                {
                    dic.Add(ip, time);
                }
            }

            if (dic.Count > 0)
            {
                var result = dic.OrderBy(c => c.Value).First().Key;
                return result;
            }
            else
            {
                return null;
            }
        }

        public static string GetAppSettingsValue(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        /// <summary>
        /// ping ip,测试能否ping通
        /// </summary>
        /// <param name="strIP">IP地址</param>
        /// <returns></returns>
        private static long PingIp(string strIP, int timeOut = 1000)
        {
            long bRet = 0;
            try
            {
                Ping pingSend = new Ping();
                PingReply reply = pingSend.Send(strIP, timeOut);
                if (reply.Status == IPStatus.Success)
                    bRet = reply.RoundtripTime;
            }
            catch (Exception)
            {
                bRet = 0;
            }
            return bRet;
        }
    }
}

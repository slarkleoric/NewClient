using ArcFace.Core.Helper;
using ArcFace.Core.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ArcFace.Core
{
    public static class Const
    {
        #region 程序版本信息
        public const string Version = "1.0.0";
        public const string AppName = "aiface";
        #endregion
      
        #region 数据库信息
        /// <summary> 
        /// 数据库版本
        /// </summary>
        internal static int DbVersion = 13;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        internal static string ConnectionString =
            "Data Source={0};Pooling=true;FailIfMissing=false;Version=3;UTF8Encoding=True;Journal Mode=Off;";

        /// <summary> 
        /// admin数据库密码
        /// </summary>
        internal static string AdminDbPassword = "admin123";
        /// <summary>
        /// admin数据库文件名
        /// </summary>
        internal const string AdminDbName = AppName + "_admin";

        /// <summary> 
        /// data数据库密码
        /// </summary>
        internal static string MeetingDbPassword = "data123";
        /// <summary>
        /// data数据库文件名
        /// </summary>
        internal const string MeetingDbName = AppName + "_data";
        
        /// <summary>
        /// 获取数据库路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string DbPath(this string name)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(DbPathFormat, name));
        }
        /// <summary>
        /// 数据库存放路径
        /// </summary>
        private const string DbPathFormat = "Contents\\db\\{0}.db";
      
        #endregion

        /// <summary>
        /// 日期格式
        /// </summary>
        internal const string DateFormatString = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 日志帮助实例
        /// </summary>
        public static ILogger DefaultLogger => LogManager.Logger(AppName);

        /// <summary>
        /// api_host地址
        /// </summary>
        internal static string Host => ConfigHelper.Read<string>("api_host");

        /// <summary>
        /// 内容格式字典
        /// </summary>
        internal static readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            {"*", "application/octet-stream"},
            {".doc", "application/msword"},
            {".ico", "image/x-icon"},
            {".gif", "image/gif"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".png", "image/x-png"},
            {".mp3", "audio/mpeg"},
            {".mpeg", "audio/mpeg"},
            {".flv", "video/x-flv"},
            {".mp4", "application/octet-stream"},
        };

        /// <summary> 获得当前应用软件的版本 </summary>
        public static Version CurrentVersion
        {
            get
            {
                var location = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).Location;
                if (location == null)
                    return new Version();
                var version = FileVersionInfo.GetVersionInfo(location).ProductVersion;
                return !string.IsNullOrWhiteSpace(version) ? new Version(version) : new Version();
            }
        }

        /// <summary> 是否64位系统 </summary>
        public static bool Is64Bit => !Environment.Is64BitProcess;

        #region 网络连接状态

        [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary> 
        /// 是否有网络链接
        /// </summary>
        public static bool IsConnected()
        {
            try
            {
                int connection;
                if (!InternetGetConnectedState(out connection, 0))
                    return false;
#if DEBUG
                const int INTERNET_CONNECTION_MODEM = 1;

                const int INTERNET_CONNECTION_LAN = 2;

                const int INTERNET_CONNECTION_PROXY = 4;

                const int INTERNET_CONNECTION_MODEM_BUSY = 8;

                var netstatus = string.Empty;
                if ((connection & INTERNET_CONNECTION_PROXY) != 0)
                    netstatus += " 采用代理上网  \n";
                if ((connection & INTERNET_CONNECTION_MODEM) != 0)
                    netstatus += " 采用调治解调器上网 \n";
                if ((connection & INTERNET_CONNECTION_LAN) != 0)
                    netstatus += " 采用网卡上网  \n";
                if ((connection & INTERNET_CONNECTION_MODEM_BUSY) != 0)
                    netstatus += " MODEM被其他非INTERNET连接占用  \n";
                Console.WriteLine(netstatus);
#endif
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 
        #endregion

        /// <summary>
        /// 客户端名称
        /// </summary>
        public static string UserAgent
        {
            get
            {
                var agent = new StringBuilder();
                agent.AppendFormat("ArcFace/{0}/{1}/{2}", CurrentVersion, Environment.OSVersion,
                    Environment.MachineName);
                return agent.ToString();
            }
        }
    }
}

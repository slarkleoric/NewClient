
using ArcFace.Core.DbHelper;
using ArcFace.Core.Dtos;
using ArcFace.Core.Helper;
using ArcFace.Core.Logging;
using ArcFaceClient.Commands;
using ArcFaceClient.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace ArcFaceClient
{
    public partial class App
    {

        /// <summary>
        /// 账号信息
        /// </summary>
        public static UserInfoDto CurrentUser;



        private readonly ILogger _logger = LogManager.Logger<App>();

        [DllImport("user32")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            var currentProcess = Process.GetCurrentProcess();
            var process = Process.GetProcesses()
                .FirstOrDefault(t => t.ProcessName == "aiface" && t.Id != currentProcess.Id);
            if (process != null)
            {
                //选中当前的句柄窗口
                SetForegroundWindow(process.MainWindowHandle);
                Current.Shutdown();
                return;
            }
            //全局异常处理
            DispatcherUnhandledException += (sender, ev) =>
            {
                _logger.Error(ev.Exception.Message, ev.Exception);
                ev.Handled = false;
            };
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            ConfigHelper.InitConfig(@"Contents/config/application.xml");
            AutoCreateTableHelper.InitializeDb();

            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            new LoginView().Show();
            base.OnStartup(e);
        }

        private static void CheckVersion(object args)
        {
            LocalSysCmds.CheckVersionCommand.Execute(args);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ConfigHelper.Dispose();
            base.OnExit(e);
        }

       
    }
}

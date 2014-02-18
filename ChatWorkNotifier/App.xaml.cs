using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ChatworkNotifier
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private NotifyIconWrapper notifyIconWrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            notifyIconWrapper = new NotifyIconWrapper();

            notifyIconWrapper.notifyIcon.BalloonTipClicked += notifyIcon_BalloonTipClicked;

            var bt = new BackgroundTask(notifyIconWrapper);
            bt.Setup();
            bt.StartThreadMain();
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.chatwork.com/");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            notifyIconWrapper.Dispose();
        }

    }
}

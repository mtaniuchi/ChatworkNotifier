using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chatwork.Service;

namespace ChatworkNotifier
{
    public class BackgroundTask
    {
        private NotifyIconWrapper notifyIconWrapper;
        private TimerCallback timerCallback;
        private string APIKey = string.Empty;

        public BackgroundTask(NotifyIconWrapper notifyIconWrapper)
        {
            this.notifyIconWrapper = notifyIconWrapper;
            timerCallback = new TimerCallback(ThreadMain);
        }

        public void Setup()
        {
            APIKey = ChatWorkNotifier.Properties.Settings.Default.APIKey;
            if (string.IsNullOrWhiteSpace(APIKey))
            {
                this.notifyIconWrapper.notifyIcon.ShowBalloonTip(
                    5,
                    @"ChatworkNotifier",
                    @"APIKEYは必須です",
                    System.Windows.Forms.ToolTipIcon.Info);
            }
        }

        public void StartThreadMain(int intervalSec = 30)
        {
            var timer = new Timer(timerCallback, null, 1000, intervalSec * 1000);
        }

        private void ThreadMain(object state)
        {
            Console.WriteLine("{0}:{1}", APIKey, DateTime.Now);

            try
            {
                var client = new ChatworkClient(APIKey);
                var status = client.My.GetStatusAsync().Result;
                Console.WriteLine("{0}, {1}, {2}", status.unread_num, status.mention_num, status.mytask_num);

                if (status.unread_num == 0 && status.mention_num == 0) return;

                this.notifyIconWrapper.notifyIcon.ShowBalloonTip(
                    5,
                    @"ChatworkNotifier",
                    string.Format("未読：{0} あなた宛：{1} タスク数：{2}", status.unread_num, status.mention_num, status.mytask_num),
                    System.Windows.Forms.ToolTipIcon.Info);
            }
            catch (Exception ex)
            {
                this.notifyIconWrapper.notifyIcon.ShowBalloonTip(
                    5,
                    @"ChatworkNotifier",
                    ex.Message,
                    System.Windows.Forms.ToolTipIcon.Info);
            }
        }
    }
}

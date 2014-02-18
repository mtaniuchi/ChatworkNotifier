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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace ChatworkNotifier
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // クローズ処理をキャンセルして、タスクバーの表示も消す
            e.Cancel = true;
            this.WindowState = System.Windows.WindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtAPIKey.Text = ChatWorkNotifier.Properties.Settings.Default.APIKey;
        }

        private void 適用_Click(object sender, RoutedEventArgs e)
        {
            ChatWorkNotifier.Properties.Settings.Default.APIKey = txtAPIKey.Text;
            ChatWorkNotifier.Properties.Settings.Default.Save();
            ChatWorkNotifier.Properties.Settings.Default.Reload();

            this.WindowState = System.Windows.WindowState.Minimized;
            this.ShowInTaskbar = false;
        }
    }
}

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Rug.Osc;

namespace OscDemo.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        // タイマのインスタンス
        private DispatcherTimer _timer;

        //OSCのレシーバー
        private OscReceiver _OscReceiver;

        //OSC受信待ちをするタスク
        private Task _task = null;

        public MainWindow()
        {
            Console.WriteLine("Wpf start.");
            InitializeComponent();

            viewModel = new MainWindowViewModel()
            {
                OscText = "aaaaa"
            };
            this.DataContext = viewModel;

            //_OscReceiver = new OscReceiver(2345);
            //_OscReceiver.Connect();
            //_task = new Task(() => OscListenProcess());
            //_task.Start();

            
            
            
        }

        private void SendFunction (object sender, RoutedEventArgs e)
        {

            // 送信先はローカルホスト
            var address = IPAddress.Parse("127.0.0.1");
            
            // 送信先のポートを指定
            using var oscSender = new OscSender(address, 2345);
            var msg = "from Wpf.";
            // 接続
            oscSender.Connect();
            oscSender.Send(new OscMessage("/test", msg));
            
            viewModel.OscText = "asdfg";
            viewModel.BgColor = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0xFF, 0x00));
        }


        private void ConnectFunction (object sender, RoutedEventArgs e)
        {
            var thread = new Thread(new ThreadStart(Connect));
            thread.Start();
        }

        private void Connect()
        {
            using var oscReceiver = new OscReceiver(2345);

            //// 接続処理
            oscReceiver.Connect();

            // 無限ループにして受信待ち
            while (true)
            {
                var oscPacket = oscReceiver.Receive();
                viewModel.OscText = oscPacket.ToString();
            }
        }

        /// <summary>
        /// OSC受信をListenする処理
        /// Taskで実行されており、繰り返し受信を確認し
        /// 接続が終了したら処理を終わらせる
        /// </summary>
        private void OscListenProcess()
        {
            //using var oscServer = new OscReceiver(2345);

            //// 接続処理
            //oscServer.Connect();

            // 無限ループにして受信待ち
            while (true)
            {
                var oscPacket = _OscReceiver.Receive();
                viewModel.OscText = oscPacket.ToString();
                Console.WriteLine(oscPacket.ToString());
            }
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            _OscReceiver.Close();
            _timer.Stop();
        }
    }
}

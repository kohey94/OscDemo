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

        private OscReceiver oscReceiver;

        private Thread thread;

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
            Console.WriteLine("送信処理");
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
            Console.WriteLine("接続処理");
            
            oscReceiver = new OscReceiver(2345);

            // Create a thread to do the listening
            thread = new Thread(new ThreadStart(ListenLoop));

            // Connect the receiver
            oscReceiver.Connect();

            // Start the listen thread
            thread.Start();
        }

        private void ListenLoop()
        {
            Console.WriteLine("ListenLoop");
            try
            {
                while (oscReceiver.State != OscSocketState.Closed)
                {
                    Console.WriteLine("wait...");
                    // if we are in a state to recieve
                    if (oscReceiver.State == OscSocketState.Connected)
                    {
                        // get the next message 
                        // this will block until one arrives or the socket is closed
                        OscPacket packet = oscReceiver.Receive();

                        // Write the packet to the console 
                        Console.WriteLine(packet.ToString());

                        // DO SOMETHING HERE!
                        viewModel.OscText = packet.ToString();
                    }
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                // if the socket was connected when this happens
                // then tell the user
                if (oscReceiver.State == OscSocketState.Connected)
                {
                    Console.WriteLine("Exception in listen loop");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            oscReceiver.Close();
            thread.Join();
        }
    }
}

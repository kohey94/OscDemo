using System;
using System.Net;
using Rug.Osc;

namespace OscDemo.Client
{
    class OscClient
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("OSC Client.");
            Console.WriteLine("周波数[Hz] 再生時間[msec]");


            // 送信先はローカルホスト
            var address = IPAddress.Parse("127.0.0.1");
            
            // 送信先のポートを指定
            using var oscSender = new OscSender(address, 2345);
            
            // 接続
            oscSender.Connect();

            //// 無限ループにしてメッセージ送信
            //while (true)
            //{
            //    var msg = Console.ReadLine();
            //    oscSender.Send(new OscMessage("/test", msg.ToString()));
            //}
            while (true)
            {
                var msg = Console.ReadLine().Split(" ");
                if (int.TryParse(msg[0], out int freq) && int.TryParse(msg[1], out int time))
                {
                    //Console.Beep(freq, time);
                    oscSender.Send(new OscMessage("/beep", freq, time));
                }
            }

        }
    }
}

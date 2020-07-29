using System;
using Rug.Osc;

namespace OscDemo.Server
{
    class OscServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OSC Server.");

            // 12345ポートでOscレシーバーを作成
            using var oscReceiver = new OscReceiver(2345);

            // 接続処理
            oscReceiver.Connect();

            // 無限ループにして受信待ち
            while(true)
            {
                var oscPacket = oscReceiver.Receive();
                var msg = oscPacket.ToString();
                Console.WriteLine(msg);
                var f_t = msg.Split(" ");
                Console.WriteLine($"{f_t[0]} {f_t[1]} {f_t[2]}");
                if (int.TryParse(f_t[1], out int freq) && int.TryParse(f_t[2], out int time))
                    Console.Beep(freq, time);
            }
        }
    }
}

using System;
using Rug.Osc;

namespace OscDemo.Server
{
    class OscServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OSC Server,");

            // 12345ポートでOscレシーバーを作成
            using OscReceiver oscServer = new OscReceiver(12345);
            
            // 接続処理
            oscServer.Connect();

            // 無限ループにして受信待ち
            while(true)
            {
                OscPacket oscPacket = oscServer.Receive();
                Console.WriteLine(oscPacket.ToString());
            }
        }
    }
}

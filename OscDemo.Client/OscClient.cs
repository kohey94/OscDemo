﻿using System;
using System.Net;
using Rug.Osc;

namespace OscDemo.Client
{
    class OscClient
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OSC Client.");

            // 送信先はローカルホスト
            var address = IPAddress.Parse("127.0.0.1");
            
            // 送信先のポートを指定
            using var oscSender = new OscSender(address, 2345);
            
            // 接続
            oscSender.Connect();
            
            // 無限ループにしてメッセージ送信
            while (true)
            {
                var msg = Console.ReadLine();

                oscSender.Send(new OscMessage("/test", msg.ToString()));
            }
        }
    }
}

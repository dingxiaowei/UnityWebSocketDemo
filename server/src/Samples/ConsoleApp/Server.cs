using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Fleck.Samples.ConsoleApp
{
    class Server
    {
        static void Main()
        {
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:8081");
            server.Start(socket =>
            {
                var id = socket.ConnectionInfo.Id;
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    Console.WriteLine(id + ": Connected");
                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                    Console.WriteLine(id + ": Closed");
                };
                socket.OnBinary = bytes =>
                {
                    Console.WriteLine(id + ": Received: bytes(" + bytes.Length + ")");
                    socket.Send(bytes);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(id + ": Received: " + message + "");
                    socket.Send(message);
                };
                socket.OnError = error =>
                {
                    Console.WriteLine(socket.ConnectionInfo.Id + ": " + error.Message);
                };
            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }
        }
    }
}

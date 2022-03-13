using Google.Protobuf;
using HelixSdk.Hnet;
using HelixSdk.Hnet.Wire;
using HolaUtilLib;
using System;
using System.Collections.Generic;
using System.Net;
using pb = global::Google.Protobuf;

namespace CorePlay
{
    public class WSSocketConnection
    {
        protected IPEndPoint mEndPoint;
        /// <summary>
        /// 服务器连接地址 ws://127.0.0.1:8800/stream/grid/12121
        /// </summary>
        protected string mWSRemotePoint;
        protected int mPort;
        protected System.TimeSpan mTimeOut;
        protected System.Action<bool> mOnConnected;
        protected ChannelBase<NetMessage> conn;
        protected Dictionary<string, string> mHeaders;
        protected string mServer = "1001";
        public bool isConnected { get { return conn != null ? conn.Active : false; } }
        public WSSocketConnection(string wsRemotePoint, string server, int port, System.TimeSpan timeout, Dictionary<string, string> connectHeaders, Action<bool> connectEvent)
        {
            mWSRemotePoint = wsRemotePoint;
            mPort = port;
            mTimeOut = timeout;
            mOnConnected = connectEvent;
            mHeaders = connectHeaders;
            if (!string.IsNullOrEmpty(server))
                mServer = server;
            CreateConnect();
        }

        private void CreateConnect()
        {
            ClientManager<NetMessage> client = new WebSocketClientManager();
            client.ConnectedHandler += OnConected;
            client.MsgHandler += OnThirdHandler;
            client.HandshakeDoneHandler += OnHandshakeDone;
            client.ShutdownHandler += OnShutdown;

            ChannelOpts opts = new ChannelOpts();
            opts.Headers = mHeaders;
            opts.ReadTimeout = mTimeOut;
            opts.WriteTimeout = mTimeOut;
            opts.KeepAliveInterval = mTimeOut;
            conn = client.DialTimeout(string.Format("foo:{0}", UnityEngine.Random.Range(0, 10000)), ServiceType.Tcp, opts);
            if (conn == null)
                throw new NetException("conn err!");

            conn.Connect(mWSRemotePoint, mPort, mTimeOut);
        }

        public void SendPBMessage(int msgId, pb::IMessage message)
        {
            if (!isConnected)
            {
                return;
            }
            //TODO:待使用MsgPool
            NetMessage msg = new NetMessage();
            msg.Type = msgId;
            msg.Xid = mServer;
            msg.Content = message.ToByteString();
            conn.Send(msg);
        }

        public void Update()
        {
            conn?.Update();
        }

        public void DisConnect()
        {
            conn?.Close();
        }

        void OnConected(object sender, EventArgsBase args)
        {
            ConnectedEventArgs<NetMessage> status = args as ConnectedEventArgs<NetMessage>;
            if (status.IsConnected)
            {
                HolaLog.Log("ws连接成功");
                mOnConnected?.Invoke(true);
            }
            else
            {
                HolaLog.Log("ws连接失败");
                mOnConnected?.Invoke(false);
            }
        }

        void OnThirdHandler(object sender, EventArgsBase args)
        {
            DispatchMsgEventArgs<NetMessage> msgArgs = args as DispatchMsgEventArgs<NetMessage>;
            NetMessage msg = msgArgs.Message;
            HolaLog.Log($"接收到服务器消息:{msg.Type}");
            HolaLog.Log($"onThirdHandler msgContent={msg.Content.ToStringUtf8()}");

            MessageDispatcher.singleton.ProcessMsg(msg);
        }

        //握手完成事件
        void OnHandshakeDone(object sender, EventArgsBase args)
        {
            HolaLog.Log("握手完成返回事件");
        }

        void OnShutdown(object sender, EventArgsBase args)
        {
            HolaLog.Log("OnShutDownHandler");
        }
    }
}

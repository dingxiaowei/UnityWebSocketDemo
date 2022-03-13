using HolaUtilLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using pb = global::Google.Protobuf;

namespace CorePlay
{
    public class WSSocketSession
    {
        protected WSSocketConnection mConnection;
        public bool IsConnectServer { get { return mConnection != null ? mConnection.isConnected : false; } }
        protected MessageDispatcher mDispatcher = new MessageDispatcher();

        private float _heartTimeStart;
        private float _heartIntervalTime = 3.0f;
        public WSSocketSession(string addr, string server, int port, System.TimeSpan timeout, Dictionary<string, string> connectHeaders, System.Action<bool> connectEvent)
        {
            mConnection = new WSSocketConnection(addr, server, port, timeout, connectHeaders, connectEvent);
        }
        public void Disconnect()
        {
            mConnection?.DisConnect();
        }

        public void Update()
        {
            if (mConnection != null && mConnection.isConnected)
            {
                try
                {
                    mConnection.Update();
                    if (Time.realtimeSinceStartup - _heartTimeStart >= _heartIntervalTime)
                    {
                        _heartTimeStart += _heartIntervalTime;
                        SendPing();
                    }
                }
                catch (Exception e)
                {
                    if (!mConnection.isConnected)
                    {
                        //返回
                        HolaLog.Log("[Update]" + e.Message);
                    }
                }
            }
        }

        public void SendPBMessage(int msgId, pb::IMessage message)
        {
            mConnection?.SendPBMessage(msgId, message);
        }

        public void Send<T>(pb::IMessage message) where T : pb::IMessage
        {
            Type t = typeof(T);
            int msgId = NetMessageIdList.TypeToMsgId(t);
            SendPBMessage(msgId, message);
        }

        public void RegisterOnMessageReceived<T>(Action<pb::IMessage> handler) where T : pb::IMessage
        {
            mDispatcher.RegisterOnMessageReceived<T>(handler);
        }

        public void UnRegisterOnMessageReceived<T>(Action<pb::IMessage> handler) where T : pb::IMessage
        {
            mDispatcher.UnRegisterOnMessageReceived<T>(handler);
        }

        void SendPing()
        {
            //Send((int)HereMessageID.KEEP_ALIVE, "");//TODO:发送心跳是否可以发空消息
        }
    }
}

using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WSSocketSession
{
    protected WSSocketConnection mConnection;
    protected readonly MessageDispatcher mDispatcher = MessageDispatcher.sInstance;

    public bool IsConnected { get { return mConnection.IsConnect; } }

    public WSSocketSession(string serverUrl, string userIdStr, Dictionary<string, string> headers, Action<bool> onConnectedCallBack)
    {
        mConnection = new WSSocketConnection(serverUrl, userIdStr, headers, onConnectedCallBack);
    }

    public void Connect()
    {
        mConnection?.CreateConnect();
    }

    public void Send(int msgId, IMessage message)
    {
        mConnection?.Send(msgId, message);
    }

    public void Send<T>(IMessage message) where T : IMessage
    {
        var t = typeof(T);
        int msgId = NetMessageIdList.TypeToMsgId(t);
        mConnection?.Send(msgId, message);
#if DEBUG_NETWORK
        UnityEngine.Debug.Log("Send message : " + msgId.ToString() + "_" + NetMessageIdList.MsgIdToType(msgId));
#endif
    }
    public void RegisterOnMessageReceived<T>(Action<object> handler) where T : IMessage
    {
        mDispatcher?.RegisterOnMessageReceived<T>(handler);
    }

    public void UnregisterOnMessageReceived<T>(Action<object> handler) where T : IMessage
    {
        mDispatcher?.UnregisterOnMessageReceived<T>(handler);
    }


    public void Disconnect()
    {
        mConnection?.DisConnect();
        DisposeOnDisconnected();
    }

    public void Update()
    {
        if (IsConnected)
        {
            mDispatcher?.DispatchMessages();
        }
    }

    protected void DisposeOnDisconnected()
    {

    }
}

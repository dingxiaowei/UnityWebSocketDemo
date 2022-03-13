using Google.Protobuf;
using Protoc;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityWebSocket;

public class WSSocketConnection
{
    private string mServerUrl;
    private string mServerIdStr = "1001";
    private Dictionary<string, string> mHeaders;
    private IWebSocket mSocket;
    private Action<bool> mOnConnected;
    public bool IsConnect { get { return mSocket != null ? mSocket.ReadyState == WebSocketState.Open : false; } }
    public WSSocketConnection(string serverUrl, string serverIdStr, Dictionary<string, string> headers, Action<bool> onConnectedCallBack)
    {
        mServerUrl = serverUrl;
        mServerIdStr = serverIdStr;
        mHeaders = headers;
        mOnConnected = onConnectedCallBack;
    }

    public void Send(int msgId, IMessage message)
    {
        if (mSocket.ReadyState == WebSocketState.Open)
        {
            var msg = new NetMessage(); //TODO:使用对象池
            msg.Type = msgId;
            msg.Xid = mServerIdStr;
            msg.Oid = "";
            msg.Content = message.ToByteString();
            //ProtobufHelper.ToBytes(msg);
            mSocket.SendAsync(msg.ToByteArray());
        }
        else
        {
#if DEBUG_NETWORK
            Debug.LogError($"消息:{msgId}发送失败,当前网络连接状态:{mSocket.ReadyState}");
#endif
        }
    }

    public void CreateConnect()
    {
        mSocket = new WebSocket(mServerUrl);
        mSocket.OnOpen += Socket_OnOpen;
        mSocket.OnMessage += Socket_OnMessage;
        mSocket.OnClose += Socket_OnClose;
        mSocket.OnError += Socket_OnError;
#if DEBUG_NETWORK
        Debug.Log(string.Format("Connecting...\n"));
#endif
        mSocket.ConnectAsync(mHeaders);
    }

    private void Socket_OnOpen(object sender, OpenEventArgs e)
    {
#if DEBUG_NETWORK
        Debug.Log(string.Format("Connected: {0}\n", mServerUrl));
#endif
        if (mSocket.ReadyState == WebSocketState.Open)
        {
            mOnConnected?.Invoke(true);
        }
        else
        {
            mOnConnected?.Invoke(false);
        }
    }

    private void Socket_OnMessage(object sender, MessageEventArgs e)
    {
        if (e.IsBinary)
        {
#if DEBUG_NETWORK
            Debug.Log(string.Format("Receive Bytes ({1}): {0}\n", e.Data, e.RawData.Length));
#endif
            var netMsg = NetMessage.Parser.ParseFrom(e.RawData);
#if DEBUG_NETWORK
            Debug.Log($"Receive Msg ID:{netMsg.Type} xid:{netMsg.Xid}");
#endif
            MessageDispatcher.sInstance.ReceiveMessage(netMsg);
        }
        else if (e.IsText)
        {
#if DEBUG_NETWORK
            Debug.Log(string.Format("Receive Text: {0}\n", e.Data));
#endif
        }
    }

    private void Socket_OnClose(object sender, CloseEventArgs e)
    {
#if DEBUG_NETWORK
        Debug.Log(string.Format("WebSocket Closed: StatusCode: {0}, Reason: {1}\n", e.StatusCode, e.Reason));
#endif
    }

    private void Socket_OnError(object sender, ErrorEventArgs e)
    {
#if DEBUG_NETWORK
        Debug.LogError(string.Format("WebSocket Error: {0}\n", e.Message));
#endif
    }

    public void DisConnect()
    {
        if (mSocket != null && mSocket.ReadyState != WebSocketState.Closed)
        {
            mSocket.CloseAsync();
        }
    }
}

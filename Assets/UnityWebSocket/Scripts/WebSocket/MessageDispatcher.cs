﻿using Google.Protobuf;
using Protoc;
using System;
using System.Collections.Generic;

public class MessageDispatcher : Singleton<MessageDispatcher>
{
    protected const int MAX_DISPATCH_MESSAGE_COUNT_PER_FRAME = 16;
    protected Dictionary<int, Action<object>> mMessageHandlers = new Dictionary<int, Action<object>>();
    protected Queue<NetMessage> mMessageFrontQueue = new Queue<NetMessage>();
    protected Queue<NetMessage> mMessageBackQueue = new Queue<NetMessage>();

    public void RegisterOnMessageReceived<T>(Action<object> handler) where T : IMessage
    {
        var msgId = NetMessageIdList.TypeToMsgId(typeof(T));
        if (msgId != 0)
        {
            RegisterHandler(msgId, handler);
        }
    }

    public void UnregisterOnMessageReceived<T>(Action<object> handler) where T : IMessage
    {
        var msgId = NetMessageIdList.TypeToMsgId(typeof(T));
        if (msgId != 0)
        {
            UnregisterHandler(msgId, handler);
        }
    }

    public void ReceiveMessage(NetMessage packet)
    {
        var msgId = packet.Type;
        if (msgId != 0)
        {
            lock (mMessageBackQueue)
            {
                mMessageBackQueue.Enqueue(packet);
            }
        }
    }

    public void DispatchMessages()
    {
        int dispatchedMessageCount = 0;
        while (mMessageFrontQueue.Count > 0)
        {
            NetMessage packet = mMessageFrontQueue.Dequeue();
            DispatchMessage(packet);
            dispatchedMessageCount++;

            if (dispatchedMessageCount >= MAX_DISPATCH_MESSAGE_COUNT_PER_FRAME)
            {
                return;
            }
        }

        //swap messageQueue
        lock (mMessageBackQueue)
        {
            Queue<NetMessage> temp = mMessageBackQueue;
            mMessageBackQueue = mMessageFrontQueue;
            mMessageFrontQueue = temp;
        }
    }

    protected void RegisterHandler(int msgId, Action<object> handler)
    {
        if (handler != null)
        {
            if (mMessageHandlers.ContainsKey(msgId))
            {
                mMessageHandlers[msgId] -= handler;
                mMessageHandlers[msgId] += handler;
            }
            else
            {
                mMessageHandlers.Add(msgId, handler);
            }
        }
    }

    protected void UnregisterHandler(int msgId, Action<object> handler)
    {
        if (handler != null)
        {
            if (mMessageHandlers.ContainsKey(msgId))
            {
                mMessageHandlers[msgId] -= handler;
            }
        }
    }

    protected void DispatchMessage(NetMessage packet)
    {
        int msgId = packet.Type;
        Action<object> callbackListeners;
        if (mMessageHandlers.TryGetValue(msgId, out callbackListeners))
        {
            callbackListeners?.Invoke(packet.Content);
        }
    }
}
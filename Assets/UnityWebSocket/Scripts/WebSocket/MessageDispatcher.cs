using Google.Protobuf;
using Protoc;
using System;
using System.Collections.Generic;
using System.Reflection;

public class MessageDispatcher : Singleton<MessageDispatcher>
{
    protected const int MAX_DISPATCH_MESSAGE_COUNT_PER_FRAME = 16;
    protected Dictionary<int, Action<object>> mMessageHandlers = new Dictionary<int, Action<object>>();
    protected Dictionary<int, List<MethodInfo>> mMessageMethods = new Dictionary<int, List<MethodInfo>>();
    protected Queue<NetMessage> mMessageFrontQueue = new Queue<NetMessage>();
    protected Queue<NetMessage> mMessageBackQueue = new Queue<NetMessage>();
    protected Type mResponseAttrType = typeof(ResponseAttribute);

    //public void AutoRegiste()
    //{
    //    var types = Assembly.GetExecutingAssembly().GetTypes();
    //    foreach (var t in types)
    //    {
    //        if (t.IsAbstract || t.IsInterface)
    //            continue;
    //        var methods = t.GetMethods();
    //        foreach (var method in methods)
    //        {
    //            UnityEngine.Debug.Log("----methodname:" + method.Name);
    //            if (method.IsAbstract || method.IsVirtual)
    //                continue;
    //            var attr = method.GetCustomAttribute(mResponseAttrType);
    //            if (attr != null)
    //            {
    //                var msgId = (attr as ResponseAttribute).MsgId;
    //                //RegisterHandler(msgId, );
    //                UnityEngine.Debug.LogError(method);
    //                UnityEngine.Debug.LogError(attr);
    //            }
    //        }
    //    }
    //}

    public void ResponseRegister()
    {
        foreach (var method in ResponseAttribute.GetResponseMethod(Reflection.GetExecutingAssembly()))
        {
            var msgIds = ResponseAttribute.GetMsgIds(method);
            if (msgIds != null)
            {
                for (int i = 0; i < msgIds.Length; i++)
                {
                    RegisterMethod(msgIds[i], method);
#if DEBUG_NETWORK
                    UnityEngine.Debug.Log($"注册消息:{msgIds[i]}  函数名:{method.Name}");
#endif
                }
            }
        }
    }

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

    private void RegisterMethod(int msgId, MethodInfo method)
    {
        if (method != null)
        {
            if (mMessageMethods.ContainsKey(msgId))
            {
                mMessageMethods[msgId].Add(method);
            }
            else
            {
                var methodList = new List<MethodInfo>();
                methodList.Add(method);
                mMessageMethods.Add(msgId, methodList);
            }
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

        List<MethodInfo> methods;
        if (mMessageMethods.TryGetValue(msgId, out methods))
        {
            foreach (var method in methods)
            {
                var type = method.ReflectedType;
                var obj = Activator.CreateInstance(type);
                method.Invoke(obj, new object[] { packet.Content });
            }
        }
    }
}
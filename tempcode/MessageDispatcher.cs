using HelixSdk.Hnet.Wire;
using HolaUtilLib;
using System;
using System.Collections.Generic;
using pb = global::Google.Protobuf;

namespace CorePlay
{
    public partial class MessageDispatcher : XSingleton<MessageDispatcher>
    {
        public delegate void EventListenerDelegate(NetMessage Netmsg);

        private Dictionary<int, List<EventListenerDelegate>> eventListeners = new Dictionary<int, List<EventListenerDelegate>>();

        protected Dictionary<int, Action<pb::IMessage>> mMessageHandlers = new Dictionary<int, Action<pb::IMessage>>();

        public Dictionary<int, List<EventListenerDelegate>> EventListeners
        {
            get
            {
                return eventListeners;
            }
            set
            {
                EventListeners = value;
            }
        }

        public void RegisterOnMessageReceived<T>(Action<pb::IMessage> handler) where T : pb::IMessage
        {
            int msgId = NetMessageIdList.TypeToMsgId(typeof(T));
            if (msgId != 0)
            {
                RegisterHandler(msgId, handler);
            }
        }

        public void UnRegisterOnMessageReceived<T>(Action<pb::IMessage> handler) where T : pb::IMessage
        {
            int msgId = NetMessageIdList.TypeToMsgId(typeof(T));
            if (msgId != 0)
            {
                UnRegisterHandler(msgId, handler);
            }
        }

        protected void RegisterHandler(int msgId, Action<pb::IMessage> handler)
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

        protected void UnRegisterHandler(int msgId, Action<pb::IMessage> handler)
        {
            if (handler != null)
            {
                if (mMessageHandlers.ContainsKey(msgId))
                {
                    mMessageHandlers[msgId] -= handler;
                }
            }
        }

        public void AddMessageListener(int eventType, EventListenerDelegate listener)
        {
            if (eventListeners.ContainsKey(eventType))
            {
                List<EventListenerDelegate> list = eventListeners[eventType];
                list.Add(listener);
            }
            else
            {
                List<EventListenerDelegate> list = new List<EventListenerDelegate>();
                list.Add(listener);
                eventListeners.Add(eventType, list);
            }
        }

        public void RemoveMessageListener(int eventType, EventListenerDelegate listener)
        {
            if (eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType].Remove(listener);
                if (eventListeners[eventType].Count == 0)
                {
                    eventListeners.Remove(eventType);
                }
            }
        }

        /// <summary>
        /// 分发消息
        /// </summary>
        /// <param name="msg"></param>
        public void ProcessMsg(NetMessage msg)
        {
            if (eventListeners.ContainsKey(msg.Type))
            {
                List<EventListenerDelegate> listeners = eventListeners[msg.Type];
                for (int i = 0; i < listeners.Count; i++)
                {
                    listeners[i](msg);
                }
            }
            else
            {
                if (mMessageHandlers.ContainsKey(msg.Type))
                {
                    Action<pb::IMessage> callback;
                    if (mMessageHandlers.TryGetValue(msg.Type, out callback))
                    {
                        callback?.Invoke(msg);
                    }
                }
            }
        }
    }
}

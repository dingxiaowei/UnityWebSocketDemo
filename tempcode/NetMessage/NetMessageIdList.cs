using CorePlay;
using HolaUtilLib;
using Protoc;
using System;
using System.Collections.Generic;

//public sealed partial class StandardRequest : INetMessage { }
//public sealed partial class StandardResponse : INetMessage { }
//public sealed partial class EnterRoomCommand : INetMessage { }
//public sealed partial class ClientMoveData : IShared
//{
//    public void Dispose(){ }

//    public void OnCreated(){ }
//}

//TODO:如果频繁交互的消息体，可以考虑通过部分类集成对象池对象的接口来实现

public class NetMessageIdList : XSingleton<NetMessageIdList>
{
    private Dictionary<int, Type> mIdToType = new Dictionary<int, Type>();
    private Dictionary<Type, int> mTypeToId = new Dictionary<Type, int>();
    public NetMessageIdList()
    {
        //自定义消息
        //mIdToType[(int)HereMessageID.MsgItemOp] = typeof(PlayerMoveEvent);
        //mTypeToId[typeof(PlayerMoveEvent)] = (int)HereMessageID.MsgItemOp;
        //mIdToType[(int)HereMessageID.MsgPlayerLogin] = typeof(PlayerLoginEvent);
        //mTypeToId[typeof(PlayerLoginEvent)] = (int)HereMessageID.MsgPlayerLogin;
        //mIdToType[(int)HereMessageID.MsgPlayerAttack] = typeof(PlayerAttackEvent);
        //mTypeToId[typeof(PlayerAttackEvent)] = (int)HereMessageID.MsgPlayerAttack;
        //mIdToType[(int)HereMessageID.CHATMESSAGE] = typeof(ChatMessageEvent);
        //mTypeToId[typeof(ChatMessageEvent)] = (int)HereMessageID.CHATMESSAGE;

        //Proto消息
        mIdToType[(int)ROOM_MSG_TYPE.RmEnterRoomCommand] = typeof(EnterRoomCommand);
        mTypeToId[typeof(EnterRoomCommand)] = (int)ROOM_MSG_TYPE.RmEnterRoomCommand;
        mIdToType[(int)ROOM_MSG_TYPE.RmQueryRoomRequest] = typeof(StandardRequest);
        mTypeToId[typeof(StandardRequest)] = (int)ROOM_MSG_TYPE.RmQueryRoomRequest;
        mIdToType[(int)ROOM_MSG_TYPE.RmClientMoveData] = typeof(ClientMoveData);
        mTypeToId[typeof(ClientMoveData)] = (int)ROOM_MSG_TYPE.RmClientMoveData;
    }

    public static Type MsgIdToType(int id)
    {
        Type t = null;
        singleton.mIdToType.TryGetValue(id, out t);
        return t;
    }

    public static int TypeToMsgId(Type type)
    {
        int msgId = 0;
        singleton.mTypeToId.TryGetValue(type, out msgId);
        return msgId;
    }
}

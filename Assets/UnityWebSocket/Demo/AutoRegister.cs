using Protoc;
using System.Collections;
using System.Collections.Generic;
using Protoc.AutoRegister.HeartBeat;
using Protoc.Managers;
using UnityEngine;

public class AutoRegister : MonoBehaviour
{
    private void Start()
    {
        MessageDispatcher.sInstance.AutoRegistHandlers();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "连接Socket"))
        {
            NetManager.sInstance.ConnectServer();
        }
        
        if (GUI.Button(new Rect(10, 60, 100, 40), "连发100消息"))
        {
            for (int i = 0; i < 100; i++)
            {
                S2C_EnterMap msg = new S2C_EnterMap();
                msg.Message = "服务器向客户端发送的进入地图消息";
                msg.RpcId = 1;
                msg.UnitId = i;
                msg.Error = 0;
                List<Protoc.UnitInfo> unitInfos = new List<UnitInfo>();
                unitInfos.Add(new UnitInfo() { UnitId = 222, X = 1, Y = 1, Z = 1 });
                msg.Units.AddRange(unitInfos);
                NetManager.sInstance.SocketSession?.SendAsync((int)OuterOpcode.S2C_EnterMapResponse, msg);
            }
        }
        if (GUI.Button(new Rect(10, 110, 100, 40), "连发消息"))
        {
            S2C_EnterMap msg = new S2C_EnterMap();
            msg.Message = "服务器向客户端发送的进入地图消息";
            msg.RpcId = 1;
            msg.UnitId = 0;
            msg.Error = 0;
            List<Protoc.UnitInfo> unitInfos = new List<UnitInfo>();
            unitInfos.Add(new UnitInfo() { UnitId = 222, X = 1, Y = 1, Z = 1 });
            msg.Units.AddRange(unitInfos);
            NetManager.sInstance.SocketSession?.SendAsync((int)OuterOpcode.S2C_EnterMapResponse, msg);
        }
        
        if (GUI.Button(new Rect(10, 170, 100, 40), "开始心跳"))
        {
            RepeatSendHeartBeatRequest();
        }
        
        if (GUI.Button(new Rect(10, 230, 100, 40), "断开Socket"))
        {
            NetManager.sInstance.SocketSession.Disconnect();
        }
    }

    private void RepeatSendHeartBeatRequest()
    {
        InvokeRepeating("StartHeartBeat",0,3);
    }

    private void StartHeartBeat()
    {
        NetManager.sInstance.SendHeartBeat();
    }

    private void Update()
    {
        if (NetManager.sInstance.SocketSession != null && NetManager.sInstance.SocketSession.IsConnected)
        {
            NetManager.sInstance.SocketSession.Update();
        }
    }

    private void OnDestroy()
    {
        NetManager.sInstance.SocketSession?.Disconnect();
        MessageDispatcher.sInstance.Dispose();
    }
}

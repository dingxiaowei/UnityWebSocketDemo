using Protoc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRegister : MonoBehaviour
{
    private string serverUrl = "ws://124.223.54.98:8081";
    //private string serverUrl = "ws://127.0.0.1:8081";
    private WSSocketSession socketSession;

    private void Start()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("User", "dxw");
        socketSession = new WSSocketSession(serverUrl, "1001", headers, (res) =>
        {
            var connectState = res ? "连接成功" : "连接失败";
            Debug.Log($"websocket {connectState}");
        });

        MessageDispatcher.sInstance.AutoRegistHandlers();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "连接Socket"))
        {
            socketSession?.ConnectAsync();
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
                socketSession.SendAsync((int)OuterOpcode.S2C_EnterMapResponse, msg);
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
            socketSession.SendAsync((int)OuterOpcode.S2C_EnterMapResponse, msg);
        }
    }

    private void Update()
    {
        if (socketSession != null && socketSession.IsConnected)
        {
            socketSession.Update();
        }
    }

    private void OnDestroy()
    {
        socketSession?.Disconnect();
        MessageDispatcher.sInstance.Dispose();
    }
}

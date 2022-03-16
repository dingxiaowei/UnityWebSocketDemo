﻿using Google.Protobuf;
using Protoc;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketDemo : MonoBehaviour
{
    private string serverUrl = "ws://127.0.0.1:5963";
    private WSSocketSession socketSession;
    private Person person;//测试消息

    private void Awake()
    {
        person = new Person();
        person.Id = 1001;
        person.Name = "dxw";
        person.Address = ByteString.CopyFromUtf8("中国,江苏");
        person.Email = "dingxiaowei2@huawei.com";

        List<Person.Types.PhoneNumber> phoneNumList = new List<Person.Types.PhoneNumber>();
        Person.Types.PhoneNumber phoneNumber1 = new Person.Types.PhoneNumber();
        phoneNumber1.Number = "13262983383";
        phoneNumber1.Type = Person.Types.PhoneType.Home;
        phoneNumList.Add(phoneNumber1);
        Person.Types.PhoneNumber phoneNumber2 = new Person.Types.PhoneNumber();
        phoneNumber2.Number = "13262983384";
        phoneNumber2.Type = Person.Types.PhoneType.Mobile;
        phoneNumList.Add(phoneNumber2);
        person.Phone.AddRange(phoneNumList);
    }
    private void Start()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("User", "dxw");
        socketSession = new WSSocketSession(serverUrl, "1001", headers, (res) =>
        {
            var connectState = res ? "连接成功" : "连接失败";
            Debug.Log($"websocket {connectState}");
        });

        //注册监听方式1：自动注册，无需手动写Register方法，前提：需要在监听的方法写上特性，特性支持多个ID号，并且方式需要是public属性的
        MessageDispatcher.sInstance.ResponseAutoRegister();
        //注册监听方式2：泛型监听，前提：需要手写在MessageIdList脚本里手动绑定Id跟类型的对应关系
        //MessageDispatcher.sInstance.RegisterOnMessageReceived<Person>(OnReceivedPersonMsg);
        //注册监听方式3：手动写上Id与监听方法
        //MessageDispatcher.sInstance.RegisterHandler((int)MsgType.EPersonMsg2, OnReceivedPersonMsg);
    }
    

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "连接Socket"))
        {
            socketSession?.ConnectAsync();
        }
        if (GUI.Button(new Rect(10, 60, 100, 40), "连发100条消息"))
        {
            for (int i = 0; i < 100; i++)
            {
                person.Id = i;
                socketSession.Send((int)MsgType.EPersonMsg, person);
            }
        }
        if (GUI.Button(new Rect(10, 110, 100, 40), "发送消息"))
        {
            socketSession.Send<Person>(person);
        }
        if (GUI.Button(new Rect(10, 160, 100, 40), "收发多个消息"))
        {
            for (int i = 0; i < 10; i++)
            {
                person.Id = i;
                socketSession.Send((int)MsgType.EPersonMsg, person);
                person.Id = 10 + i;
                socketSession.Send((int)MsgType.EPersonMsg2, person);
            }
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
        MessageDispatcher.sInstance.ClearMessageMethods();
        socketSession?.Disconnect();
    }
}

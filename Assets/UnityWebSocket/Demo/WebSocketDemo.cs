using Google.Protobuf;
using Protoc;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketDemo : MonoBehaviour
{
    public string address = "ws://127.0.0.1:5963";
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
        socketSession = new WSSocketSession(address, "1001", headers, (res) =>
        {
            Debug.Log(res);
        });

        MessageDispatcher.sInstance.RegisterOnMessageReceived<Person>(OnReceivedPersonMsg);
    }

    protected void OnReceivedPersonMsg(object msg)
    {
        Debug.Log("接收到服务器下发的person消息");
        if (msg == null)
        {
            Debug.LogError("接受的person消息有误");
            return;
        }
        var person = Person.Parser.ParseFrom(msg as ByteString);
        DebugPerson(person);
    }

    //测试用
    void DebugPerson(Person desPerson)
    {
        Debug.Log(string.Format("ID:{0} Name:{1} Email:{2} Address:{3}", desPerson.Id, desPerson.Name, desPerson.Email, System.Text.Encoding.UTF8.GetString(desPerson.Address.ToByteArray())));
        for (int i = 0; i < desPerson.Phone.Count; i++)
        {
            Debug.Log(string.Format("PhoneNum:{0} Type:{1}", desPerson.Phone[i].Number, desPerson.Phone[i].Type));
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 40), "连接Socket"))
        {
            socketSession?.Connect();
        }
        if (GUI.Button(new Rect(10, 60, 100, 40), "发送消息"))
        {
            socketSession.Send(1, person);
            //socketSession.Send<Person>(person);
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
        MessageDispatcher.sInstance.UnregisterOnMessageReceived<Person>(OnReceivedPersonMsg);
        socketSession?.Disconnect();
    }
}

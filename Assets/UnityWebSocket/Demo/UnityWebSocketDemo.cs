using Google.Protobuf;
using Protoc;
using System.Collections.Generic;
using UnityEngine;

namespace UnityWebSocket.Demo
{
    public class UnityWebSocketDemo : MonoBehaviour
    {
        public string address = "ws://127.0.0.1";
        public string sendText = "Hello World!";
        public bool logMessage = true;

        private IWebSocket socket;

        private string log = "";
        private int sendCount;
        private int receiveCount;
        private Vector2 scrollPos;

#if !UNITY_EDITOR && UNITY_WEBGL
    private void Awake()
    {
        address = "wss://echo.websocket.org";
    }
#endif

        private void OnGUI()
        {
            var scale = Screen.width / 800f;
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(scale, scale, 1));
            var width = GUILayout.Width(Screen.width / scale - 10);

            WebSocketState state = socket == null ? WebSocketState.Closed : socket.ReadyState;

            GUILayout.Label("SDK Version: 2.5.0", width);
            var stateColor = state == WebSocketState.Closed ? "red" : state == WebSocketState.Open ? "#11ff11" : "#aa4444";
            var richText = new GUIStyle() { richText = true };
            GUILayout.Label(string.Format(" <color=white>State:</color> <color={1}>{0}</color>", state, stateColor), richText);

            GUI.enabled = state == WebSocketState.Closed;
            GUILayout.Label("Address: ", width);
            address = GUILayout.TextField(address, width);

            GUILayout.BeginHorizontal();
            GUI.enabled = state == WebSocketState.Closed;
            if (GUILayout.Button(state == WebSocketState.Connecting ? "Connecting..." : "Connect"))
            {
                socket = new WebSocket(address);
                socket.OnOpen += Socket_OnOpen;
                socket.OnMessage += Socket_OnMessage;
                socket.OnClose += Socket_OnClose;
                socket.OnError += Socket_OnError;
                AddLog(string.Format("Connecting...\n"));
                socket.ConnectAsync();
            }

            GUI.enabled = state == WebSocketState.Open;
            if (GUILayout.Button(state == WebSocketState.Closing ? "Closing..." : "Close"))
            {
                AddLog(string.Format("Closing...\n"));
                socket.CloseAsync();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("Text: ");
            sendText = GUILayout.TextArea(sendText, GUILayout.MinHeight(50), width);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Send"))
            {
                if (!string.IsNullOrEmpty(sendText))
                {
                    socket.SendAsync(sendText);
                    if (logMessage)
                        AddLog(string.Format("Send: {0}\n", sendText));
                    sendCount += 1;
                }
            }
            if (GUILayout.Button("Send Bytes"))
            {
                if (!string.IsNullOrEmpty(sendText))
                {
                    //var bytes = System.Text.Encoding.UTF8.GetBytes(sendText);
                    //socket.SendAsync(bytes);

                    //if (logMessage)
                    //    AddLog(string.Format("Send Bytes ({1}): {0}\n", sendText, bytes.Length));
                    //sendCount += 1;
                    Person person = new Person();
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

                    NetMessage msg = new NetMessage();
                    msg.Xid = "1001";
                    msg.Type = 1001;
                    msg.Content = person.ToByteString();
                    msg.Oid = "";
                    socket.SendAsync(msg.ToByteArray());
                    sendCount++;
                }
            }
            if (GUILayout.Button("Send x100"))
            {
                if (!string.IsNullOrEmpty(sendText))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var text = (i + 1).ToString() + ". " + sendText;
                        socket.SendAsync(text);

                        if (logMessage)
                            AddLog(string.Format("Send: {0}\n", text));
                        sendCount += 1;
                    }
                }
            }
            if (GUILayout.Button("Send Bytes x100"))
            {
                if (!string.IsNullOrEmpty(sendText))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        var text = (i + 1).ToString() + ". " + sendText;
                        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
                        socket.SendAsync(bytes);
                        if (logMessage)
                            AddLog(string.Format("Send Bytes ({1}): {0}\n", text, bytes.Length));
                        sendCount += 1;
                    }
                }
            }
            GUILayout.EndHorizontal();

            GUI.enabled = true;
            GUILayout.BeginHorizontal();
            logMessage = GUILayout.Toggle(logMessage, "Log Message");
            GUILayout.Label(string.Format("Send Count: {0}", sendCount));
            GUILayout.Label(string.Format("Receive Count: {0}", receiveCount));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Clear"))
            {
                log = "";
                receiveCount = 0;
                sendCount = 0;
            }

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.MaxHeight(Screen.height / scale - 270), width);
            GUILayout.Label(log);
            GUILayout.EndScrollView();
        }

        private void AddLog(string str)
        {
            log += str;
            // max log
            if (log.Length > 32 * 1024)
            {
                log = log.Substring(16 * 1024);
            }
        }

        private void Socket_OnOpen(object sender, OpenEventArgs e)
        {
            AddLog(string.Format("Connected: {0}\n", address));
        }

        private void Socket_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsBinary)
            {
                if (logMessage)
                    AddLog(string.Format("Receive Bytes ({1}): {0}\n", e.Data, e.RawData.Length));

                var netMsg = NetMessage.Parser.ParseFrom(e.RawData);
                Debug.Log("----------------------接受消息------------------------");
                Debug.Log($"type:{netMsg.Type} xid:{netMsg.Xid} oid:{netMsg.Oid}");
                var stu = Person.Parser.ParseFrom(netMsg.Content);
                DebugPerson(stu);
            }
            else if (e.IsText)
            {
                if (logMessage)
                    AddLog(string.Format("Receive: {0}\n", e.Data));
            }
            receiveCount += 1;
        }

        private void Socket_OnClose(object sender, CloseEventArgs e)
        {
            AddLog(string.Format("Closed: StatusCode: {0}, Reason: {1}\n", e.StatusCode, e.Reason));
        }

        private void Socket_OnError(object sender, ErrorEventArgs e)
        {
            AddLog(string.Format("Error: {0}\n", e.Message));
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

        private void OnApplicationQuit()
        {
            if (socket != null && socket.ReadyState != WebSocketState.Closed)
            {
                socket.CloseAsync();
            }
        }
    }
}

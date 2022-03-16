## **快速开始**
本项目是Unity WebSocket消息分发的展示Demo，项目设计的知识点C#的反射、特性、Linq、异步、泛型、以及消息列队、消息分发、对象池、protobuf3的生成和使用、bat批处理，自压测一帧收发100条消息速度很快，gc较少，稳定可靠。

### Demo支持的功能
* 支持泛型/非泛型异步收发消息
```
//非泛型方式
socketSession.Send((int)MsgType.EPersonMsg, person);
//泛型方式
socketSession.Send<Person>(person);
```
* 三种消息监听方法
```
//注册监听方式1：自动注册，无需手动写Register方法，前提：需要在监听的方法写上特性，特性支持多个ID号，并且方式需要是public属性的
MessageDispatcher.sInstance.ResponseAutoRegister();
//注册监听方式2：泛型监听，前提：需要手写在MessageIdList脚本里手动绑定Id跟类型的对应关系
MessageDispatcher.sInstance.RegisterOnMessageReceived<Person>(OnReceivedPersonMsg);
//注册监听方式3：手动写上Id与监听方法
MessageDispatcher.sInstance.RegisterHandler((int)MsgType.EPersonMsg2, OnReceivedPersonMsg);
```
* 一帧支持收发多个消息
自压测100个消息反应很快
![](img/1.png)
* 自动注册消息监听
```
MessageDispatcher.sInstance.ResponseAutoRegister();
```
会自动搜集带有特性的网络回调方法
* 特性支持多个ID号处理同一个消息，配合自动消息监听使用
![](img/2.png)
如果一个下行消息包对应多个ID号，特性上支持多可变参数的配置，注意方法要是public的
* 支持自定义请求头的配置
在创建WSSocketSession对象的时候支持配置Headers请求参数，例如用于鉴权认证啥的
* 自带C#测试服务器
直接在Unity的菜单栏Tools里面启动服务器即可，目前服务器是没有业务逻辑，接收到数据包直接发送该包给客户端，主要是用于客户端的测试，可以在此服务器的基础上自行添加业务逻辑，可以实现网络游戏了。
### **使用方法**

- 代码示例

  ```csharp
  // 命名空间
  using UnityWebSocket;

  // 创建实例
  string address = "ws://echo.websocket.org";
  WebSocket socket = new WebSocket(address);

  // 注册回调
  socket.OnOpen += OnOpen;
  socket.OnClose += OnClose;
  socket.OnMessage += OnMessage;
  socket.OnError += OnError;

  // 连接
  socket.ConnectAsync();

  // 发送 string 类型数据
  socket.SendAsync(str)

  // 或者 发送 byte[] 类型数据（建议使用）
  socket.SendAsync(bytes);

  // 关闭连接
  socket.CloseAsync();
  ```
- 收发消息
	- 发送消息
	参考上面两种消息发送形式，支持异步发送
	- 接收消息
	添加监听，参考上面三种监听方式，任意使用其中一种即可，分发的消息处理
    ```
    [Response((int)MsgType.EPersonMsg, (int)MsgType.EPersonMsg2)]
        public void OnReceivedPersonMsg(object msg)
        {
            if (msg == null)
            {
                Debug.LogError("接受的person消息有误");
                return;
            }
            var person = Person.Parser.ParseFrom(msg as ByteString);
    #if DEBUG_NETWORK
            Debug.Log("----打印消息分发的角色------");
            DebugPerson(person);
    #endif
        }
	```
- Protobuf3的生成
  在Tools/ProtoGenTools中点击GenAllC#.bat就会把当前目录下所有的proto文件生成对应的C#数据类，会生成到client文件夹下，然后点击CopyC#ProtocolsToCorePlay.bat会将C#类文件自动拷贝到Unity的对应工程目录下

- 功能菜单：
  - Tools -> UnityWebSocket -> Server(Fleck) ， WebSocket 服务器，基于 [Fleck](https://github.com/statianzo/Fleck) 插件实现的测试服务器，仅用于开发测试。

- Unity 编译宏（可选项）：
  - `UNITY_WEB_SOCKET_LOG` 打开底层日志输出。


### **注意（Warning）**

- 插件中多个命名空间中存在 **WebSocket** 类，适用不同环境，请根据自身需求选择。

  命名空间 | 平台 | 方式 |  说明
  -|-|-|-
  UnityWebSocket | 全平台 | 同步(无阻塞) | **[推荐]** 无需考虑异步回调使用 Unity 组件的问题。
  UnityWebSocket.Uniform | 全平台 | 异步 | 需要考虑异步回调使用 Unity 组件的问题。
  UnityWebSocket.WebGL | WebGL平台 | 异步 | 仅支持WebGL平台下的通信。
  UnityWebSocket.NoWebGL | 非WebGL平台 | 异步  | 仅支持非WebGL平台下的通信。

### 工程代码
https://codehub-g.huawei.com/d00605132/UnityWebSocket/home
感兴趣的同学可以自行学习，后面有空的话，我也会专门做一个网络模块相关的主题分享以及配套网络游戏Demo工程

### 对接测试
wsurl   ws://cyberverse.hwcloudtest.cn:9995/bff/hola/room/123
要设置Header User,"dxw"
发送1020 返回1025 1030


## **快速开始**


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

- 功能菜单：
  - Tools -> UnityWebSocket -> Preference ，偏好设置面板，包含WebGL的平台配置和检测，版本更新检测，问题反馈渠道等。
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

### 对接测试
wsurl   ws://cyberverse.hwcloudtest.cn:9995/bff/hola/room/123
要设置Header User,"dxw"
发送1020 返回1025 1030


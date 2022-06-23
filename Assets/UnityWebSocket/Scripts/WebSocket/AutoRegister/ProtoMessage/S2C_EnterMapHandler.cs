using Google.Protobuf;
using Protoc;

[MessageHandler]
public class S2C_EnterMapHandler : AMHandler<S2C_EnterMap>
{
    //protected override void Run(S2C_EnterMap message)
    //{
    //    message.Debug();
    //}
    protected override void Run(ByteString content)
    {
        UnityEngine.Debug.Log("~~~~~~~~~~~~~~收到服务器返回的消息S2C_EnterMap");
        var s2cEnterMap = S2C_EnterMap.Parser.ParseFrom(content);
        s2cEnterMap.Debug();
    }
}

[MessageHandler]
public class C2S_EnterMapHandler : AMHandler<C2S_EnterMap>
{
    protected override void Run(ByteString content)
    {
        UnityEngine.Debug.Log("收到服务器返回消息EnterMap");
        var c2sEnterMap = C2S_EnterMap.Parser.ParseFrom(content);
        c2sEnterMap.ToString();
    }
}

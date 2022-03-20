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
        UnityEngine.Debug.Log("执行消息的Run方法");
        var s2cEnterMap = S2C_EnterMap.Parser.ParseFrom(content);
        s2cEnterMap.Debug();
    }
}

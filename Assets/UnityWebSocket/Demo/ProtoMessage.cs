using Google.Protobuf;
using Protoc;
using UnityEngine;

public enum MsgType : int
{
    EPersonMsg = 1,
    EPersonMsg2 = 2
}

public class ProtoMessage
{
    //必须是public的  否则无法被搜集
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

    //测试用
    void DebugPerson(Person desPerson)
    {
        Debug.Log("打印person信息");
        Debug.Log(string.Format("ID:{0} Name:{1} Email:{2} Address:{3}", desPerson.Id, desPerson.Name, desPerson.Email, System.Text.Encoding.UTF8.GetString(desPerson.Address.ToByteArray())));
        for (int i = 0; i < desPerson.Phone.Count; i++)
        {
            Debug.Log(string.Format("PhoneNum:{0} Type:{1}", desPerson.Phone[i].Number, desPerson.Phone[i].Type));
        }
    }
}

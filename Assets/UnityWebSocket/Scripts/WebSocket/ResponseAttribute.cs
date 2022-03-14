using System;

public class ResponseAttribute : Attribute
{
    public int MsgId { get; private set; }
    public ResponseAttribute(int id)
    {
        this.MsgId = id;
    }
}

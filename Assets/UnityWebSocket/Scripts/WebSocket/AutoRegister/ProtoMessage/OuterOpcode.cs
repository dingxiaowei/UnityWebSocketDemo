namespace Protoc
{
    public static partial class OuterOpcode
    {
        public const int C2S_EnterMapRequest = 101;
        public const int S2C_EnterMapResponse = 102;
    }

    [Message(OuterOpcode.C2S_EnterMapRequest)]
    public partial class C2S_EnterMap
    {
    }

    [Message(OuterOpcode.S2C_EnterMapResponse)]  //TODO:这里编号可以写proto里面的id编号
    public partial class S2C_EnterMap
    {
        public void Debug()
        {
            UnityEngine.Debug.Log($"Message:{this.Message}  UnitId:{this.UnitId}");
            foreach (var unit in this.Units)
            {
                UnityEngine.Debug.Log($"ID:{unit.UnitId} X:{unit.X} Y:{unit.Y} Z:{unit.Z}");
            }
        }
    }
}
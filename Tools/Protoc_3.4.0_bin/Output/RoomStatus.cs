// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: room_status.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace Msg {

  #region Enums
  public enum ROOM_STATUS_MSG_TYPE {
    RsmDefault = 0,
    RsmCurrentUsersRequest = 3001,
    RsmCurrentUsersResponse = 3005,
    RsmUserData = 3010,
    RsmCurrentUsersData = 3020,
    RsmCloseMicRequest = 3030,
    RsmCloseMicResponse = 3040,
    RsmCloseMicCommand = 3045,
    RsmOpenMicRequest = 3050,
    RsmOpenMicResponse = 3060,
    RsmOpenMicCommand = 3065,
    RsmSkillActionRequest = 3070,
    RsmSkillActionResponse = 3080,
    RsmSkillActionCommand = 3090,
  }

  #endregion

  #region Messages
  public sealed class UserData : pb::IMessage {
    private static readonly pb::MessageParser<UserData> _parser = new pb::MessageParser<UserData>(() => new UserData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserData> Parser { get { return _parser; } }

    /// <summary>Field number for the "userId" field.</summary>
    public const int UserIdFieldNumber = 1;
    private string userId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserId {
      get { return userId_; }
      set {
        userId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 3;
    private int timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "followed" field.</summary>
    public const int FollowedFieldNumber = 4;
    private bool followed_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Followed {
      get { return followed_; }
      set {
        followed_ = value;
      }
    }

    /// <summary>Field number for the "mic" field.</summary>
    public const int MicFieldNumber = 5;
    private bool mic_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Mic {
      get { return mic_; }
      set {
        mic_ = value;
      }
    }

    /// <summary>Field number for the "avatar" field.</summary>
    public const int AvatarFieldNumber = 6;
    private string avatar_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Avatar {
      get { return avatar_; }
      set {
        avatar_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "leader" field.</summary>
    public const int LeaderFieldNumber = 7;
    private bool leader_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Leader {
      get { return leader_; }
      set {
        leader_ = value;
      }
    }

    /// <summary>Field number for the "self" field.</summary>
    public const int SelfFieldNumber = 8;
    private bool self_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Self {
      get { return self_; }
      set {
        self_ = value;
      }
    }

    /// <summary>Field number for the "online" field.</summary>
    public const int OnlineFieldNumber = 9;
    private bool online_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Online {
      get { return online_; }
      set {
        online_ = value;
      }
    }

    /// <summary>Field number for the "offLineTime" field.</summary>
    public const int OffLineTimeFieldNumber = 10;
    private int offLineTime_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int OffLineTime {
      get { return offLineTime_; }
      set {
        offLineTime_ = value;
      }
    }

    /// <summary>Field number for the "forbidWord" field.</summary>
    public const int ForbidWordFieldNumber = 11;
    private bool forbidWord_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool ForbidWord {
      get { return forbidWord_; }
      set {
        forbidWord_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(UserId);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Timestamp != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Timestamp);
      }
      if (Followed != false) {
        output.WriteRawTag(32);
        output.WriteBool(Followed);
      }
      if (Mic != false) {
        output.WriteRawTag(40);
        output.WriteBool(Mic);
      }
      if (Avatar.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Avatar);
      }
      if (Leader != false) {
        output.WriteRawTag(56);
        output.WriteBool(Leader);
      }
      if (Self != false) {
        output.WriteRawTag(64);
        output.WriteBool(Self);
      }
      if (Online != false) {
        output.WriteRawTag(72);
        output.WriteBool(Online);
      }
      if (OffLineTime != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(OffLineTime);
      }
      if (ForbidWord != false) {
        output.WriteRawTag(88);
        output.WriteBool(ForbidWord);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserId);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Timestamp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Timestamp);
      }
      if (Followed != false) {
        size += 1 + 1;
      }
      if (Mic != false) {
        size += 1 + 1;
      }
      if (Avatar.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Avatar);
      }
      if (Leader != false) {
        size += 1 + 1;
      }
      if (Self != false) {
        size += 1 + 1;
      }
      if (Online != false) {
        size += 1 + 1;
      }
      if (OffLineTime != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(OffLineTime);
      }
      if (ForbidWord != false) {
        size += 1 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            UserId = input.ReadString();
            break;
          }
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 24: {
            Timestamp = input.ReadInt32();
            break;
          }
          case 32: {
            Followed = input.ReadBool();
            break;
          }
          case 40: {
            Mic = input.ReadBool();
            break;
          }
          case 50: {
            Avatar = input.ReadString();
            break;
          }
          case 56: {
            Leader = input.ReadBool();
            break;
          }
          case 64: {
            Self = input.ReadBool();
            break;
          }
          case 72: {
            Online = input.ReadBool();
            break;
          }
          case 80: {
            OffLineTime = input.ReadInt32();
            break;
          }
          case 88: {
            ForbidWord = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed class CurrentUsersData : pb::IMessage {
    private static readonly pb::MessageParser<CurrentUsersData> _parser = new pb::MessageParser<CurrentUsersData>(() => new CurrentUsersData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CurrentUsersData> Parser { get { return _parser; } }

    /// <summary>Field number for the "users" field.</summary>
    public const int UsersFieldNumber = 1;
    private static readonly pb::FieldCodec<global::Msg.UserData> _repeated_users_codec
        = pb::FieldCodec.ForMessage(10, global::Msg.UserData.Parser);
    private readonly pbc::RepeatedField<global::Msg.UserData> users_ = new pbc::RepeatedField<global::Msg.UserData>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Msg.UserData> Users {
      get { return users_; }
    }

    /// <summary>Field number for the "max" field.</summary>
    public const int MaxFieldNumber = 2;
    private int max_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Max {
      get { return max_; }
      set {
        max_ = value;
      }
    }

    /// <summary>Field number for the "now" field.</summary>
    public const int NowFieldNumber = 3;
    private int now_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Now {
      get { return now_; }
      set {
        now_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      users_.WriteTo(output, _repeated_users_codec);
      if (Max != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Max);
      }
      if (Now != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Now);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += users_.CalculateSize(_repeated_users_codec);
      if (Max != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Max);
      }
      if (Now != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Now);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            users_.AddEntriesFrom(input, _repeated_users_codec);
            break;
          }
          case 16: {
            Max = input.ReadInt32();
            break;
          }
          case 24: {
            Now = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed class CloseMicRequest : pb::IMessage {
    private static readonly pb::MessageParser<CloseMicRequest> _parser = new pb::MessageParser<CloseMicRequest>(() => new CloseMicRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CloseMicRequest> Parser { get { return _parser; } }

    /// <summary>Field number for the "userId" field.</summary>
    public const int UserIdFieldNumber = 1;
    private string userId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserId {
      get { return userId_; }
      set {
        userId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(UserId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            UserId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class OpenMicRequest : pb::IMessage {
    private static readonly pb::MessageParser<OpenMicRequest> _parser = new pb::MessageParser<OpenMicRequest>(() => new OpenMicRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<OpenMicRequest> Parser { get { return _parser; } }

    /// <summary>Field number for the "userId" field.</summary>
    public const int UserIdFieldNumber = 1;
    private string userId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserId {
      get { return userId_; }
      set {
        userId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(UserId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            UserId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class SkillActionRequest : pb::IMessage {
    private static readonly pb::MessageParser<SkillActionRequest> _parser = new pb::MessageParser<SkillActionRequest>(() => new SkillActionRequest());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SkillActionRequest> Parser { get { return _parser; } }

    /// <summary>Field number for the "action" field.</summary>
    public const int ActionFieldNumber = 1;
    private int action_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Action {
      get { return action_; }
      set {
        action_ = value;
      }
    }

    /// <summary>Field number for the "direction" field.</summary>
    public const int DirectionFieldNumber = 2;
    private global::Msg.QuaternionData direction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Msg.QuaternionData Direction {
      get { return direction_; }
      set {
        direction_ = value;
      }
    }

    /// <summary>Field number for the "targetId" field.</summary>
    public const int TargetIdFieldNumber = 3;
    private string targetId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TargetId {
      get { return targetId_; }
      set {
        targetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Action != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Action);
      }
      if (direction_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Direction);
      }
      if (TargetId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(TargetId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Action != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Action);
      }
      if (direction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Direction);
      }
      if (TargetId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TargetId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            Action = input.ReadInt32();
            break;
          }
          case 18: {
            if (direction_ == null) {
              direction_ = new global::Msg.QuaternionData();
            }
            input.ReadMessage(direction_);
            break;
          }
          case 26: {
            TargetId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class SkillActionCommand : pb::IMessage {
    private static readonly pb::MessageParser<SkillActionCommand> _parser = new pb::MessageParser<SkillActionCommand>(() => new SkillActionCommand());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SkillActionCommand> Parser { get { return _parser; } }

    /// <summary>Field number for the "userId" field.</summary>
    public const int UserIdFieldNumber = 1;
    private string userId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string UserId {
      get { return userId_; }
      set {
        userId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "action" field.</summary>
    public const int ActionFieldNumber = 2;
    private int action_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Action {
      get { return action_; }
      set {
        action_ = value;
      }
    }

    /// <summary>Field number for the "direction" field.</summary>
    public const int DirectionFieldNumber = 3;
    private global::Msg.QuaternionData direction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Msg.QuaternionData Direction {
      get { return direction_; }
      set {
        direction_ = value;
      }
    }

    /// <summary>Field number for the "targetId" field.</summary>
    public const int TargetIdFieldNumber = 4;
    private string targetId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TargetId {
      get { return targetId_; }
      set {
        targetId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(UserId);
      }
      if (Action != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Action);
      }
      if (direction_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Direction);
      }
      if (TargetId.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(TargetId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserId);
      }
      if (Action != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Action);
      }
      if (direction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Direction);
      }
      if (TargetId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TargetId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            UserId = input.ReadString();
            break;
          }
          case 16: {
            Action = input.ReadInt32();
            break;
          }
          case 26: {
            if (direction_ == null) {
              direction_ = new global::Msg.QuaternionData();
            }
            input.ReadMessage(direction_);
            break;
          }
          case 34: {
            TargetId = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg
{
public sealed partial class SpeedChange : Luban.BeanBase
{
    public SpeedChange(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { var __json0 = _buf["speed"]; if(!__json0.IsArray) { throw new SerializationException(); } Speed = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Speed.Add(__v0); }   }
        { var __json0 = _buf["time"]; if(!__json0.IsArray) { throw new SerializationException(); } Time = new System.Collections.Generic.List<float>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { float __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Time.Add(__v0); }   }
    }

    public static SpeedChange DeserializeSpeedChange(JSONNode _buf)
    {
        return new SpeedChange(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 速度变化倍率
    /// </summary>
    public readonly System.Collections.Generic.List<float> Speed;
    public readonly System.Collections.Generic.List<float> Time;
   
    public const int __ID__ = -1018620233;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "speed:" + Luban.StringUtil.CollectionToString(Speed) + ","
        + "time:" + Luban.StringUtil.CollectionToString(Time) + ","
        + "}";
    }
}

}

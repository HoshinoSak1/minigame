
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
public sealed partial class EffectData : Luban.BeanBase
{
    public EffectData(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { var __json0 = _buf["effectname"]; if(!__json0.IsArray) { throw new SerializationException(); } Effectname = new System.Collections.Generic.List<string>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { string __v0;  { if(!__e0.IsString) { throw new SerializationException(); }  __v0 = __e0; }  Effectname.Add(__v0); }   }
        { var __json0 = _buf["effectdefine"]; if(!__json0.IsArray) { throw new SerializationException(); } Effectdefine = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Effectdefine.Add(__v0); }   }
    }

    public static EffectData DeserializeEffectData(JSONNode _buf)
    {
        return new EffectData(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    public readonly System.Collections.Generic.List<string> Effectname;
    public readonly System.Collections.Generic.List<int> Effectdefine;
   
    public const int __ID__ = -586168069;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "effectname:" + Luban.StringUtil.CollectionToString(Effectname) + ","
        + "effectdefine:" + Luban.StringUtil.CollectionToString(Effectdefine) + ","
        + "}";
    }
}

}

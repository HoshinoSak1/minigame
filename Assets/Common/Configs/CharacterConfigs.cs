
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
public sealed partial class CharacterConfigs : Luban.BeanBase
{
    public CharacterConfigs(JSONNode _buf) 
    {
        { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
        { if(!_buf["name"].IsString) { throw new SerializationException(); }  Name = _buf["name"]; }
        { if(!_buf["hp"].IsNumber) { throw new SerializationException(); }  Hp = _buf["hp"]; }
        { if(!_buf["speed"].IsNumber) { throw new SerializationException(); }  Speed = _buf["speed"]; }
        { var __json0 = _buf["level"]; if(!__json0.IsArray) { throw new SerializationException(); } Level = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  Level.Add(__v0); }   }
        { var __json0 = _buf["WeaponId"]; if(!__json0.IsArray) { throw new SerializationException(); } WeaponId = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  WeaponId.Add(__v0); }   }
        { if(!_buf["skillID"].IsNumber) { throw new SerializationException(); }  SkillID = _buf["skillID"]; }
    }

    public static CharacterConfigs DeserializeCharacterConfigs(JSONNode _buf)
    {
        return new CharacterConfigs(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public readonly int Id;
    /// <summary>
    /// 角色名称
    /// </summary>
    public readonly string Name;
    public readonly int Hp;
    public readonly int Speed;
    /// <summary>
    /// 等级
    /// </summary>
    public readonly System.Collections.Generic.List<int> Level;
    /// <summary>
    /// 枪械ID
    /// </summary>
    public readonly System.Collections.Generic.List<int> WeaponId;
    /// <summary>
    /// 技能ID
    /// </summary>
    public readonly int SkillID;
   
    public const int __ID__ = -487991768;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "id:" + Id + ","
        + "name:" + Name + ","
        + "hp:" + Hp + ","
        + "speed:" + Speed + ","
        + "level:" + Luban.StringUtil.CollectionToString(Level) + ","
        + "WeaponId:" + Luban.StringUtil.CollectionToString(WeaponId) + ","
        + "skillID:" + SkillID + ","
        + "}";
    }
}

}

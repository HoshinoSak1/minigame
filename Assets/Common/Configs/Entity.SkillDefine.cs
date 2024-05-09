
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.Entity
{
public partial class SkillDefine
{
    private readonly System.Collections.Generic.Dictionary<int, SkillData> _dataMap;
    private readonly System.Collections.Generic.List<SkillData> _dataList;
    
    public SkillDefine(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, SkillData>();
        _dataList = new System.Collections.Generic.List<SkillData>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            SkillData _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = SkillData.DeserializeSkillData(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, SkillData> DataMap => _dataMap;
    public System.Collections.Generic.List<SkillData> DataList => _dataList;

    public SkillData GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public SkillData Get(int key) => _dataMap[key];
    public SkillData this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}


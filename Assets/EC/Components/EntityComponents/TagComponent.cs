using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag
{
    Player = 0,
    Enemy = 1,
    Skill =2,
    Terrain = 3,
    Weapon = 4,
}

public class TagComponent : Component
{
    CharacterComponent characterComponent;
    public Tag tag;

    public override void Init()
    {
        //int dataDefine;
        //if(entity.componentDatas.TryGetValue("TagComponent",out dataDefine))
        //{
        //    tag = (Tag)dataDefine;
        //    parent = (Tag)dataDefine;
        //}
        characterComponent = (CharacterComponent)entity.GetComponent("CharacterComponent");
    }
    public override void DataInit()
    {
        if (characterComponent != null)
        {
            tag = (Tag)(characterComponent.configs.Id / 1100);
            entity.Tag = tag;
        }

    }
    public override void Update()
    {

    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<TagComponent>(this);
    }
}

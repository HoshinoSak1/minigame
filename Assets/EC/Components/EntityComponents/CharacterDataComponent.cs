using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataComponent : Component
{
    public int maxHp;
    public int nowHp;

    public int level = 1;

    public CharacterComponent character;
    public StateComponent state;
   
    public override void Init()
    {
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");

        state = (StateComponent)entity.GetComponent("StateComponent");
    }

    public override void DataInit()
    {
        //Ѫ��
        maxHp = character.configs.Hp;
        nowHp = maxHp;

    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<CharacterDataComponent>(this);
    }

    public void SetHealth(int health)
    {
        this.nowHp = health;
        if (health <= 0)
        {
            state.isDead = true;
        }
    }
    public void SetHealthOffset(int offset)
    {
        this.nowHp -= offset;
        if (nowHp <= 0)
        {
            state.isDead = true;
        }
    }
    public void ChangeHp(int effectData)
    {
        if (state.isDead)
        {
            Debug.Log(entity.entityId + " [ ������ ]");
            return;
        }
        SetHealthOffset(effectData);
        Debug.Log("����˺���" + entity.entityId + " [ " + effectData + " ]");
    }
}

using cfg;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : Component
{
    public CharacterComponent character;
    public WeaponComponent weapon;

    public int skillId;
    public Dictionary<SkillType, SkillBaseData> data;

    public List<float> nowCdtime = new List<float>();
    public override void Init()
    {
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        weapon = (WeaponComponent)entity.GetComponent("WeaponComponent");

        skillId = character.configs.SkillID;

        SkillData config =  TableDataManager.Instance.tables.SkillDefine.Get(skillId);
        if (config != null)
        {
            int cnt = config.SkillType.Count;

            data = new Dictionary<SkillType, SkillBaseData>(cnt);


            SkillBaseData baseData;
            for (int i = 0; i < cnt; i++)
            {
                baseData = new SkillBaseData(   i,
                                                config.SkillType[i],
                                                config.TransmiterId[i],
                                                config.Demage[i],
                                                config.Heal[i],
                                                config.FieldType[i],
                                                config.FieldWidth[i],
                                                config.FieldHeight[i],
                                                config.VaildTime[i],
                                                config.CdTime[i]);
                if(baseData.Type == SkillType.NORMAL)
                {
                    baseData.CDTime = weapon.fireRate;
                }

                data[baseData.Type] = baseData;

                nowCdtime.Add(baseData.CDTime);
            }

        }
    }

    public void UseSkill(SkillType skillType)
    {
        SkillBaseData baseData = data[skillType];

        if (baseData == null) { return; }

        if(!CheckCanUseSkill(skillType))return;
        //�չ�Ĭ�ϴ�weapon���
        if (skillType == SkillType.NORMAL)
        {
            weapon.Fire();
        }
        //������Ҫ�ɷ��������ļ���
        else if(baseData.transmiterId!=0)
        {
            WeaponConfigs transmiter = TableDataManager.Instance.tables.WeaponDefine.Get(baseData.transmiterId);
            //����������Entity
            //֪ͨ������Entity���з���
        }
        //����ʣ�༼�� �������AOE
        else
        {
            //��������Entity
        }
        SetSkillCD(skillType);
    }

    public override void Update()
    {
        for(int i = 0;i < nowCdtime.Count; i++)
        {
            nowCdtime[i] += Time.deltaTime;
        }
    }

    public bool CheckCanUseSkill(SkillType skillType)
    {
        SkillBaseData skillBaseData = data[skillType];
        if (skillBaseData.CDTime <= nowCdtime[skillBaseData.idx]) return true;
        return false;
    }
    public void SetSkillCD(SkillType skillType)
    {
        SkillBaseData skillBaseData = data[skillType];
        nowCdtime[skillBaseData.idx] = 0;
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<SkillComponent>(this);
    }
}

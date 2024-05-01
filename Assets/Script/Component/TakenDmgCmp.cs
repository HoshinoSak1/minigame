using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenDmgCmp : MonoBehaviour
{
    //����������Ϣ���� hp������GOʱ����
    [SerializeField]
    private int hp = 100;

    public string ComponentId;

    public string cmpTag;
    public void Init(string ComponentId,int Hp)
    {
        this.ComponentId = ComponentId;
        this.hp = Hp;
        this.cmpTag = gameObject.tag;
    }

    public int GetHp()
    {
        return hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtCmp atker = collision.gameObject.GetComponent<HurtCmp>();
        int dmg = atker.dmg;
        string atkerTag = atker.tag;
        if (atker != null && atkerTag != cmpTag)
        {
            hp = HurtAction.TakenDmg(dmg, hp);
            Debug.Log(string.Format("��� {0} �˺���ʣ��Ѫ�� ��{1}", dmg, hp));
        }
    }

}

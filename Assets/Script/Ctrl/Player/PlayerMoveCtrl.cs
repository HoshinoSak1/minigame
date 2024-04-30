using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl: BaseModel
{
    public GameObject player;
    public Rigidbody2D rb;

    public CharacterData data;
    //�ٶ�Ӧ�ö����dataȡ������ûд
    public float speed = 100f;

    public void PlayerMove(Vector2 direction)
    {
        MoveAction.MoveGo(player, speed, direction);
    }

    #region ��ʼ��
    public bool Init()
    {
        player = GameObject.Find("Player");

        rb = player.GetComponentInChildren<Rigidbody2D>();
        data = player.GetComponent<CharacterData>();
        return true;
    }
    public UniTask<bool> InitAysnc()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region ��������
    public void OnFixUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }
    #endregion

}

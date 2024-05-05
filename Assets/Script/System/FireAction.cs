using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class FireAction 
{
    /// <summary>
    /// ��������
    ///     �����ӵ�
    ///     ����Ч��
    ///     ����
    /// </summary>
    /// <param name="fireDefines">��������Ļ�����Ϣ</param>
    /// <param name="transmitter">������</param>
    public static void Fire(List<FireDefine> fireDefines,GameObject transmitter)
    {
        foreach(FireDefine fireDefine in fireDefines)
        {
            fireDefine.prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathUtils.GetBulletPrefabFromID(fireDefine.bulletId));

            float timeBetweenBullets = fireDefine.timeBetweenBullets;

            for (int i = 0; i < fireDefine.volleyCount; i++)
            {
                Vector3 baseDir = transmitter.transform.up;
                // �������һ���Ƕ�
                float randomAngle = Random.Range(fireDefine.downLimit, fireDefine.upLimit);

                // ���㵯���ķ���
                Vector2 direction = Quaternion.Euler(0f, 0f, randomAngle) * transmitter.transform.up;

                GameCore.Instance.GetModel<TimerCtrl>().RegisterTimer(fireDefine.timeBetweenBullets, fireDefine.bulletsPerVolley, delegate()
                {
                    BaseFire(fireDefine, transmitter, direction);
                }, true);
            }
        }
    }

    /// <summary>
    /// ���е����������
    /// </summary>
    private static void BaseFire(FireDefine fireDefine, GameObject transmitter, Vector2 direction)
    {
        //�����ӵ�
        GameObject bullet = ObjectPoolManager.Instance.GetPrefabInstance(fireDefine.bulletId, fireDefine.prefab);

        bullet.transform.position = transmitter.transform.position;
        bullet.tag = transmitter.tag;
        //������
        BulletCmp bulletCmp = bullet.GetComponent<BulletCmp>();
        bulletCmp.Init(fireDefine.bulletData);
        //�ӵ���ʧ
        bulletCmp.SetDestroyCmp(true);
        //�ӵ��˺�
        bulletCmp.SetHurtCmp("bullet");

        //����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        MoveAction.MoveRb(rb, (int)fireDefine.bulletData.speed, direction);
    }


    
}

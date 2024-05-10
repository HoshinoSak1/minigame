using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformComponent : Component
{
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 lastPosition;
    public Quaternion lastRotation;

    public override void Init()
    {
        if(entity.go == null) return;
        position = this.entity.go.transform.position;
        rotation = this.entity.go.transform.rotation;

        lastPosition = position;
        lastRotation = rotation;
    }
    public void SetPostionOffset(float x, float y)
    {
        position.x += x;
        position.y += y;
    }
    public void SetPostion(float x, float y)
    {
        lastPosition = position;

        position.x = x;
        position.y = y;

    }

    public void SetRotationLookAt(Vector3 lookPostion)
    {
        lastRotation = rotation;

        // �������Ӧ�ó���ķ���
        Vector3 directionToLook = lookPostion - position;

        // ͨ��LookRotation�����������Ҫ�������ת
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // Ӧ����ת������
        rotation = lookRotation;

    }

    public void SetRotationLookAt(Vector3 lookPostion,Vector3 basePostion)
    {
        lastRotation = rotation;

        // �������Ӧ�ó���ķ���
        Vector3 directionToLook = lookPostion - basePostion;

        // ͨ��LookRotation�����������Ҫ�������ת
        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, directionToLook);

        // Ӧ����ת������
        rotation = lookRotation;

    }

    public int GetPosDir(Vector3 position)
    {
        Vector3 dir = this.position - position;
        dir.Normalize();
        float checkLeft = Vector3.Dot(dir,Vector3.left);
        float checkUp = Vector3.Dot(dir,Vector3.up);
        if (checkLeft >= 0 && checkUp >= 0) return 2;
        else if(checkLeft < 0 && checkUp >= 0) return 3;
        else if (checkLeft < 0 && checkUp < 0) return 4;
        else if(checkLeft >= 0 && checkUp < 0) return 1;
        return 0;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<TransformComponent>(this);
    }
}

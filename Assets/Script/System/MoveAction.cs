using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction
{
    /// <summary>
    /// transform�ƶ�
    /// </summary>
    /// <param name="go"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public static void MoveGo(GameObject go, float speed,Vector2 direction)
    {
        go.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    /// <summary>
    /// �����ƶ�
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="speed"></param>
    /// <param name="direction"></param>
    public static void MoveRb(Rigidbody2D rb, float speed, Vector2 direction)
    {
        rb.velocity  = direction.normalized * speed;
    }

}

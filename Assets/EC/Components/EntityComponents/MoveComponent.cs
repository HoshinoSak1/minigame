using cfg;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum MoveType
{
    WALK = 1,
    RUN = 2,
    DASH = 3,
}
public class MoveComponent : Component
{
    public CharacterComponent character;
    public TransformComponent transformComponent;
    public StateComponent state;
    public ControllerComponent controller;
    public HitComponent hit;

    public float basespeed;
    public float speed;
    //�߻�û�����ݣ�д���ƶ���ײ��ⷶΧ
    public float radius = 0.5f;

    public MoveType moveType;
    public Vector2 input;
    public Vector2 moveTo;
    public bool forceMoveOffset = false;
    public bool needRaycaster = true;

    //�ٶȱ仯Ч������
    SpeedChange speedChange;
    List<float> timeCount;
    int idx = 0;

    float forceMoveTime;
    float forceMoveSpeed;
    Vector2 forceMoveDir = new Vector2();

    public override void Init()
    {
        transformComponent = (TransformComponent)entity.GetComponent("TransformComponent");
        state = (StateComponent)entity.GetComponent("StateComponent");
        controller = (ControllerComponent)entity.GetComponent("ControllerComponent");

        moveType = MoveType.WALK;
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");
        hit = (HitComponent)entity.GetComponent("HitComponent");
    }
    public override void DataInit()
    {
        //�ǽ�ɫʱ����ɫ�������speed
        if(character != null)
        {
            basespeed = character.configs.Speed;
        }
    }

    /// <summary>
    /// ���þ��������ֵ
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        this.basespeed = speed;
    }
    /// <summary>
    /// �������ŵ�����ֵ
    /// </summary>
    /// <param name="ratio">����</param>
    public void SetSpeedRatio(float ratio)
    {
        this.speed = basespeed * ratio;
    }

    public override void Update()
    {
        if(controller != null) input = controller.movepos;
        bool needMove = input != Vector2.zero;

        UpdateState(needMove);
        speed = basespeed;
        if(hit != null)
        {
            if(hit.speedChangeEffect != null)
            {
                speedChange = hit.speedChangeEffect.speedChange;
                hit.speedChangeEffect = null;
                SetSpeed();
            }
            if(hit.dirChangeEffect != null)
            {
                SetForceMove(hit.dirChangeEffect.targetTime,hit.dirChangeEffect.targetSpeeds, hit.dirChangeEffect.dir);
                hit.dirChangeEffect = null;
            }
        }
        if(forceMoveTime>0)
        {
            forceMoveTime = forceMoveTime - Time.deltaTime;
            ForceMove(forceMoveDir, forceMoveSpeed, forceMoveTime);
            UpdateSpeed();
            return;
        }

        UpdateSpeed();
        UpdateMove(needMove);
    }

    public void SetSpeed()
    {
        timeCount = new List<float>();
        for(int i = 0;i<speedChange.Time.Count;i++)
        {
            timeCount.Add(speedChange.Time[i]);
        }
    }
    public void UpdateSpeed()
    {
        if (speedChange == null) return;
        if(idx >= timeCount.Count)
        {
            idx = 0;
            timeCount = null;
            SetSpeedRatio(1);
            speedChange = null;
            //Debug.Log("��ǰ�ٶȱ��� ����" );
            return;
        }
        timeCount[idx] -= Time.deltaTime;
        SetSpeedRatio(speedChange.Speed[idx]);
        //Debug.Log("��ǰ�ٶȱ��� �� " + speedChange.Speed[idx] + " ʣ��ʱ�䣺[ " + timeCount[idx] +" ]");
        if (timeCount[idx] <= 0)
        {
            idx ++;
        }
    }

    public void UpdateState( bool needMove)
    {
        if (state != null)
        {
            if (state.state == State.DESTROY || state.state == State.WAITDESTROY || state.state == State.DEATH)
            {
                basespeed = 0;
                speed = 0;
                return;
            }
            if (!needMove)
            {
                state.state = State.IDLE;
                return;
            }

            state.state = State.MOVE;
        }
    }
    public void UpdateMove(bool needMove)
    {
        if (!needMove) { return; }
        input = input.normalized;
        DoMove(input.x, input.y);
        Vector2 needTo = (Vector2)transformComponent.position + new Vector2(moveTo.x, moveTo.y).normalized * moveTo.magnitude;
        if (needRaycaster)
        {
            if (moveType != MoveType.DASH)
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, radius, LayerMask.GetMask("Enemy"));
            }
            else
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, radius);
            }
        }
        transformComponent.SetPostion(needTo.x, needTo.y);
    }

    public void SetForceMove(float targetTime, float targetSpeeds, Vector2 dir)
    {
        moveType = MoveType.DASH;
        forceMoveTime = targetTime;
        forceMoveSpeed = targetSpeeds;
        forceMoveDir = new Vector2( dir.x,dir.y);
    }

    public void ForceMove(Vector2 dir,float speed,float time)
    {
        DoForceMove(dir.x, dir.y, speed);
        Vector2 needTo = (Vector2)transformComponent.position + new Vector2(moveTo.x, moveTo.y).normalized * moveTo.magnitude;
        if (needRaycaster)
        {
            if (moveType != MoveType.DASH)
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, radius, LayerMask.GetMask("Enemy"));
            }
            else
            {
                needTo = PhysicsRay.CheckCollision(transformComponent.position, moveTo.x, moveTo.y, radius);
            }
        }
        transformComponent.SetPostion(needTo.x, needTo.y);

        if(time <0)
        {
            time = 0;
            forceMoveDir = Vector2.zero;
            moveType = MoveType.WALK;
        }
    }
    public void DoForceMove(float x, float y,float speed)
    {
        moveTo.x = x * speed * Time.deltaTime;
        moveTo.y = y * speed * Time.deltaTime;
    }
    public void DoMove(float x, float y)
    {
        if (forceMoveOffset) { return; }
        moveTo.x =  x * speed * Time.deltaTime;
        moveTo.y =  y * speed * Time.deltaTime;
    }

    public override void OnCache()
    {
        CachePool.Instance.Cache<MoveComponent>(this);
    }
}

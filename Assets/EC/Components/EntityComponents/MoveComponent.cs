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
    //策划没给数据，写死移动碰撞检测范围
    public float radius = 0.5f;

    public MoveType moveType;
    public Vector2 input;
    public Vector2 moveTo;
    public bool forceMoveOffset = false;
    public bool needRaycaster = true;
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
        //是角色时读角色配置里的speed
        if(character != null)
        {
            basespeed = character.configs.Speed;
        }
    }

    /// <summary>
    /// 配置具体的数据值
    /// </summary>
    /// <param name="speed"></param>
    public void SetSpeed(float speed)
    {
        this.basespeed = speed;
    }
    /// <summary>
    /// 配置缩放的数据值
    /// </summary>
    /// <param name="ratio">倍率</param>
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
        }

        UpdateSpeed();
        UpdateMove(needMove);
    }
    SpeedChange speedChange;
    List<float> timeCount;
    int idx = 0; 
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
            Debug.Log("当前速度比率 正常" );

            return;
        }
        timeCount[idx] -= Time.deltaTime;
        SetSpeedRatio(speedChange.Speed[idx]);
        Debug.Log("当前速度比率 ： " + speedChange.Speed[idx] + " 剩余时间：[ " + timeCount[idx] +" ]");
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

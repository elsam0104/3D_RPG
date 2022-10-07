using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateSleep : State<MonsterFSM>
{
    private Animator animator;

    protected int hashSleep = Animator.StringToHash("sleep");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
    }
    public override void OnStart()
    {
        animator?.SetBool(hashSleep, true);
    }
    public override void OnUpdate(float deltaTime)
    {
        //Debug.Log($"zzz... {stateMachineClass.TiredPoint}");
        stateMachineClass.TiredPoint -= stateMachineClass.HealPoint * Time.deltaTime;
        if(stateMachineClass.TiredPoint<0)
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnEnd()
    {
        animator?.SetBool(hashSleep, false);
    }
   
}

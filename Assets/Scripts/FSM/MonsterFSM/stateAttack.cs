using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateAttack : State<MonsterFSM>
{
    private Animator animator;

    private StateAttackController stateAttackCtl;
    private IAttackAble iAttackAble;

    private int hashAttack = Animator.StringToHash("attack");
    private int hashAttackIdx = Animator.StringToHash("attackIdx");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();

        stateAttackCtl = stateMachineClass.GetComponent<StateAttackController>();
        iAttackAble = stateMachineClass.GetComponent<IAttackAble>();
    }
    public override void OnStart()
    {
        if(iAttackAble==null||iAttackAble.nowAttackBehaviour == null)
        {
            stateMachine.ChangeState<stateIdle>();
            return;
        }

        stateAttackCtl.startStateAttackControllerHandler -= delegateAttackStart;
        stateAttackCtl.endStateAttackControllerHandler -= delegateAttackEnd;
        stateAttackCtl.startStateAttackControllerHandler += delegateAttackStart;
        stateAttackCtl.endStateAttackControllerHandler += delegateAttackEnd;

        animator?.SetInteger(hashAttackIdx, iAttackAble.nowAttackBehaviour.anyMotionIdx);
        animator?.SetTrigger(hashAttackIdx);
    }
    public override void OnUpdate(float deltaTime)
    {

    }
    public override void OnHitEvent()
    {

    }
    public void delegateAttackStart()
    {
        Debug.Log("delegate attack start");
    }
    public void delegateAttackEnd()
    {
        Debug.Log("delegate attack end");
        stateMachine.ChangeState<stateIdle>();
    }
}

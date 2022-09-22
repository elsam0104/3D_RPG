using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateAttack : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;
    private Transform monsterTranform;

    private int hashMove = Animator.StringToHash("move");
    private int hashAttack = Animator.StringToHash("attack");
    private int hashTarget = Animator.StringToHash("target");
    private int hashMoveSpeed = Animator.StringToHash("moveSpeed");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();
        monsterTranform = stateMachineClass.GetComponent<Transform>();
    }
    public override void OnStart()
    {
        animator?.SetTrigger(hashAttack);
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        if(target)
        {
            Vector3 dir = target.transform.position - monsterTranform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            monsterTranform.rotation = Quaternion.LerpUnclamped(monsterTranform.rotation, quat, 20.0f * deltaTime);
        }
        else
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    public override void OnHitEvent()
    {
        stateMachine.ChangeState<stateIdle>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class stateIdle : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    private int hashMove = Animator.StringToHash("move");
    private int hashAttack = Animator.StringToHash("attack");
    private int hashTarget = Animator.StringToHash("target");
    private int hashMoveSpeed = Animator.StringToHash("moveSpeed");

    public override void OnAwake()
    {
        animator = stateMachineClass.GetComponent<Animator>();
        characterController = stateMachineClass.GetComponent<CharacterController>();
        agent = stateMachineClass.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        animator?.SetBool(hashMove, false);
        animator?.SetBool(hashTarget, false);
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        if(target)
        {
            if(stateMachineClass.GetFlagAttack)
            {
                stateMachine.ChangeState<stateAttack>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
    }
}

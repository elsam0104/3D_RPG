using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class stateIdle : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    public bool isRomming = false;
    private float minIdleTime = 0.0f;
    private float maxIdleTime = 2.0f;
    private float retIdleTime = 0.0f;

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

        if (isRomming)
        {
            retIdleTime = Random.Range(minIdleTime, maxIdleTime);
            //대기시간 조정
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
       
        if (target)
        {
            if (stateMachineClass.GetFlagAttack)
            {
                stateMachine.ChangeState<stateAttack>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
        else if (isRomming && stateMachine.getStateDurationTime > retIdleTime)
        {
            stateMachine.ChangeState<StateRomming>();
        }
        else if (stateMachineClass.TiredPoint>=stateMachineClass.SleepTime)
        {
            stateMachine.ChangeState<stateSleep>();
        }
    }
}

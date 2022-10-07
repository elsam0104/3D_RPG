using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class stateMove : State<MonsterFSM>
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
        animator?.SetBool(hashMove, true);
        agent?.SetDestination(stateMachineClass.target.position);
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = stateMachineClass.SearchEnemy();
        stateMachineClass.TiredPoint += stateMachineClass.TiredIncreament * Time.deltaTime;
        if (target)
        {
            agent.SetDestination(stateMachineClass.target.position);
            if (agent.remainingDistance > agent.stoppingDistance) //가고있는 중
            {
                characterController.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);
            }
            else //도착
            {
                stateMachine.ChangeState<stateIdle>();
            }
        }
        else //타겟 안 보임
        {
            stateMachine.ChangeState<stateIdle>();
        }
    }
    
    public override void OnEnd()
    {
        agent?.ResetPath();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateRomming : State<MonsterFSM>
{
    private Animator animator;
    private CharacterController characterController;
    private NavMeshAgent agent;

    private MonsterFSM monsterFSM;

    protected int hashMove = Animator.StringToHash("move");
    protected int hashAttack = Animator.StringToHash("attack");
    protected int hashMoveSpeed = Animator.StringToHash("moveSpeed");

    public override void OnAwake()
    {
        monsterFSM = stateMachineClass as MonsterFSM;

        animator = monsterFSM.GetComponent<Animator>();
        characterController = monsterFSM.GetComponent<CharacterController>();
        agent = monsterFSM.GetComponent<NavMeshAgent>();
    }

    public override void OnStart()
    {
        Transform posTarget = null;
        if (monsterFSM.posTarget == null) // ���� �� �� ����
            posTarget = monsterFSM.SearchNextTargetPositon();
        else
            posTarget = monsterFSM.posTarget;

        agent.stoppingDistance = 0;
        if (posTarget)
        {
            agent?.SetDestination(posTarget.position);
            animator?.SetBool(hashMove, true);
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        Transform target = monsterFSM.SearchEnemy();
        stateMachineClass.TiredPoint += stateMachineClass.TiredIncreament * Time.deltaTime;
        
        if (target)
        {
            if (monsterFSM.GetFlagAttack)
            {
                stateMachine.ChangeState<stateAttack>();
            }
            else
            {
                stateMachine.ChangeState<stateMove>();
            }
        }
        else
        {
            if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance + 0.001f))//��� ��� �������� + �����ؾ���
            {
                Transform posTarget = monsterFSM.SearchNextTargetPositon();
                if (posTarget != null)
                    agent?.SetDestination(posTarget.position);
                stateMachine.ChangeState<stateIdle>();
            }
            else
            {
                characterController.Move(agent.velocity * deltaTime);
                animator.SetFloat(hashMoveSpeed, agent.velocity.magnitude / agent.speed, 0.1f, deltaTime);
            }
        }
    }
   
    public override void OnEnd()
    {
        agent?.ResetPath();
        agent.stoppingDistance = monsterFSM.attackRange;
    }
}

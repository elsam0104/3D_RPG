using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM : MonoBehaviour
{
    private StateMachine<MonsterFSM> fsmManager;
    public StateMachine<MonsterFSM> FsmManager => fsmManager;

    private FieldOfView fov;
    public Transform target => fov.FirstTarget;

    public float attackRange;

    public Transform[] posTargets;
    public Transform posTarget = null; //현 로밍 위치
    public int posTargetIdx = 0;
    public float TiredPoint { get; set; } = 0f;
    public float HealPoint { get; set; } = 5.0f;
    public float TiredIncreament { get; } = 3.0f;
    public float SleepTime { get; } = 50f;
    public bool GetFlagAttack
    {
        get
        {
            if (target == null)
                return false;
            float distance = Vector3.Distance(transform.position, target.position);
            return (distance <= attackRange);
        }
    }
    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        fsmManager = new StateMachine<MonsterFSM>(this, new stateIdle());

        fsmManager.AddStateList(new stateMove());
        fsmManager.AddStateList(new stateAttack());

    }
    private void Update()
    {
        fsmManager.OnUpdate(Time.deltaTime);
    }
    public void OnHitEvent()
    {
        Debug.Log("OnHitEvent");
        fsmManager.OnHitEvent();
    }
    public Transform SearchEnemy()
    {
        return target;
    }

    public Transform SearchNextTargetPositon()
    {
        posTarget = null;
        if (posTargets.Length > 0 && posTargets.Length > posTargetIdx)
            posTarget = posTargets[posTargetIdx];

        posTargetIdx = (posTargetIdx + 1) % posTargets.Length;

        return posTarget;
    }
}

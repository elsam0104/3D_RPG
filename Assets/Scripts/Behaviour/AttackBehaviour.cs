using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public int anyMotionIdx;
    public int importanceAttackNo;
    public int attackDamage;
    public float attackRange = 2f;

    [SerializeField]
    private float attackCoolTime;
    protected float nowAttackCoolTime = 0.0f;

    public bool IsAvaliable => nowAttackCoolTime >= attackCoolTime;

    public GameObject effectPrefab;
    public LayerMask targetLayerMask;

    private void Start()
    {
        //시작하자마자 바로 공격 가능
        nowAttackCoolTime = attackCoolTime;
    }
    private void Update()
    {
        if(nowAttackCoolTime<attackCoolTime)
        {
            nowAttackCoolTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">공격할 적</param>
    /// <param name="posAttackStart">발사체 발사 위치</param>
    public abstract void CallAttackMotion(GameObject target = null, Transform posAttackStart = null);
}

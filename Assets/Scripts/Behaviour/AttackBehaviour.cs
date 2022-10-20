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
        //�������ڸ��� �ٷ� ���� ����
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
    /// <param name="target">������ ��</param>
    /// <param name="posAttackStart">�߻�ü �߻� ��ġ</param>
    public abstract void CallAttackMotion(GameObject target = null, Transform posAttackStart = null);
}

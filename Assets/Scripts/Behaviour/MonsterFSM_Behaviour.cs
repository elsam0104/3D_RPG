using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSM_Behaviour : MonsterFSM, IAttackAble, IDamageAble
{

    public AttackBehaviour nowAttackBehaviour
    {
        get;
        private set;
    }
    public int Hp
    {
        get;
        private set;
    }

    public int maxHP = 100;
    public bool GetFlagLive => Hp > 0;

    public LayerMask targetLayerMask;
    public Transform projectileStartTransform;
    public Transform weaponHitTranform;

    [SerializeField]
    private List<AttackBehaviour> attackBehaviours = new List<AttackBehaviour>();

    public void OnExeculteAttack()
    {
        if(nowAttackBehaviour !=null&&target!=null)
        {
            nowAttackBehaviour.CallAttackMotion(target.gameObject, projectileStartTransform);
        }
    }

    public void SetDamage(int dmg, GameObject EffectPrefab)
    {
        if (!GetFlagLive)
            return;
        Hp -= dmg;

        if (EffectPrefab)
            Instantiate(EffectPrefab, weaponHitTranform);
    }

    protected override void Start()
    {
        base.Start();
        OnAwakeAttackBehaviour();

        attackRange = nowAttackBehaviour?.attackRange ?? 2.0f;
        Hp = maxHP;
    }
    protected override void Update()
    {
        base.Update();
        OnCheckAttackBehaviour();
    }


    private void OnAwakeAttackBehaviour()
    {
        foreach(AttackBehaviour behaviour in attackBehaviours)
        {
            if (nowAttackBehaviour == null)
                nowAttackBehaviour = behaviour;

            behaviour.targetLayerMask = targetLayerMask;
        }
    }
    private void OnCheckAttackBehaviour()
    {
        if(nowAttackBehaviour == null||!nowAttackBehaviour.IsAvaliable)
        {
            nowAttackBehaviour = null;
            foreach(AttackBehaviour behaviour in attackBehaviours)
            {
                if(behaviour.IsAvaliable)
                {
                    if( (nowAttackBehaviour=null)||(nowAttackBehaviour.importanceAttackNo <behaviour.importanceAttackNo))
                    {
                        nowAttackBehaviour = behaviour;
                        attackRange = nowAttackBehaviour.attackRange;
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleeAttackBehaviour : AttackBehaviour
{
    [SerializeField]
    MaleeAttackCollision attackCollision;
    public override void CallAttackMotion(GameObject target = null, Transform posAttackStart = null)
    {
        Collider[] colliders = attackCollision?.CheckOverlapBox(targetLayerMask);

        foreach(Collider col in colliders)
        {
            col.gameObject.GetComponent<IDamageAble>()?.SetDamage(attackDamage, effectPrefab);
        }
        nowAttackCoolTime = 0.0f;
    }
}

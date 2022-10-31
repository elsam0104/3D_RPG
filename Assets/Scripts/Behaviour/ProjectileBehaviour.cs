using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : AttackBehaviour
{
    public override void CallAttackMotion(GameObject target = null, Transform posAttackStart = null)
    {
        if (target == null) return;

        Vector3 vecProjectile = posAttackStart?.position ?? transform.position;

        if (effectPrefab != null)
        {
            GameObject objProjectile = GameObject.Instantiate<GameObject>(effectPrefab, vecProjectile, Quaternion.identity);
            objProjectile.transform.forward = transform.forward;

            ProjectileAttackCollision colid = objProjectile.GetComponent<ProjectileAttackCollision>();

        }
    }
}

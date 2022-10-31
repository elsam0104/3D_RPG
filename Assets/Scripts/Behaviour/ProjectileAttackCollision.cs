using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackCollision : MonoBehaviour
{
    public float projectileSpeed;

    public GameObject objProjectileStartEffect;
    public GameObject objProjectileHitEffect;

    public AudioClip projectileStartClip;
    public AudioClip projectileHitClip;

    //내부적으로 충돌 확인
    private bool flagProjectileCollid;

    private Rigidbody rigid;

    [HideInInspector]
    public AttackBehaviour attackBehaviour;
    [HideInInspector]
    public GameObject projectileParents;
    [HideInInspector]
    public GameObject target;

    private void Start()
    {
        if (projectileParents != null)
        {
            Collider collidProjectile = GetComponent<Collider>();
            Collider[] collidParents = projectileParents.GetComponentsInChildren<Collider>();
            foreach (Collider col in collidParents)
            {
                Physics.IgnoreCollision(collidProjectile, col);
            }
        }

        rigid = GetComponent<Rigidbody>();

        if (objProjectileStartEffect != null)
        {
            var projectileStartEffect = Instantiate(this.objProjectileStartEffect, transform.position, Quaternion.identity);

            projectileStartEffect.transform.forward = gameObject.transform.forward;

            ParticleSystem particleSystem = projectileStartEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(projectileStartEffect, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem particleSystemChild = projectileStartEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (particleSystemChild)
                {
                    Destroy(projectileStartEffect, particleSystemChild.main.duration);
                }
            }
        }
        if (projectileStartClip != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(projectileStartClip);
        }

        if (target != null)
        {
            Vector3 vecProjectile = target.transform.position;
            vecProjectile.y += 1.0f;
            transform.LookAt(vecProjectile);
        }
    }

    private void FixedUpdate()
    {
        if (rigid != null && projectileSpeed > 0)
        {
            rigid.position += transform.forward * (Time.deltaTime * projectileSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (flagProjectileCollid)
            return;

        flagProjectileCollid = true;
        Collider projectileCollider = GetComponent<Collider>();
        projectileCollider.enabled = false;

        projectileSpeed = 0;
        rigid.isKinematic = true;

        ContactPoint contactPoint = collision.contacts[0];
        Quaternion rotationContact = Quaternion.FromToRotation(Vector3.up, contactPoint.normal); //충돌한 곳 법선 벡터
        Vector3 vecContact = contactPoint.point;

        if (objProjectileHitEffect != null)
        {
            GameObject projectileHitEffect = Instantiate(objProjectileHitEffect, vecContact, rotationContact) as GameObject;

            ParticleSystem particleSystem = projectileHitEffect.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(projectileHitEffect, particleSystem.main.duration);
            }
            else
            {
                ParticleSystem particleSystemChild = projectileHitEffect.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (particleSystemChild)
                {
                    Destroy(projectileHitEffect, particleSystemChild.main.duration);
                }
            }
        }

        if(projectileHitClip!= null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(projectileHitClip);
        }

        IDamageAble iDmgAble = collision.gameObject.GetComponent<IDamageAble>();
        if(iDmgAble != null)
        {
            iDmgAble.SetDamage(attackBehaviour?.attackDamage ?? 0, null); // ??-> 앞의 값이 false면 앞의 값
        }

        Destroy(gameObject);
    }
}
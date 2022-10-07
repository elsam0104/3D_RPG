using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Sight Elements")]
    public float eyeRadius = 5f;
    [Range(0, 360)]
    public float eyeAngle = 90f;

    [Header("Search Elements")]
    public float delayFindTime = 0.2f;

    public LayerMask targetLayerMask;
    public LayerMask blockLayerMask;

    private List<Transform> targetLists = new List<Transform>();
    private Transform firstTarget;
    private float distanceTarget = 0.0f;

    public List<Transform> TargetLists => targetLists;
    public Transform FirstTarget => firstTarget;
    public float DistansTarget => distanceTarget;

    private void Start()
    {
        StartCoroutine("UpdateFindTargets", delayFindTime);
    }

    IEnumerator UpdateFindTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindTargets();
        }
    }
    void FindTargets()
    {
        //init
        distanceTarget = 0.0f;
        firstTarget = null;
        targetLists.Clear();

        Collider[] overlapSphereTargets = Physics.OverlapSphere(transform.position, eyeRadius, targetLayerMask);

        for (int i = 0; i < overlapSphereTargets.Length; i++)
        {
            Transform target = overlapSphereTargets[i].transform;
            Vector3 lookAtTarget = (target.position - transform.position).normalized; //나를 바라보는 방향

            if (Vector3.Angle(transform.forward, lookAtTarget) < eyeAngle / 2) //시야범위 안
            {
                float firstTargetDistance = Vector3.Distance(transform.position, target.position);

                //적과 나 사이에 장애물 판별
                if (!Physics.Raycast(transform.position, lookAtTarget, firstTargetDistance, blockLayerMask))
                {
                    targetLists.Add(target);

                    if (firstTarget == null || (distanceTarget > firstTargetDistance))
                    {
                        firstTarget = target;
                        distanceTarget = firstTargetDistance;
                    }
                }
            }
        }
    }
}

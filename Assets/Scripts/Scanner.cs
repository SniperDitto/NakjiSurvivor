using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform res = null;
        float diff = 100;

        foreach (var t in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = t.transform.position;
            float currentDiff = Vector3.Distance(myPos, targetPos);

            if (currentDiff < diff)
            {
                diff = currentDiff;
                res = t.transform;
            }
        }
        
        return res;
    }
}

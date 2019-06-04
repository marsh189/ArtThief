using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOW : MonoBehaviour
{
    public float viewRadius = 5;
    public float viewAngle = 135;
    public LayerMask obstacleMask;
    public LayerMask targetMask;
    public List<Transform> visibleTargets = new List<Transform>();

    Collider2D[] targetsInRadius;


    void FixedUpdate()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        targetsInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius);

        visibleTargets.Clear();

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

            if (Vector2.Angle(dirTarget, transform.right) < viewAngle/2)
            {
                float distToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirTarget, distToTarget, obstacleMask))
                {
                    if (Physics2D.Raycast(transform.position, dirTarget, distToTarget, targetMask))
                    {
                        visibleTargets.Add(target);
                    }
                    
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleDeg, bool global)
    {
        if (!global)
        {
            angleDeg += transform.eulerAngles.z;
        }

        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }
}

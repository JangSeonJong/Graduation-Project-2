using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewAngle : MonoBehaviour
{
    [SerializeField]
    float viewAngle;
    [SerializeField]
    float viewDistance;
    [SerializeField]
    LayerMask targetMask;

    Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool SearchTarget()
    {
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Debug.DrawRay(transform.position + transform.up, leftBoundary * viewDistance, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary * viewDistance, Color.red);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        foreach (Collider col in target)
        {
            Transform targetPos = col.transform;
            if (targetPos.tag == "Player")
            {
                Vector3 dir = (targetPos.position - transform.position).normalized;
                float angle = Vector3.Angle(dir, transform.forward);

                if (angle < viewAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, dir, out hit, viewDistance))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            Debug.DrawRay(transform.position + transform.up, dir, Color.blue);

                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
}


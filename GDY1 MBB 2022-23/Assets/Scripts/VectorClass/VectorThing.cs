using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorThing : MonoBehaviour
{
    public Transform A;
    public Transform B;

    public float lengthOfVector;
    public float lengthOfNormalizedVector;

    [Range(1f, 10f)]
    public float offset;

    public float aTobDistance;

    private void OnDrawGizmos()
    {
        //Vector2 point = transform.position;
        //lengthOfVector = point.magnitude;

        //Vector2 directionToPoint = point.normalized;
        //lengthOfNormalizedVector = directionToPoint.magnitude;

        //Gizmos.DrawLine(Vector2.zero, directionToPoint);

        Vector2 aPosition = A.position;
        Vector2 bPosition = B.position;

        //Gizmos.DrawLine(aPosition, bPosition);

        aTobDistance = (bPosition - aPosition).magnitude;

        Vector2 aTob = bPosition - aPosition;
        Vector2 aTobDirectionOnly = aTob.normalized;
        Gizmos.DrawLine(aPosition, aPosition + aTobDirectionOnly);
        //Gizmos.DrawLine(Vector2.zero, aTobDirectionOnly);

        //Vector2 midPoint = (aPosition + bPosition) / 2;
        Vector2 offsetVector = aTobDirectionOnly * offset;

        Gizmos.DrawSphere(aPosition + offsetVector, 0.1f);

    }
}

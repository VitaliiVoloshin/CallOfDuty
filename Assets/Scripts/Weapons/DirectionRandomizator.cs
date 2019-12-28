using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionRandomizator : MonoBehaviour
{
    public Vector3 GetRandomDirection(Transform directionStartPoint, Vector2 direction)
    {
        return RandomRayPoint(directionStartPoint,direction.x, direction.y);
    }

    Vector3 RandomRayPoint(Transform startPoint,float spread, float range)
    {
        float degree = Random.Range(-spread / 2, spread / 2);
        Quaternion angle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        return angle * startPoint.forward * range;
    }
}

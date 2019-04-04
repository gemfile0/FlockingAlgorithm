using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, 
                                          List<Transform> context, 
                                          Flock flock)
    {
        Vector3 centerOffset = flock.Center - agent.transform.position;
        float t = centerOffset.magnitude / flock.StayinRadius;
        if (t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
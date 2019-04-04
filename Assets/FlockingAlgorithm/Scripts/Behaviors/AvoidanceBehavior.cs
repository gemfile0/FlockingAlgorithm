using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;

    public override Vector3 CalculateMove(FlockAgent agent,
                                          List<Transform> context, 
                                          Flock flock)
    {
        if (context.Count == 0) 
        {
            return Vector3.zero;
        }

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context, flock.Agents);
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        int nAvoid = 0;
        Vector3 avoidanceMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoidanceMove += (agent.transform.position - item.position);
                nAvoid++;
            }
        }

        if (nAvoid > 0) 
        {
            avoidanceMove /= nAvoid;
        }
        avoidanceMove = Vector3.SmoothDamp(agent.transform.forward, avoidanceMove, ref currentVelocity, flock.AgentSmoothTime);
        return avoidanceMove;
    }
}
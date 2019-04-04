using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
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

        Vector3 cohesionMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
            cohesionMove += item.position;
        }
        cohesionMove /= filteredContext.Count;

        // create offset from agent position
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, flock.AgentSmoothTime);
        return cohesionMove;
    }
}
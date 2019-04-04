using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, 
                                          List<Transform> context, 
                                          Flock flock)
    {
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context, flock.Agents);
        if (filteredContext.Count == 0) 
        {
            return agent.transform.forward;
        }

        Vector3 alignmentMove = Vector3.zero;
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.forward;
        }
        alignmentMove /= filteredContext.Count;

        return alignmentMove;
    }
}
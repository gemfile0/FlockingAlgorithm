using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original, Dictionary<string, FlockAgent> agents)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            FlockAgent itemAgent = agents[item.name];
            if (itemAgent == null) 
            {
                Debug.LogError("This sholud not happen.");
            }

            if (itemAgent.Flock == agent.Flock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
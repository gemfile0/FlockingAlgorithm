using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Dictionary<string, FlockAgent> Agents { get { return agents; } }
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public float NeighborRadius { get { return neighborRadius; } }
    public float AvoidanceRadius { get { return avoidanceRadius; } }
    public Vector3 Center { get { return center; } }
    public float StayinRadius { get { return stayinRadius; } }
    public float AgentSmoothTime { get { return agentSmoothTime; } }

    [SerializeField] private FlockAgent agentPrefab;
    [SerializeField] private FlockBehavior behavior;
    [SerializeField, Range(1, 500)] private int startingCount = 250;
    [SerializeField, Range(1f, 100f)] private float driveFactor = 10f;
    [SerializeField, Range(1f, 100f)] private float maxSpeed = 5f;
    [SerializeField, Range(1f, 10f)] private float neighborRadius = 1.5f;
    [SerializeField, Range(0f, 1f)] private float avoidanceRadiusMultiplier = 0.5f;
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField, Range(10f, 20f)] private float stayinRadius = 15f;
    [SerializeField] private float agentSmoothTime = 0.5f;

    private Dictionary<string, FlockAgent> agents = new Dictionary<string, FlockAgent>();
    private const float AgentDensity = 0.08f;

    // square* prefix 가 붙은 변수들은 벡터의 비교 연산의 성능을 높이기 위한 용도입니다.
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    private float avoidanceRadius;

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        avoidanceRadius = neighborRadius * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * AgentDensity,
                Random.rotation,
                transform
            );
            newAgent.name = string.Format("Agent {0}", i);
            newAgent.Initialize(this);
            agents.Add(newAgent.name, newAgent);
        }
    }

    void Update()
    {
        foreach (FlockAgent agent in agents.Values)
        {
            List<Transform> context = GetNearByObjects(agent);

            // FOR DEMO ONLY
            // agent.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.Lerp(Color.white, Color.red, context.Count / 6f));

            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
            agent.NeighborCount = context.Count;
        }
    }

    List<Transform> GetNearByObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.CachedTransform.position, neighborRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.CachedCollider)
            {
                context.Add(c.transform.parent);
            }
        }

        return context;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    public Flock Flock { get; private set; }
    
    public Collider CachedCollider { get; private set; }

    public Transform CachedTransform { get; private set; }
    public int NeighborCount { get; set; }

    void Start()
    {
        CachedCollider = GetComponentInChildren<Collider>();
        CachedTransform = transform;
    }

    public void Initialize(Flock flock)
    {
        Flock = flock;
    }

    public void Move(Vector3 velocity)
    {
        if (velocity != Vector3.zero) {
            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
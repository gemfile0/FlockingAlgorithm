using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    public abstract Vector3 CalculateMove(FlockAgent agent/* 자기 자신 */, 
                                          List<Transform> context/* 주변의 무리 */, 
                                          Flock flock/* 자신의 종 */);
}
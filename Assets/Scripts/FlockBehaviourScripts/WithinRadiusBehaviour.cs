using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Within Radius")]
public class WithinRadiusBehaviour : FlockBehaviour
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //maintain the flock with a fixed radius
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float c = centerOffset.magnitude / radius;

        //check proximity to the radius
        if(c < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * c * c;
    }
}

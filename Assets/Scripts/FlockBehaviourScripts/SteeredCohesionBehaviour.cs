using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FlockBehaviour
{
    Vector2 currentVelocity;
    public float smoothTime = 0.5f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // return nno changes if no neighbours are present
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 cohesiveMovement = Vector2.zero;
        foreach (Transform unit in context)
        {
            //ensure cohesive behaviour for each transform
            cohesiveMovement += (Vector2)unit.position;
        }
        cohesiveMovement /= context.Count;
        // create the offset depending on the agent position
        cohesiveMovement -= (Vector2)agent.transform.position;
        cohesiveMovement = Vector2.SmoothDamp(agent.transform.right, cohesiveMovement, ref currentVelocity, smoothTime);
        return cohesiveMovement;
    }
}

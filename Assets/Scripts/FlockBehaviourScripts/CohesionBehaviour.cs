using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // return nno changes if no neighbours are present
        if(context.Count == 0)
        {
            return Vector2.zero;
        }
        // averages all the points present
        Vector2 cohesiveMovement = Vector2.zero;
        foreach(Transform unit in context)
        {
            cohesiveMovement += (Vector2)unit.position;
        }
        cohesiveMovement /= context.Count;
        // create the offset depending on the agent position
        cohesiveMovement -= (Vector2)agent.transform.position;
        return cohesiveMovement;
    }
}

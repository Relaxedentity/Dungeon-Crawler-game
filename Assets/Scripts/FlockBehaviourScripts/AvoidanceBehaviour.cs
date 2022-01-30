using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // return nno changes if no neighbours are present
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        Vector2 avoidingMovement = Vector2.zero;
        int n_avoid = 0;
        foreach (Transform unit in context)
        {
            if(Vector2.SqrMagnitude(unit.position - agent.transform.position) < flock.SquareAvoidanceRadius) 
            {
                n_avoid++;
                //the avoiding force vector
                avoidingMovement += (Vector2)(agent.transform.position - unit.position);
            }
        }
        if(n_avoid > 0)
        {
            avoidingMovement /= n_avoid;
        }
        return avoidingMovement;
    }
}

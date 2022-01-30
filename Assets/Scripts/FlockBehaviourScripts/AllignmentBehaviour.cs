using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Allignment")]
public class AllignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Maintain allignment if no neighbours are present
        if (context.Count == 0)
        {
            return agent.transform.right;
        }
        Vector2 allignedMovement = Vector2.zero;
        foreach (Transform unit in context)
        {
            //allign each of their transforms
            allignedMovement += (Vector2)unit.transform.right;
        }
        allignedMovement /= context.Count;
        return allignedMovement;
    }
}

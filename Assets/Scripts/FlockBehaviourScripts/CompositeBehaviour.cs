using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //error handling
        if(weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch" + name, this);
            return Vector2.zero;
        }

        Vector2 movement = Vector2.zero;

        //iteration through the flock behaviours an implement the behaviours according to weight
        for (int i = 0; i< behaviours.Length; i++)
        {
            Vector2 partialMovement = behaviours[i].CalculateMove(agent, context, flock) * weights[i];
            if(partialMovement != Vector2.zero)
            {
                if(partialMovement.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMovement.Normalize();
                    partialMovement *= weights[i];
                }
                movement += partialMovement;
            }
        }
        return movement;
    }
}

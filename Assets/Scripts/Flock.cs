using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agent;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [Range(10, 500)]
    public int flockSize = 10;
    const float agentDensity = 0.08f;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float AR_Multiplier;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        //assigning variables to assist in calculation
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * AR_Multiplier * AR_Multiplier;

        for (int i = 0; i < flockSize; i++)
        {
            //instantiate the flock 
            FlockAgent newAgent = Instantiate(
                agent, new Vector3(Random.Range(transform.position.x - 4, transform.position.x + 4),
                    Random.Range(transform.position.y - 4, transform.position.y + 4), transform.position.z), Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            //organise agents
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }

    }
    private void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 movement = behaviour.CalculateMove(agent, context, this);
            movement *= driveFactor;
            if(movement.sqrMagnitude > squareMaxSpeed)
            {
                movement = movement.normalized * maxSpeed;
            }
            agent.Motion(movement);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        //method to place nearby objects into a list
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach(Collider2D collider in contextColliders)
        {
            if(collider != agent.AgentCollider)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }
}

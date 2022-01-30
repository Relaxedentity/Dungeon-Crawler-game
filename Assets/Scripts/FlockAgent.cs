using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    public int health;

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Motion(Vector2 velocity)
    {
        //orientation of the agent
        transform.right = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            health -= 5;
            if (health <= 0)
            {
                this.gameObject.SetActive(false);
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenSword : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;


    public void Setup(Vector2 velocity, Vector3 direction)
    {
        //setup the velocity and rotation of the projectile
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Enemy"))
            //allow the projectile to go through enemies
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //allow the projectile to go through enemies
        if (other.name != "Player" )
        { 
            if (!other.gameObject.CompareTag("Enemy")) 
                {
                    if (!other.gameObject.CompareTag("RoomCollider"))
                    {
                        Destroy(this.gameObject);
                    }
                }
        }
    }
}

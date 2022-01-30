using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeDummy : Enemy
{
    public override void receiveDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health += 100;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}

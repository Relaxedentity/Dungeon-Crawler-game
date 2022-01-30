using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    //enemy state machine
    idle,
    walk,
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentEnemyState;
    public FloatValue maxHealth;
    public float health;
    public string mobName;
    public float movementSpeed;
    public bool isBoss;
    public LootTable thisLoot;
    public NavMeshAgent agent;
    public GameObject deathEffect;
    public GameObject spawnEffect;

    private void Awake()
    {
        health = maxHealth.intialValue;
        agent = GetComponent<NavMeshAgent>();
        GameObject spawneffect = Instantiate(spawnEffect, transform.position, Quaternion.identity);
        spawneffect.SetActive(true);
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    public void Knock(Rigidbody2D enemyRigidbody, float knockTime, float damage)
    {
        // run the knock coroutine to be used in knockback script
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(KnockCo(enemyRigidbody, knockTime));
            receiveDamage(damage);

        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidBody, float knockbackTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            myRigidBody.velocity = Vector2.zero;
            currentEnemyState = EnemyState.idle;


        }
    }
    public virtual void receiveDamage(float damage)
    {
        health -= damage;
        if(health <=0)
        {
            MakeLoot();
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            effect.SetActive(true);
 
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
    }
    public void MakeLoot()
    {
        //loot creation method
        if (thisLoot != null)
        {
            GameObject current = thisLoot.LootPowerup(); 
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}

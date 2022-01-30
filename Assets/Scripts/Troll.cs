using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : Demon
{
    public enum TrollState
    {
        // boss states
        neutral,
        scared

    }

    public Transform popUp;
    public float period = 0.0f;
    public TrollState currentTrollState;
    float playerHealth;
    float playerMaxHealth;

    void Start()
    {
        currentEnemyState = EnemyState.idle;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerHealth = playerObject.GetComponent<player>().currentHealth;
        playerMaxHealth = playerObject.GetComponent<player>().PlayerHealth.intialValue;
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //change behaviour depending on enemy health
        if (!isBoss)
        {
            checkDistance();
        }
        else if (isBoss && health <= 0.35 * maxHealth.intialValue)
        {
            currentTrollState = TrollState.scared;
            RunAway();
        }
        else if (isBoss && health > 0.35 * maxHealth.intialValue)
        {
            currentTrollState = TrollState.neutral;
            checkDistance();
        }

    }


    private void Update()
    {
        if (currentTrollState == TrollState.scared)
        {
            if (period > 0.35f)
            {
                health += 8;
                Transform damagePopupTransform = Instantiate(popUp, transform.position, Quaternion.identity);
                DamagePopup dmgPopUp = damagePopupTransform.GetComponent<DamagePopup>();
                dmgPopUp.HealingSetup(8);
                period = 0;
            }
        }
        period += Time.deltaTime;
        // create a delay between projectiles
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public virtual void RunAway()
    {
        //checking if the enemy state is not staggered
        if (currentEnemyState == EnemyState.idle || currentEnemyState == EnemyState.walk
            && currentEnemyState != EnemyState.stagger)
        {
            //move away from the target
            //agent.SetDestination(roomTarget.position);
            Vector3 temp = Vector3.MoveTowards(transform.position,
                target.position - transform.position, movementSpeed*5 * Time.deltaTime);
            enemyRigidbody.MovePosition(temp);

            Vector3 tempVector = target.transform.position - transform.position;

            if (canFire)
            {
                //instantiate and launch projectile
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Rigidbody2D>().velocity = tempVector * 2;
                canFire = false;
            }

            changeAnim(target.position - transform.position);
            changeState(EnemyState.walk);
            anim.SetBool("aggro", true);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBoss : Bouncy
{
    public enum BossState
    {
        // boss states
        runAway,
        dealDmg

    }

    public Transform popUp;
    public float period = 0.0f;
    public BossState currentBossState;
    private void FixedUpdate()
    {
        //change behaviour depending on enemy health

        if (!isBoss)
        {
            checkDistance();
        }
        else if (isBoss && health <= 0.35 * maxHealth.intialValue)
        {
            currentBossState = BossState.runAway;
            RunAway();
        }
        else if (isBoss && health > 0.35 * maxHealth.intialValue)
        {
            currentBossState = BossState.dealDmg;
            checkDistance();
        }

    }


    private void Update()
    {
        if (currentBossState == BossState.runAway)
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
                target.position - transform.position, movementSpeed * Time.deltaTime);
            enemyRigidbody.MovePosition(temp);

            changeAnim(target.position - transform.position);
                changeState(EnemyState.walk);
                anim.SetBool("aggro", true);

            }
    }
}

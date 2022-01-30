using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Bouncy
{
    public GameObject projectile;
    public float fireDelay;
    public float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        // create a delay between projectiles
        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }
    public override void checkDistance()
    {
        //checking if the target is within chase and attack radius
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
           && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            //checking if the enemy state is not staggered
            if (currentEnemyState == EnemyState.idle || currentEnemyState == EnemyState.walk
                && currentEnemyState != EnemyState.stagger)
            {
                //move towards the target
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position, movementSpeed * Time.deltaTime);
                //calculate vector for projectile
                Vector3 tempVector = target.transform.position - transform.position;

                if (canFire)
                {
                    //instantiate and launch projectile
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Rigidbody2D>().velocity = tempVector * 2;
                    canFire = false;
                }

                changeAnim(temp - transform.position);
                enemyRigidbody.MovePosition(temp);
                changeState(EnemyState.walk);
                anim.SetBool("aggro", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("aggro", false);
        }
    }
}

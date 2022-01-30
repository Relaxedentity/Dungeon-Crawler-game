using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : Enemy
{
    public Rigidbody2D enemyRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;


    void Start()
    {
        currentEnemyState = EnemyState.idle;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        checkDistance();
    }
    public virtual void checkDistance()
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
                /*Vector3 temp = Vector3.MoveTowards(transform.position,
                    target.position, movementSpeed * Time.deltaTime);
*/
                changeAnim(target.position - transform.position);
                /*enemyRigidbody.MovePosition(temp);*/
                agent.SetDestination(target.position);
                changeState(EnemyState.walk);
                anim.SetBool("aggro", true);

            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("aggro", false);
        }
    }
    public virtual void changeState(EnemyState newState)
    {
        //method for changing enemy states
        if (currentEnemyState != newState)
        {
            currentEnemyState = newState;
        }
    }
    private void setAnimFLoat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }
    public virtual void changeAnim(Vector2 direction)
    {
        //method for changing the animator depending on the direction
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0)
            {
                setAnimFLoat(Vector2.left);
            }
            else if (direction.x > 0)
            {
                setAnimFLoat(Vector2.right);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y < 0)
            {
                setAnimFLoat(Vector2.down);
            }
            else if (direction.y > 0)
            {
                setAnimFLoat(Vector2.up);
            }
        }
    }

}



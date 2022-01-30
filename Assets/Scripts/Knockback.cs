using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbackTime;
    public int minDamage;
    public int maxDamage;
    public Transform popUp;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyFlock") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("floorSpike"))
        {
            Rigidbody2D contact = other.GetComponent<Rigidbody2D>();
            if (contact != null)
            {
                // damage Randomiser
                int damage = Random.Range(minDamage, maxDamage + 1);
                int chance = 3;
                if (other.gameObject.CompareTag("Enemy"))
                {
                    //percentage chance of no damage on boss mobs
                    if (other.GetComponent<Enemy>().isBoss && Random.Range(1, 10) <= chance)
                    {
                        damage = 0;
                    }
                }
                if (damage != 0)
                {
                    //calculate and initiate knockback 
                    Vector2 difference = contact.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    contact.AddForce(difference, ForceMode2D.Impulse);
                }

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    if (damage != 0)
                    {
                        //change enemy state and start knock coroutine
                        contact.GetComponent<Enemy>().currentEnemyState = EnemyState.stagger;
                        //dmg increases depending on collected coins
                        int coins = PlayerPrefs.GetInt("coins");
                        damage += coins / 8;
                        other.GetComponent<Enemy>().Knock(contact, knockbackTime, damage);
                    }

                    //instantiate and set up damage pop up
                    Transform damagePopupTransform = Instantiate(popUp, other.GetComponent<Enemy>().transform.position, Quaternion.identity);
                    DamagePopup dmgPopUp = damagePopupTransform.GetComponent<DamagePopup>();
                    bool critical;
                    if(damage == maxDamage)
                    {
                        critical = true;
                    }
                    else
                    {
                        critical = false;
                    }
                    dmgPopUp.Setup(damage, critical);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    //if player state is not staggered, stagger player and start knock coroutine
                    if (other.GetComponent<player>().currentState != PlayerState.stagger)
                    {
                        contact.GetComponent<player>().currentState = PlayerState.stagger;
                        other.GetComponent<player>().Knock(knockbackTime, damage);

                    }
                }
            }
        }
    }
}

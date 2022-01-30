using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spell : MonoBehaviour
{
    public GameObject projectile;
    public float projectileForce;
    public player player;
    public AudioSource audioSource;
    public AudioClip spellAudio;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if playerstate is not staggered
            if (player.currentState != PlayerState.stagger)
            {
                //instantiate projectile and launch after calculating the vector using the mouse position
                GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPosition = transform.position;
                Vector2 direction = (mousePosition - myPosition).normalized;
                spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                audioSource.PlayOneShot(spellAudio, 0.3f);
            }
        }
    }
}

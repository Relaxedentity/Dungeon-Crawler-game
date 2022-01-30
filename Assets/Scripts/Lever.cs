using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject indicator;
    public bool used;
    public bool playerInRange;
    SpriteRenderer spriteRenderer;
    public Sprite leverOff;
    public Sprite leverOn;

    public void Start()
    {
        used = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverOff;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // is player is within range, display appropriate indicator 
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            if (used == false)
            {
                indicator.SetActive(true);
            }

        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //hide indicators when player leaves the range of the object
        if (collision.gameObject.CompareTag("Player"))
        {
            indicator.SetActive(false);
            playerInRange = false;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && used == false)
        {
            indicator.SetActive(false);
            used = true;
            spriteRenderer.sprite = leverOn;
        }
    }
}

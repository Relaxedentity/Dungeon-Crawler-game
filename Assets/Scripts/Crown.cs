using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public GameObject indicator;
    public GameObject negIndicator;
    public bool playerInRange;
    public SignalSender crown;
    public player player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if player is within acceptable range to display appropriate indicator
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;

            if (player.coins == 200)
            {
                indicator.SetActive(true);
            }
            else if (player.coins != 200)
            {
                negIndicator.SetActive(true);

            }

        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //remove indicator when player leaves acceptable range
        if (collision.gameObject.CompareTag("Player"))
        {
            negIndicator.SetActive(false);
            indicator.SetActive(false);
            playerInRange = false;
        }
    }
    public virtual void Update()
    {
        //active the vanity item for the player if conditions are met
        if (player.coins == 200)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            {
                crown.Raise();
                indicator.SetActive(false);
                int newCoins = player.coins -= 200;
                PlayerPrefs.SetInt("coins", newCoins);
            }
        }
    }
}

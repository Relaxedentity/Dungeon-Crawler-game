using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fountain : MonoBehaviour
{
    public GameObject indicator;
    public GameObject negIndicator;
    public bool playerInRange;
    public SignalSender refresh;
    public bool used;
    public string color;
    public player player;
    Scene c_scene;
    string sceneName;
    public void Start()
    {
        
        used = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // is player is within range, display appropriate indicator 
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            if (used == false)
            {
                if (player.fountainColor == color  || player.fountainColor == "")
                {
                    indicator.SetActive(true);
                }
            }
            else if(used == true || PlayerPrefs.GetString("fountainColor") != color)
            {
                negIndicator.SetActive(true);
                
            }

        }
        
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        //hide indicators when player leaves the range of the object
        if (collision.gameObject.CompareTag("Player"))
        {
            negIndicator.SetActive(false);
            indicator.SetActive(false);
            playerInRange = false;
        }
    }
    public virtual void  Update()
    {
        //allow usage of the object when conditions are fulfilled
        if (player.fountainColor == color || player.fountainColor == "")
        {
            if (Input.GetKeyDown(KeyCode.E) && playerInRange && used == false)
            {
                refresh.Raise();
                indicator.SetActive(false);
                used = true;
                player.fountainColor = color;
                //set fountain color for the rest of the run
                PlayerPrefs.SetString("fountainColor", color);
            }
        }
    }
}

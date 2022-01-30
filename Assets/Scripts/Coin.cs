using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<player>().coins += 1;
            PlayerPrefs.SetInt("coins", player.GetComponent<player>().coins);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] mobs;
    private GameObject mob;
    public int reqMobs;

    private void Start()
    {
        //number of spawned mobs increase depending on collected coins
        int coins = PlayerPrefs.GetInt("coins");
        reqMobs += Mathf.RoundToInt(coins / 15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            
            for (int i = 0; i<reqMobs;i++ )
            {
                //randomly select mobs provided within the array
                int selector = Random.Range(0, mobs.Length);              
                mob = Instantiate(mobs[selector], new Vector3(Random.Range(transform.position.x - 2, transform.position.x + 2), 
                    Random.Range(transform.position.y - 2, transform.position.y + 2), transform.position.z), Quaternion.identity);
                mob.transform.localScale = Vector3.one;
            }
        }
        this.gameObject.SetActive(false);
    }
}

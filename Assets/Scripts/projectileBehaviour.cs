using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name != "Player")
        {
            if (!other.gameObject.CompareTag("RoomCollider"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

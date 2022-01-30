using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyOnDeath : MonoBehaviour
{
    public player player;


    private void Update()
    {
        if (player.currentHealth <= 0)
        {
            StartCoroutine("Death");
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Lobby");
    }
}

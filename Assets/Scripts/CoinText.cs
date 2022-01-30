using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    public Text cointText;
    public player player;

    public void Update()
    {
        cointText.text = player.coins.ToString() + "  (ATK + " + Mathf.RoundToInt(player.coins / 15) + ")" ;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    public TextMeshPro textMesh;
    private float timer;
    private Color textColor;
    public static Transform dmgPopUp;

    public void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


    public void Setup(int damage, bool critical)
    {
        Color newCol;
        textMesh.SetText(damage.ToString());
        if (!critical)
        {
            if(ColorUtility.TryParseHtmlString("#ededed", out newCol))
            {
                textMesh.color = newCol;
            }
        }
        else
        {
            if (ColorUtility.TryParseHtmlString("#ee7272", out newCol))
            {
                textMesh.color = newCol;
            }
        }
        timer = 0.5f;
    }

    public void HealingSetup(int healing)
    {
        Color newColheal;
        textMesh.SetText("+" + healing.ToString());

        if (ColorUtility.TryParseHtmlString("#96ffb2", out newColheal))
        {
            textMesh.color = newColheal;
        }
        timer = 0.5f;
    }

    public void Update()
    {
        float floatingSpeed = 2f;
        transform.position += new Vector3(0, floatingSpeed) * Time.deltaTime;

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            float disappearRate = 3f;
            textColor.a -= disappearRate * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(this.gameObject);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coincollection : MonoBehaviour
{
     static private int Coin = 0;

    public TextMeshProUGUI coinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Coin++;
            coinText.text = "Coin " + Coin.ToString();
            Debug.Log(Coin);
            Destroy(this.gameObject);
        }
    }

}

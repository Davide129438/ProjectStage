using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coincollection : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
            audioManager.PlaySFX(audioManager.collectCoin);
        }
    }

}

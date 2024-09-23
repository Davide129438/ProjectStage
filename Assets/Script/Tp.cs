using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    [SerializeField] Transform tp;

    [SerializeField] GameObject Jammo_LowPoly;

    AudioManager audioManager;
    private void OnTriggerStay(Collider other)
    {
       
        if (other.transform.tag == "Player")
        {
           
            StartCoroutine(Teleport());
        }
           
    }

    IEnumerator Teleport()
    {
       // audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        yield return new WaitForSeconds(0.5f);
        //audioManager.PlaySFX(audioManager.teleportIn);
        Jammo_LowPoly.transform.position = new Vector3(
            tp.transform.position.x,
            tp.transform.position.y,
            tp.transform.position.z
            );
    }
}

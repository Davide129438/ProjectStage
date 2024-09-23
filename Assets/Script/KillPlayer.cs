using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KillPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawn_point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.transform;
           other.gameObject.SetActive(false);
            StartCoroutine(Respawn());
        }
        //GameObject go = Instantiate(player.gameObject);
        //go.transform.position = respawn_point.transform.position;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        player.position = respawn_point.position;
        player.gameObject.SetActive(true);

    }

}

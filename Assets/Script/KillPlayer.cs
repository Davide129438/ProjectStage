using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KillPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawn_point;

    Animator animator;
    int count = 0;
    AudioManager audioManager;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(count == 0)
            {
                count = 1;
                player = other.gameObject.transform;
                animator.SetBool("IsDeath", true);
                animator.Play("Death");
                audioManager.PlaySFX(audioManager.Death);

                StartCoroutine(OffPlayer(player));
                StartCoroutine(Respawn());
            }
            
        }
        //GameObject go = Instantiate(player.gameObject);
        //go.transform.position = respawn_point.transform.position;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
   
        animator.SetBool("IsDeath", false);
        player.position = respawn_point.position;
        player.gameObject.SetActive(true);
        count = 0;

    }

    private IEnumerator OffPlayer(Transform player)
    {
        yield return new WaitForSeconds(1.5f);
        player.gameObject.SetActive(false);
    }

}

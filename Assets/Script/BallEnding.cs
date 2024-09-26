using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallEnding : MonoBehaviour
{
    [SerializeField] string nomeScena;
    [SerializeField] GameObject Explosion;
    AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            //qua ci vuole il codice dell'animazione dell'esplosione e il suono
            Explosion.SetActive(true);
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            audioManager.PlaySFX(audioManager.Exsplosion);
            StartCoroutine(TpScene());
            
        }

    }

    private IEnumerator TpScene()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(nomeScena);
    }

}
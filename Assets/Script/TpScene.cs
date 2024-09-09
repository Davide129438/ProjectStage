using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpScene : MonoBehaviour
{
    [SerializeField] string nomeScena;

 

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            SceneManager.LoadScene(nomeScena);
        }
           
    }

}

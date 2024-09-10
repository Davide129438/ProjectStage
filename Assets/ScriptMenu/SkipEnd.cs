using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipEnd : MonoBehaviour
{
  public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

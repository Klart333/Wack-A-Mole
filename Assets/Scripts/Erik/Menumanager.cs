using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menumanager : MonoBehaviour
{
    // metoder för knapperna, start och quit
   public void Startgame()
    {
        SceneManager.LoadScene(1);
    }

   public void Quitgame()
    {
        Application.Quit();
    }
}

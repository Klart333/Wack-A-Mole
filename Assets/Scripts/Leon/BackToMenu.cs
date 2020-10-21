using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void SwitchToMenu()
    {
        SceneManager.LoadScene(0);  //denna kod byte scen när knappen blir tryckt 
    }
    
    

}

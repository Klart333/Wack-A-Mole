﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void BackToGame()
    {
        SceneManager.LoadScene(1);  //denna kod byte scen när knappen blir tryckt 
    }

   
}

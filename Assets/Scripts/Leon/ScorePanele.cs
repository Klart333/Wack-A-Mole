﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePanele : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();   // här hemtar jag komponenten textmesh pro UGUI
        SkrivPoeng(5);

    }
    
   public void SkrivPoeng(int score)
   {
        scoreText.GetComponent<TextMeshProUGUI>().text = (string)score.ToString();   //
   }

}
